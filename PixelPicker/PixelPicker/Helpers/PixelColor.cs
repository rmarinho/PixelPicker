﻿using System;
using System.Globalization;
using System.Windows.Media;

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
        public PixelColor(Color c)
            : this(c.A, c.R, c.G, c.B)
        {
        }

        public PixelColor(int alpha, int red, int green, int blue)
            : this()
        {

            this.a = Check(alpha, "Alpha");
            this.r = Check(red, "Red");
            this.g = Check(green, "Green");
            this.b = Check(blue, "Blue");
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
        /// Get a Pixel the hexadecima.
        /// </summary>
        /// <param name="hex">The hexadecimal.</param>
        /// <returns></returns>
        public static PixelColor FromHex(string hex)
        {
            int red;
            int green;
            int blue;
            int alpha;
            ConvertFromHexToRGB(hex, out alpha, out red, out green, out blue);
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
            for (int i = 0; i < ncolors; i++)
            {
                int r = this.r;
                int g = this.g;
                int b = this.b;
                if (r > 150)
                {
                    r = (int)(this.r - (i * factor));
                    if (r < 0)
                        r = 0;
                }
                else if (g > 150)
                {
                    g = (int)(this.g - (i * factor));
                    if (g < 0)
                        g = 0;
                }
                else if (b > 150)
                {
                    b = (int)(this.b - (i * factor));
                    if (b < 0)
                        b = 0;
                }


                array[i] = new PixelColor(255, r, g, b);
            }
            return array;
        }
       
        private static void ConvertFromHexToRGB(string hexColor, out int alpha, out int red, out int green, out int blue)
        {
            if (hexColor.IndexOf('#') != 0)
                throw new ArgumentException("HEX color not in the correct format");
            
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
                throw new ArgumentException("HEX color not in the correct format");
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
                throw new ArgumentException(string.Format("The {0} is invalid", propertyName));
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
