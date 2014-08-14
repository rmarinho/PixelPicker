using PixelPicker.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelPicker.ViewModel
{
    internal class MainViewModel : ViewModel
    {
        const string DefaultImageUrl = "default.png";

        public MainViewModel()
        {
            var clr = Colors.Red;
            CurrentColor = new PixelColor(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);
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

   
        public SolidColorBrush CurrentBrush
        {
            get { return new SolidColorBrush(_currentColor.ToNormalColor()); }
        }


        private PixelColor _currentColor;

        public PixelColor CurrentColor
        {
            get { return _currentColor; }
            set
            {
                _currentColor = value;
                OnPropertyChanged("CurrentColor");
                OnPropertyChanged("CurrentBrush");
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


        private RelayCommand<PixelColor> _getPixelCommand = null;

        public RelayCommand<PixelColor> GetPixelCommand
        {
            get
            {
                return _getPixelCommand ?? new RelayCommand<PixelColor>(clr => CurrentColor = clr);

            }
        }


    }
}
