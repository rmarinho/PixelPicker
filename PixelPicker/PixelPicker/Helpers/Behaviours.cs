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
    /// <summary>
    /// A Behavior that invokes a command when MouseDown event is fired and passes the Color of the clicked pixed
    /// </summary>
    public class GetPixelCommandBehavior : Behavior<Image>
    {
        Color _defaultColor = Colors.Transparent;

        protected override void OnAttached()
        {
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;

            base.OnAttached();
        }
   
        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;

            base.OnDetaching();
        }

        void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if someone is listening
            if (GetPixelCommand != null)
            {
                var color = ProcessMouseEvent(e);
                GetPixelCommand.Execute(color);
            }
        }

        Color ProcessMouseEvent(MouseEventArgs e)
        {
            BitmapImage bitmapSource = AssociatedObject.Source as BitmapImage;

            if (bitmapSource != null)
            {
                var scaleX = bitmapSource.PixelWidth / AssociatedObject.ActualWidth;
                var x = (int)(e.GetPosition(AssociatedObject).X * scaleX);
                var scaleY = bitmapSource.PixelHeight / AssociatedObject.ActualHeight;
                var y = (int)(e.GetPosition(AssociatedObject).Y * scaleY);
                if (x < 0 || x > bitmapSource.PixelWidth - 1 || y < 0 || y > bitmapSource.PixelHeight - 1)
                    return _defaultColor;
                return bitmapSource.GetPixelColor(x, y);

            }
            return _defaultColor;

        }

        public ICommand GetPixelCommand
        {
            get { return (ICommand)GetValue(GetPixelCommanddProperty); }
            set { SetValue(GetPixelCommanddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetPixelCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetPixelCommanddProperty =
            DependencyProperty.Register("GetPixelCommand", typeof(ICommand), typeof(GetPixelCommandBehavior), new PropertyMetadata(null));

    }
}
