using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Imaging;
using PixelPicker.Helpers;
using System.Windows;
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
        [ExpectedException(typeof(ArgumentException))]
        public void TestWrongRedArgument()
        {
            var color = System.Drawing.Color.Red;
            var newColor = new PixelColor(255, 257, Colors.Red.G, Colors.Red.B);

         
        }
    }
}
