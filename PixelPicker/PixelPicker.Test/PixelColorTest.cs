using Microsoft.VisualStudio.TestTools.UnitTesting;
using PixelPicker.Helpers;
using System;
using System.Windows.Media;
namespace PixelPicker.Test
{
    [TestClass]
    public class PixelColorTest
    {

        [TestMethod]
        public void TestRedColor()
        {
            var color = Colors.Red;
            var newColor = new PixelColor(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);

            Assert.IsTrue(color.R == newColor.Red, "Red is not good");
            Assert.IsTrue(color.G == newColor.Green, "Green is not good");
            Assert.IsTrue(color.B == newColor.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColor.Alpha, "Alpha is not good");
        }


        [TestMethod]
        public void TestRedColorHSL()
        {
            var color = System.Drawing.Color.Red;
            var newColor = new PixelColor(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);

            Assert.IsTrue(color.GetHue() == newColor.Hue, "Hue is not good");
            Assert.IsTrue(color.GetSaturation() == newColor.Saturation, "Saturation is not good");
            Assert.IsTrue(color.GetBrightness() == newColor.Luminosity, "Luminosity is not good");

        }

        [TestMethod]
        public void TestKnownTransparent()
        {
            var color = Colors.Transparent;
            var transparentColor = PixelColor.Transparent;

            Assert.IsTrue(color.R == transparentColor.Red, "Red is not good");
            Assert.IsTrue(color.G == transparentColor.Green, "Green is not good");
            Assert.IsTrue(color.B == transparentColor.Blue, "Blue is not good");
            Assert.IsTrue(color.A == transparentColor.Alpha, "Alpha is not good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWrongRedArgument()
        {
            var color = System.Drawing.Color.Red;
            var newColor = new PixelColor(255, 257, Colors.Red.G, Colors.Red.B);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWrongGreenArgument()
        {
            var color = System.Drawing.Color.Red;
            var newColor = new PixelColor(255, Colors.Red.R, 257, Colors.Red.B);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWrongBlueArgument()
        {
            var color = System.Drawing.Color.Red;
            var newColor = new PixelColor(255, Colors.Red.R, Colors.Red.G, 257);
        }

        [TestMethod]
        public void TestHexRedColor()
        {
            var color = Colors.Red;
            var newColorHex = PixelColor.FromHex("#FF0000");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        public void TestHexWhiteColor()
        {
            var color = Colors.White;
            var newColorHex = PixelColor.FromHex("#FFFFFF");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        public void TestHexWhiteWithTransparencyColor()
        {
            var color = Colors.White;
            color.A = 10;
            var newColorHex = PixelColor.FromHex("#0AFFFFFF");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestHexException()
        {
            var color = Colors.White;
            var newColorHex = PixelColor.FromHex("0AFFFFFF");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestHexException2()
        {
            var color = Colors.White;
            var newColorHex = PixelColor.FromHex("s");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        public void TestHexFullTransparent()
        {
            var color = Colors.Transparent;
            var newColorHex = PixelColor.FromHex("#00FFFFFF");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        public void TestOperatorColorToPixelColor()
        {
            var color = Colors.Transparent;
            PixelColor newColorHex = Colors.Transparent;

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }

        [TestMethod]
        public void TestOperatorPixelColorToColor()
        {
            var color = Colors.Transparent;
            Color newColorHex = PixelColor.Transparent;

            Assert.IsTrue(color.R == newColorHex.R, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.G, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.B, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.A, "Alpha is not good");
        }


        [TestMethod]
        public void TestHSLRedColor()
        {
            var color = Colors.Red;
            var newColorHex = PixelColor.FromHSL(255, 0, 1, 0.5);

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }


        [TestMethod]
        public void TestHSLGreenColor()
        {
            var color = Color.FromRgb(0,255,0);
            var newColorHex = PixelColor.FromHSL(255, 120, 1, 0.5);

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good");
        }


        [TestMethod]
        public void TestGreenColorAllFormats()
        {
            var color = Color.FromRgb(0, 255, 0);
            var newcolor = new PixelColor(0, 255, 0);
            var newColorHex = PixelColor.FromHex("#00FF00");
            var newColorHSL = PixelColor.FromHSL(255, 120, 1, 0.5);

            Assert.IsTrue(color.R == newcolor.Red, "Red is not good");
            Assert.IsTrue(color.G == newcolor.Green, "Green is not good");
            Assert.IsTrue(color.B == newcolor.Blue, "Blue is not good");
            Assert.IsTrue(color.A == newcolor.Alpha, "Alpha is not good");

            Assert.IsTrue(color.R == newColorHex.Red, "Red is not good in HEX");
            Assert.IsTrue(color.G == newColorHex.Green, "Green is not good in HEX");
            Assert.IsTrue(color.B == newColorHex.Blue, "Blue is not good in HEX");
            Assert.IsTrue(color.A == newColorHex.Alpha, "Alpha is not good in HEX");

            Assert.IsTrue(color.R == newColorHSL.Red, "Red is not good in HSL");
            Assert.IsTrue(color.G == newColorHSL.Green, "Green is not good in HSL");
            Assert.IsTrue(color.B == newColorHSL.Blue, "Blue is not good in HSL");
            Assert.IsTrue(color.A == newColorHSL.Alpha, "Alpha is not good in HSL");
        }
    }
}
