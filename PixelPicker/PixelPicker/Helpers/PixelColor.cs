using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PixelPicker.Helpers
{

    /// <summary>
    /// Our custom Pixel Color , supports HSL values
    /// </summary>
    public struct PixelColor
    {
        public static readonly PixelColor Empty;


        static PixelColor()
        {
            Empty = new PixelColor();

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

        private void UpdateHSL()
        {
            h = GetHue();
            l = GetLuminosity();
            s = GetSaturation();
        }

        public PixelColor(Color c)
            : this(c.A, c.R, c.G, c.B)
        {
        }
        public PixelColor(int alpha, int red, int green, int blue)
            : this()
        {
            a = Check(alpha, "Alpha");
            r = Check(red, "Red");
            g = Check(green, "Green");
            b = Check(blue, "Blue");
            UpdateHSL();
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
                throw new ArgumentException("The {0} is invalid", propertyName);
            return property;
        }
    }
}
