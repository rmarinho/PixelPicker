using PixelPicker.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelPicker.ViewModel
{
    /// <summary>
    /// The mainView model that holds the properties and logic 
    /// </summary>
    internal class MainViewModel : ViewModel
    {
        #region Fields
        const string DefaultImageUrl = "default.png";
        static string pattern = Regex.Escape(@"");
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            var clr = Colors.Red;
            CurrentColor = new PixelColor(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);
            CurrentImage = new BitmapImage(new Uri(DefaultImageUrl, UriKind.RelativeOrAbsolute));
            ImageLinks = "http://xamarin.com/";
        }

        #region Private Methods
        /// <summary>
        /// Loads the page image links.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        async Task LoadPageImageLinks(string url)
        {
            if (string.IsNullOrEmpty(url) || !url.IsValidUrl())
                return;


            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                var imgResults = Regex.Matches(result, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase);
                if (imgResults.Count > 0)
                {
                    foreach (Match match in imgResults)
                        if (match.Groups.Count == 2)
                        {
                            CheckIsValidImage(url, match.Groups[1].Value.Trim());
                        }
                }
            }
        }

        /// <summary>
        /// Checks the image.
        /// </summary>
        /// <param name="baseUrl">The base url URL.</param>
        /// <param name="match">The match.</param>
        void CheckIsValidImage(string baseUrl, string imgUrl)
        {
            var extension = System.IO.Path.GetExtension(imgUrl).ToLowerInvariant();
            if (extension == ".jpg" || extension == ".png")
            {
                if (!imgUrl.StartsWith("http://") || !imgUrl.StartsWith("https://") || !imgUrl.StartsWith("www"))
                {
                    imgUrl = imgUrl.Insert(0, baseUrl);
                }
                if (imgUrl.IsValidUrl())
                    ImageUrls.Insert(0, imgUrl);
            }
        }

        /// <summary>
        /// Parses image links from the textbox.
        /// </summary>
        /// <param name="text">The text.</param>
        [Obsolete]
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

        /// <summary>
        /// Updates the color pallete.
        /// </summary>
        void UpdateColorPallete()
        {
            ColorPallete.Clear();
            foreach (var item in _currentColor.GetPallete(_numOfColorsInPallete, _luminosityOnPallete))
            {
                ColorPallete.Add(new SolidColorBrush(item));
            }

        }

        #endregion

        #region Properties

        private string _imageLinks;

        public string ImageLinks
        {
            get { return _imageLinks; }
            set
            {
                _imageLinks = value.Trim();
                OnPropertyChanged("ImageLinks");
                if (!string.IsNullOrEmpty(_imageLinks) && _imageLinks.IsValidUrl())
                    //fire and forget
                    LoadPageImageLinks(_imageLinks);

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
                if (!string.IsNullOrEmpty(_selectedImageUrl))
                    CurrentImage = new BitmapImage(new Uri(_selectedImageUrl, UriKind.RelativeOrAbsolute));
            }
        }


        public SolidColorBrush CurrentBrush
        {
            get { return new SolidColorBrush(_currentColor); }
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
                LuminosityOnPallete = _currentColor.Luminosity;
                UpdateColorPallete();
            }
        }

        private ObservableCollection<SolidColorBrush> _colorPallete = new ObservableCollection<SolidColorBrush>();

        public ObservableCollection<SolidColorBrush> ColorPallete
        {
            get { return _colorPallete; }
            set
            {
                _colorPallete = value;
                OnPropertyChanged("ColorPallete");
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

        private int _numOfColorsInPallete = 5;

        public int NumOfColorsInPallete
        {
            get { return _numOfColorsInPallete; }
            set
            {
                _numOfColorsInPallete = value;
                OnPropertyChanged("NumOfColorsInPallete");
                UpdateColorPallete();
            }
        }

        private double _luminosityOnPallete = 0.5;

        public double LuminosityOnPallete
        {
            get { return _luminosityOnPallete; }
            set
            {
                _luminosityOnPallete = value;
                OnPropertyChanged("LuminosityOnPallete");
                UpdateColorPallete();
            }
        }


        #endregion

        #region Commands
        private RelayCommand<PixelColor> _getPixelCommand = null;

        public RelayCommand<PixelColor> GetPixelCommand
        {
            get
            {
                return _getPixelCommand ?? new RelayCommand<PixelColor>(clr => CurrentColor = clr);

            }
        }

        #endregion

    }
}
