using Microsoft.VisualStudio.TestTools.UnitTesting;
using PixelPicker.Helpers;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
namespace PixelPicker.Test
{
    [TestClass]
    public class GetPixelExtensionTest
    {
              const string DefaultImageUrl = "default.png";

        [AssemblyInitialize]
        public static void InitializeTestAssembly(TestContext ctx)
        {
            if (Application.Current == null)
                new Application();
        }


        [DeploymentItem("testimage.png")]
        [TestMethod]
        public void GetPixelColorRedCorner()
        {
            int x = 0;
            int y = 1;
            int a = 255;
            int r = 255;
            int g = 0;
            int b = 0;

            BitmapImage bmp = new BitmapImage(new Uri("testimage.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
            var pixColor = bmp.GetPixelColorUsingPixelColor(x, y);
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
        }

        [DeploymentItem("testimage.png")]
        [TestMethod]
        public void GetPixelColorBlackCorner()
        {
            int x = 0;
            int y = 0;
            int a = 255;
            int r = 0;
            int g = 0;
            int b = 0;

            BitmapImage bmp = new BitmapImage(new Uri("testimage.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
            var pixColor = bmp.GetPixelColorUsingPixelColor(x, y);
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
        }

        [DeploymentItem("testimage.png")]
        [TestMethod]
        public void GetPixelColorBlueCorner()
        {
            int x = 1;
            int y = 0;
            int a = 255;
            int r = 0;
            int g = 0;
            int b = 255;

            BitmapImage bmp = new BitmapImage(new Uri("testimage.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
            var pixColor = bmp.GetPixelColorUsingPixelColor(x, y);
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
        }

        [DeploymentItem("testimage.png")]
        [TestMethod]
        public void GetPixelColorGreenCorner()
        {
            int x = 1;
            int y = 1;
            int a = 255;
            int r = 0;
            int g = 255;
            int b = 0;

            BitmapImage bmp = new BitmapImage(new Uri("testimage.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
            var pixColor = bmp.GetPixelColorUsingPixelColor(x, y);
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
        }

        [DeploymentItem(DefaultImageUrl)]
        [TestMethod]
        public void GetPixelColorTransparent()
        {
            int x = 0;
            int y = 0;
            int a = 0;
            int r = 255;
            int g = 255;
            int b = 255;

            BitmapImage bmp = new BitmapImage(new Uri("default.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
            var pixColor = bmp.GetPixelColorUsingPixelColor(x, y);
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");

            Assert.IsTrue(pixColor.Red == r, "Red PixelColor is not good");
            Assert.IsTrue(pixColor.Green == g, "Green PixelColor is not good");
            Assert.IsTrue(pixColor.Blue == b, "Blue PixelColor is not good");
            Assert.IsTrue(pixColor.Alpha == a, "Alpha PixelColor is not good");
        }

        [DeploymentItem(DefaultImageUrl)]
        [TestMethod]
        public void GetPixelWithColor()
        {
            int x = 65;
            int y = 43;
            int a = 255;
            int r = 179;
            int g = 47;
            int b = 206;

            BitmapImage bmp = new BitmapImage(new Uri("default.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
            var pixColor = bmp.GetPixelColorUsingPixelColor(x, y);
           
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
            Assert.IsTrue(pixColor.Red == r, "Red PixelColor is not good");
            Assert.IsTrue(pixColor.Green == g, "Green PixelColor is not good");
            Assert.IsTrue(pixColor.Blue == b, "Blue PixelColor is not good");
            Assert.IsTrue(pixColor.Alpha == a, "Alpha PixelColor is not good");
        }

        [DeploymentItem(DefaultImageUrl)]
        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsException))]
        public void GetPixelOutOfBoundsX()
        {
            int x = -1;
            int y = 0;
            BitmapImage bmp = new BitmapImage(new Uri("default.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColorUsingPixelColor(x, y);
        }

        [DeploymentItem(DefaultImageUrl)]
        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsException))]
        public void GetPixelOutOfBoundsY()
        {
            int x = 0;
            int y = -1;
            BitmapImage bmp = new BitmapImage(new Uri("default.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColorUsingPixelColor(x, y);
        }
    }
}
