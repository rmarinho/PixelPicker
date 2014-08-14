using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Imaging;
using PixelPicker.Helpers;
using System.Windows;
namespace PixelPicker.Test
{
    [TestClass]
    public class GetPixelExtensionTest
    {
        [AssemblyInitialize]
        public static void InitializeTestAssembly(TestContext ctx)
        {
            if (Application.Current == null)
                new Application();
        }
        const string DefaultImageUrl = "default.png";
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
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
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
            Assert.IsTrue(color.R == r, "Red is not good");
            Assert.IsTrue(color.G == g, "Green is not good");
            Assert.IsTrue(color.B == b, "Blue is not good");
            Assert.IsTrue(color.A == a, "Alpha is not good");
        }

        [DeploymentItem(DefaultImageUrl)]
        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsException))]
        public void GetPixelOutOfBoundsX()
        {
            int x = -1;
            int y = 0;
            BitmapImage bmp = new BitmapImage(new Uri("default.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
        }

        [DeploymentItem(DefaultImageUrl)]
        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsException))]
        public void GetPixelOutOfBoundsY()
        {
            int x = 0;
            int y = -1;
            BitmapImage bmp = new BitmapImage(new Uri("default.png", UriKind.RelativeOrAbsolute));
            var color = bmp.GetPixelColor(x, y);
        }
    }
}
