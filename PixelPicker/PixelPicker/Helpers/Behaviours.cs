using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PixelPicker.Helpers
{
    /// <summary>
    /// A Behavior that invokes a command when MouseDown event is fired and passes the MouseButtonEventArgs
    /// </summary>
    public class MouseDownToCommandBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;

            base.OnAttached();
        }

        void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseDownCommand != null)
                MouseDownCommand.Execute(e);

        }

        public ICommand MouseDownCommand
        {
            get { return (ICommand)GetValue(MouseDownCommandProperty); }
            set { SetValue(MouseDownCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseDownCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDownCommandProperty =
            DependencyProperty.Register("MouseDownCommand", typeof(ICommand), typeof(MouseDownToCommandBehavior), new PropertyMetadata(null));



        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;

            base.OnDetaching();
        }


    }
}
