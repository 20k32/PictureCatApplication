using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;
using System.Windows.Threading;

namespace PictureCat
{
    public class FlexWrapPanel : WrapPanel
    {
        private Thickness childMargin = new Thickness(50);
        public double RequestedItemWidth
        {
            get { return (double)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(double), typeof(FlexWrapPanel), new PropertyMetadata(double.NaN));

        protected override Size MeasureOverride(Size constraint)
        {
            if (!double.IsNaN(RequestedItemWidth))
            {
                double requestedWidth = RequestedItemWidth;
                double panelWidth = constraint.Width;
                if (requestedWidth > panelWidth)
                {
                    requestedWidth = panelWidth;
                }
                foreach (UIElement childElement in InternalChildren)
                {
                    double newWidth = requestedWidth - childMargin.Left - childMargin.Right;
                    if (newWidth < 0)
                    {
                        newWidth = 0;
                    }
                    childElement.SetValue(WidthProperty, newWidth);
                }
            }
            //GC.Collect();
            return base.MeasureOverride(constraint);
        }
    }
}
