using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Intime.OPC.CustomControls
{
    /// <summary>
    ///     ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : Button
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof (ImageSource), typeof (ImageButton),
                new UIPropertyMetadata(null));

        public static readonly DependencyProperty GrayImageSourceProperty =
            DependencyProperty.Register("GrayImageSource", typeof (ImageSource), typeof (ImageButton),
                new UIPropertyMetadata(null));

        public ImageButton()
        {
            InitializeComponent();

            Style = FindResource("ImageButtonStyle") as Style;
            IsEnabledChanged += ImageButton_IsEnabledChanged;
        }


        public ImageSource ImageSource
        {
            get { return (ImageSource) GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...


        public ImageSource GrayImageSource
        {
            get { return (ImageSource) GetValue(GrayImageSourceProperty); }
            set { SetValue(GrayImageSourceProperty, value); }
        }

        private void ImageButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEnabled && ImageSource != null)
            {
                innerImage.Source = ImageSource;
            }
            else if (!IsEnabled && GrayImageSource != null)
            {
                innerImage.Source = GrayImageSource;
            }
        }

        // Using a DependencyProperty as the backing store for GrayImageSource.  This enables animation, styling, binding, etc...


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (IsEnabled && ImageSource != null)
            {
                innerImage.Source = ImageSource;
            }
            else if (!IsEnabled && GrayImageSource != null)
            {
                innerImage.Source = GrayImageSource;
            }
        }
    }
}