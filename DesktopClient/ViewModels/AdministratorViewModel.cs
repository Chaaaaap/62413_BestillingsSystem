using DesktopClient.Properties;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopClient.ViewModels
{
    public class AdministratorViewModel : BaseViewModel
    {
        public AdministratorViewModel(BaseViewModel parent) : base(parent)
        {
            Name = "Administrator";
            Image img = Image.FromFile("C:\\Users\\cille_000\\Documents\\DTU\\c#\\DesktopClient\\Icons\\key.png");
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                Icon = bitmapImage;
            }
        }

        public string Name { get; set; }

        public ImageSource Icon { get; set; }
    }
}
