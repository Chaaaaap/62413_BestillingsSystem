using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Models;

namespace DesktopClient.ViewModels
{
    public class ItemOverviewViewModel : BaseViewModel
    {
        #region UI Properties
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

        private string _tmpSearch;

        public string TmpSearch
        {
            get => _tmpSearch;
            set
            {
                if (value == _tmpSearch)
                {
                    return;
                }

                _tmpSearch = value;
                OnPropertyChanged(TmpSearch);
            }
        }
        #endregion

        public ItemOverviewViewModel(BaseViewModel parent) : base(parent)
        {
            InitializeCommands();
            PopulateItems();
        }

        public ICommand SearchItemCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }

        private void InitializeCommands()
        {
            SearchItemCommand = new CommandHandler(SearchItems);
            ClearSearchCommand = new CommandHandler(ClearSearch);
        }

        private async Task<ObservableCollection<Item>> PopulateItems()
        {
            var itemList = await Service.GetAllItems();

            Items.Clear();

            foreach (var item in itemList)
            {
                Items.Add(item);
            }

            return Items;
        }

        private async void SearchItems(object sender)
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

        public async void ClearSearch(object sender)
        {
            await PopulateItems();
            TmpSearch = "";
        }
    }
}
