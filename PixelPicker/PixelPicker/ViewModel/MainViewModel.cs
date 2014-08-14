using PixelPicker.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelPicker.ViewModel
{
    /// <summary>
    /// The mainView model that holds the properties and logic 
    /// </summary>
    internal class MainViewModel : ViewModel
    {
        const string DefaultImageUrl = "default.png";

        public MainViewModel()
        {
            var clr = Colors.Red;
            CurrentColor = new PixelColor(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);
            CurrentImage = new BitmapImage(new Uri(DefaultImageUrl, UriKind.RelativeOrAbsolute));
            ImageLinks = @"https://raw.githubusercontent.com/XForms/XForms-Toolkit/master/screenshots/ios/buttons.png;
                           http://xamarin.com/guide/img/xs-icon.png;    http://xamarin.com/guide/img/xs-icon.png";
        }

        void ParseLinks(string text)
        {
            ImageUrls.Clear();
            var urls = text.Split(';');
            foreach (var item in urls)
            {
                if (item.IsValidUrl())
                    ImageUrls.Add(item);

            }
        }

        private string _imageLinks;

        public string ImageLinks
        {
            get { return _imageLinks; }
            set
            {
                _imageLinks = value;
                OnPropertyChanged("ImageLinks");
                if (!string.IsNullOrEmpty(_imageLinks))
                    ParseLinks(_imageLinks);
            }
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

        private string _selectedImageUrl;

        public string SelectedImageUrl
        {
            get { return _selectedImageUrl; }
            set
            {
                _selectedImageUrl = value;
                OnPropertyChanged("SelectedImageUrl");
                if(!string.IsNullOrEmpty(_selectedImageUrl))
                    CurrentImage = new BitmapImage(new Uri(_selectedImageUrl, UriKind.RelativeOrAbsolute));
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
