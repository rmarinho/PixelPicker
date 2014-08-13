using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelPicker.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the color of the pixel.
        /// </summary>
        /// <param name="bitmapSource">The bitmap source.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>Color</returns>
        /// <exception cref="PixelPicker.Helpers.OutOfBoundsException"></exception>
        public static Color GetPixelColor(this BitmapImage bitmapSource, int x, int y)
        {
            if (bitmapSource == null)
                throw new Exception("No BitmapImage");

            if (x < 0 || x > bitmapSource.PixelWidth - 1 || y < 0 || y > bitmapSource.PixelHeight - 1)
                throw new OutOfBoundsException();

            var cb = new CroppedBitmap(bitmapSource, new Int32Rect(x, y, 1, 1));

            byte[] pixels = new byte[4];
            cb.CopyPixels(pixels, 4, 0);

            cb = null;

            var a = pixels[3];
            var r = pixels[2];
            var g = pixels[1];
            var b = pixels[0];
            return Color.FromArgb(a, r, g, b);
        }
    }




}
