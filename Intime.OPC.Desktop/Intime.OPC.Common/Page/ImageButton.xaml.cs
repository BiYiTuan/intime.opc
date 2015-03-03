using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPCApp.Common
{
    /// <summary>
    /// ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : Button {
        public ImageButton() {
            InitializeComponent();

            this.Style = this.FindResource("ImageButtonStyle") as Style;
            this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(ImageButton_IsEnabledChanged);
        }

        void ImageButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (this.IsEnabled && ImageSource != null) {
                innerImage.Source = ImageSource;
            } else if (!this.IsEnabled && GrayImageSource != null) {
                innerImage.Source = GrayImageSource;
            }
        }



        public ImageSource ImageSource {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));



        public ImageSource GrayImageSource {
            get { return (ImageSource)GetValue(GrayImageSourceProperty); }
            set { SetValue(GrayImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GrayImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GrayImageSourceProperty =
            DependencyProperty.Register("GrayImageSource", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));


        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (this.IsEnabled && ImageSource != null) {
                innerImage.Source = ImageSource;
            } else if (!this.IsEnabled && GrayImageSource != null) {
                innerImage.Source = GrayImageSource;
            }
        }
    }
}
