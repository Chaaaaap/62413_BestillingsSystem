using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WPF.Controls
{
    public class ImageButton : System.Windows.Controls.Button
    {
        #region Properties

        public static readonly DependencyProperty ImageSourceProperty = 
            DependencyProperty.Register("ImageSourceProperty", typeof(object),
            typeof(ImageButton));

        public object ImageSource
        {
            get { return (string) GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileNameProperty", typeof(string),
            typeof(ImageButton));

        public object FileName
        {
            get { return (string) GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        #endregion

        protected override void OnInitialized(EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.HighQuality);
            SnapsToDevicePixels = true;
            base.OnInitialized(e);
        }
    }
}
