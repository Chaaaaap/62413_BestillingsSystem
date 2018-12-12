using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using Common.Models;

namespace DesktopClient.ViewModels
{
    public class AdminItemViewModel : BaseViewModel
    {
        public AdminItemViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
            PopulateItems();
        }

        private Item _selectedItem;

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                CreateItem = false;
                EditItem = true;
                if (_selectedItem == value)
                    return;
                Img = null;
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private ImageSource _img;

        public ImageSource Img
        {
            get => _img;
            set
            {
                if (_img == value)
                    return;
                _img = value;
                OnPropertyChanged(nameof(Img));
            }
        }

        private string _tmpName;

        public string TmpName
        {
            get => _tmpName;
            set
            {
                if (_tmpName == value)
                    return;
                _tmpName = value;
                OnPropertyChanged(nameof(TmpName));
            }
        }

        private string _tmpSearch;

        public string TmpSearch
        {
            get => _tmpSearch;
            set
            {
                if (_tmpSearch == value)
                    return;
                _tmpSearch = value;
                OnPropertyChanged(nameof(TmpSearch));
            }
        }

        private double? _tmpPrice;

        public double? TmpPrice
        {
            get => _tmpPrice;
            set
            {
                if (_tmpPrice == value)
                    return;
                _tmpPrice = value;
                OnPropertyChanged(nameof(TmpPrice));
            }
        }

        private int? _tmpAmount;

        public int? TmpAmount
        {
            get => _tmpAmount;
            set
            {
                if (_tmpAmount == value)
                    return;
                _tmpAmount = value;
                OnPropertyChanged(nameof(TmpAmount));
            }
        }

        private byte[] _tmpPic;

        public byte[] TmpPic
        {
            get => _tmpPic;
            set
            {
                if (_tmpPic == value)
                    return;
                _tmpPic = value;
                OnPropertyChanged(nameof(TmpPic));
            }
        }

        private bool _createItem = false;

        public bool CreateItem
        {
            get => _createItem;
            set
            {
                if (_createItem == value)
                    return;
                _createItem = value;
                OnPropertyChanged(nameof(CreateItem));
            }
        }

        private bool _editItem = false;

        public bool EditItem
        {
            get => _editItem;
            set
            {
                if (_editItem == value)
                    return;
                _editItem = value;
                OnPropertyChanged(nameof(EditItem));
            }
        }

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private async Task<ObservableCollection<Item>> PopulateItems()
        {
            List<Item> itemList = await Service.GetAllItems();
            Items.Clear();
            foreach (Item item in itemList)
            {
                Items.Add(item);
            }
            return Items;
        }

        public ICommand SaveItemCommand
        {
            get;
            private set;
        }
        public ICommand CreateItemCommand
        {
            get;
            private set;
        }
        public ICommand AdminCreateItemCommand
        {
            get;
            private set;
        }
        public ICommand DeleteItemCommand
        {
            get;
            private set;
        }
        public ICommand SearchItemCommand
        {
            get;
            private set;
        }

        public ICommand ClearCommand
        {
            get;
            private set;
        }
        public ICommand BrowseCommand
        {
            get;
            private set;
        }
        private void InitializeCommands()
        {
            SaveItemCommand = new CommandHandler(SaveItemChanges);
            CreateItemCommand = new CommandHandler(AdminCreateItem);
            AdminCreateItemCommand = new CommandHandler(CreateItemClicked);
            DeleteItemCommand = new CommandHandler(DeleteItem);
            SearchItemCommand = new CommandHandler(SearchItem);
            ClearCommand = new CommandHandler(Clear);
            BrowseCommand = new CommandHandler(Browse);
        }

        public void Browse(object sender)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(FD.FileName);
                byte[] arr;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    arr = ms.ToArray();
                    TmpPic = arr;
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();

                    Img = bitmapImage;
                }
            }
        }

        public async void Clear(object sender)
        {
            TmpSearch = "";
            await PopulateItems();
        }

        public async void SearchItem(object sender)
        {
            await PopulateItems();
            var temp = Items.Select(x => x).Where(x => x.Name.ToLower().Contains(TmpSearch.ToLower()));

            List<Item> itemTemp = new List<Item>();

            foreach (Item item in temp)
            {
                itemTemp.Add(item);
            }

            Items.Clear();

            foreach (Item item in itemTemp)
            {
                Items.Add(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        public async void SaveItemChanges(object sender)
        {
            Item item = new Item
            {
                Name = TmpName ?? SelectedItem.Name,
                Price = TmpPrice ?? SelectedItem.Price,
                Amount = TmpAmount ?? SelectedItem.Amount,
                Id = SelectedItem.Id,
                Picture = TmpPic != null ? TmpPic: SelectedItem.Picture ?? new byte[0]
            };

            var updatedItem = await Service.UpdateItem(item);
            await PopulateItems();

            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SelectedItem));

            TmpName = null;
            TmpPrice = null;
            TmpAmount = null;
            TmpPic = null;
        }

        public async void AdminCreateItem(object sender)
        {
            
            Item item = new Item
            {
                Name = TmpName,
                Price = TmpPrice ?? 0.0,
                Amount = TmpAmount ?? 0,
                Picture = TmpPic ?? null
            };

            await Service.CreateItem(item);
            TmpName = null;
            TmpAmount = null;
            TmpPrice = null;
            TmpPic = null;
            await PopulateItems();
        }

        public async void DeleteItem(object sender)
        {
            await Service.DeleteItem(SelectedItem);
            await PopulateItems();
        }

        private void CreateItemClicked(object sender)
        {
            TmpName = null;
            TmpAmount = null;
            TmpPrice = null;
            SelectedItem = null;
            CreateItem = true;
            EditItem = false;

        }
    }
}
