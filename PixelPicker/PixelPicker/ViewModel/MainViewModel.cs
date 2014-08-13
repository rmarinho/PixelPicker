using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelPicker.ViewModel
{
    internal class MainViewModel : ViewModel
    {
        const string DefaultImageUrl = "default.png";
        public MainViewModel()
        {
            CurrentColor = new SolidColorBrush(Colors.Red);
            //   Image = new BitmapImage(new Uri("http://xamarin.com/guide/img/xs-icon.png", UriKind.RelativeOrAbsolute));
            CurrentImage = new BitmapImage(new Uri(DefaultImageUrl, UriKind.RelativeOrAbsolute));
        }


        private BitmapImage _currentImage;

        public BitmapImage CurrentImage
        {
            get { return _currentImage; }
            set
            {
                _currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        private SolidColorBrush _currentColor;

        public SolidColorBrush CurrentColor
        {
            get { return _currentColor; }
            set
            {
                _currentColor = value;
                OnPropertyChanged("CurrentColor");
            }
        }


        private ObservableCollection<string> _imageUrls = new ObservableCollection<string>();

        public ObservableCollection<string> ImageUrls
        {
            get { return _imageUrls; }
            set
            {
                _imageUrls = value;
                OnPropertyChanged("ImageUrls");
            }
        }

    }
}
