using System;
using System.Globalization;
using System.Windows.Media;
using PixelPicker;

namespace PixelPicker.Helpers
{
    /// <summary>
    /// Our custom Pixel Color , supports RGB, HSL and HEX values
    /// </summary>
    public struct PixelColor
    {
        public static readonly PixelColor Empty;
        public static readonly PixelColor Transparent;

        static PixelColor()
        {
            Empty = new PixelColor();
            Transparent = new PixelColor(0, 255, 255, 255);
        }

        private int r;
        private int a;
        private int g;
        private int b;
        private double h;
        private double s;
        private double l;

        public int Red { get { return r; } }
        public int Alpha { get { return a; } }
        public int Green { get { return g; } }
        public int Blue { get { return b; } }

        public double Hue { get { return h; } }
        public double Saturation { get { return s; } }
        public double Luminosity { get { return l; } }



        /// <summary>
        /// Pixel Color as a System Color 
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelColor"/> struct from a System.Windows.Color
        /// </summary>
        /// <param name="c">The c.</param>
        public PixelColor(Color c)
            : this(c.A, c.R, c.G, c.B)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelColor"/> struct.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        public PixelColor(int red, int green, int blue)
            : this(255,red,green,blue)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelColor"/> struct.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        public PixelColor(int alpha, int red, int green, int blue)
            : this()
        {

            this.a = Check(alpha, PixelPicker.Alpha);
            this.r = Check(red, PixelPicker.Red);
            this.g = Check(green, PixelPicker.Green);
            this.b = Check(blue, PixelPicker.Blue);
            UpdateHSL();
        }

        /// <summary>
        /// Get PixelColor from  RGB.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <returns></returns>
        public static PixelColor FromRGB(int red, int green, int blue)
        {
            return new PixelColor(255, red, green, blue);
        }

      
        /// <summary>
        /// Froms the HSL.
        /// </summary>
        /// <param name="alpha">The alpha value is between 0 -255</param>
        /// <param name="hue">The hue in degrees a value between 0 - 360</param>
        /// <param name="saturation">The saturation in percentage between 0 - 1</param>
        /// <param name="luminosity">The luminosity in percentage between 0 - 1</param>
        /// <returns></returns>
        public static PixelColor FromHSL(int alpha, double hue, double saturation, double luminosity)
        {
            int red;
            int green;
            int blue;
            int alpha1;
            ConvertHSLtoRGB(alpha, (float)hue, (float)saturation, (float)luminosity, out alpha1, out red, out green, out blue);
            return new PixelColor(alpha1, red, green, blue);
        }
     
        /// <summary>
        /// Get a PixelColor from the hexadecimal representation.
        /// </summary>
        /// <param name="hex">Color representation in hexadecimal notation.</param>
        /// <returns></returns>
        public static PixelColor FromHex(string hex)
        {
            int red;
            int green;
            int blue;
            int alpha;
            ConvertHEXToRGB(hex, out alpha, out red, out green, out blue);
            return new PixelColor(alpha, red, green, blue);
        }

        /// <summary>
        /// Gets the pallete.
        /// </summary>
        /// <param name="ncolors">The ncolors.</param>
        /// <param name="factor">The factor.</param>
        /// <returns></returns>
        public PixelColor[] GetPallete(int ncolors, double factor)
        {
            var array = new PixelColor[ncolors];
        
            for (var i = 0; i < ncolors; i ++)
            {
                var newL = Extensions.Clamp<double>(Math.Max(0, (this.GetLuminosity() - (factor*i*0.05) )),1,0);
                array[i] = PixelColor.FromHSL(this.a, this.GetHue(), this.GetSaturation(), newL );
            }
            return array;
        }

        private static void ConvertHSLtoRGB(int a, float h, float s, float l, out int alpha, out int red, out int green, out int blue)
        {

            if (0 > a || 255 < a)
            {
                throw new ArgumentOutOfRangeException("a", a, string.Format(PixelPicker.ArgumentOutOfBoundsException, PixelPicker.Alpha));
            }
            if (0f > h || 360f < h)
            {
                throw new ArgumentOutOfRangeException("h", h, string.Format(PixelPicker.ArgumentOutOfBoundsException, PixelPicker.Hue));
            }
            if (0f > s || 1f < s)
            {
                throw new ArgumentOutOfRangeException("s", s, string.Format(PixelPicker.ArgumentOutOfBoundsException, PixelPicker.Saturation));
            }
            if (0f > l || 1f < l)
            {
                throw new ArgumentOutOfRangeException("b", l, string.Format(PixelPicker.ArgumentOutOfBoundsException, PixelPicker.Luminosity));
            }

            alpha = a;

            if (0 == s)
            {
                red = blue = green = Convert.ToInt32(l * 255);
            }

            double fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;

            if (0.5 < l)
            {
                fMax = l - (l * s) + s;
                fMin = l + (l * s) - s;
            }
            else
            {
                fMax = l + (l * s);
                fMin = l - (l * s);
            }

            iSextant = (int)Math.Floor(h / 60f);
            if (300f <= h)
            {
                h -= 360f;
            }
            h /= 60f;
            h -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2)
            {
                fMid = h * (fMax - fMin) + fMin;
            }
            else
            {
                fMid = fMin - h * (fMax - fMin);
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            switch (iSextant)
            {
                case 1:
                    red = iMid;
                    green = iMax;
                    blue = iMid;
                    break;
                case 2:
                    red = iMin;
                    green = iMax;
                    blue = iMid;
                    break;
                case 3:
                    red = iMin;
                    green = iMid;
                    blue = iMax;
                    break;
                case 4:
                    red = iMid;
                    green = iMin;
                    blue = iMax;
                    break;
                case 5:
                    red = iMax;
                    green = iMin;
                    blue = iMid;
                    break;
                default:
                    red = iMax;
                    green = iMid;
                    blue = iMin;
                    break;
            }
        }

        private static void ConvertHEXToRGB(string hexColor, out int alpha, out int red, out int green, out int blue)
        {
            if (hexColor.IndexOf('#') != 0)
                throw new ArgumentException(PixelPicker.ArgumentExceptionHex, PixelPicker.HexColor);

            red = 0;
            green = 0;
            blue = 0;
            alpha = 255;

            hexColor = hexColor.Replace("#", "");

            if (hexColor.Length == 6)
            {
                red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 8)
            {
                alpha = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                red = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);
            }
            else
                throw new ArgumentException(PixelPicker.ArgumentExceptionHex, PixelPicker.HexColor);
        }

        private float GetLuminosity()
        {
            float num = ((float)this.Red) / 255f;
            float num2 = ((float)this.Green) / 255f;
            float num3 = ((float)this.Blue) / 255f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            return ((num4 + num5) / 2f);
        }

        private float GetHue()
        {
            if ((this.Red == this.Green) && (this.Green == this.Blue))
            {
                return 0f;
            }
            float num = ((float)this.Red) / 255f;
            float num2 = ((float)this.Green) / 255f;
            float num3 = ((float)this.Blue) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            float num6 = num4 - num5;
            if (num == num4)
            {
                num7 = (num2 - num3) / num6;
            }
            else if (num2 == num4)
            {
                num7 = 2f + ((num3 - num) / num6);
            }
            else if (num3 == num4)
            {
                num7 = 4f + ((num - num2) / num6);
            }
            num7 *= 60f;
            if (num7 < 0f)
            {
                num7 += 360f;
            }
            return num7;
        }

        private float GetSaturation()
        {
            float num = ((float)this.Red) / 255f;
            float num2 = ((float)this.Green) / 255f;
            float num3 = ((float)this.Blue) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            if (num4 == num5)
            {
                return num7;
            }
            float num6 = (num4 + num5) / 2f;
            if (num6 <= 0.5)
            {
                return ((num4 - num5) / (num4 + num5));
            }
            return ((num4 - num5) / ((2f - num4) - num5));
        }

        private static int Check(int property, string propertyName)
        {
            if (property > 255 || property < 0)
                throw new ArgumentOutOfRangeException(string.Format(PixelPicker.ArgumentOutOfBoundsException , propertyName));
            return property;
        }

        private void UpdateHSL()
        {
            h = GetHue();
            l = GetLuminosity();
            s = GetSaturation();
        }


        // Conversion from Color to PixelColor
        public static implicit operator PixelColor(Color c)
        {
            return new PixelColor(c.A, c.R, c.G, c.B);
        }

        // Conversion from PixelColor to Color
        public static implicit operator Color(PixelColor c)
        {
            return c.ToColor();
        }
    }
}
