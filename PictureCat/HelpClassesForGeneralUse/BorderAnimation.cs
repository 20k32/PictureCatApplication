using System;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace PictureCat
{
    public static class BorderAnimation
    {
        public static void DoReverseAnimation(EventHandler OnCompleted, Border currentBorder)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = Colors.Khaki;
            colorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            colorAnimation.AutoReverse = true;
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(colorAnimation);
            storyboard.Completed += OnCompleted!;
            Storyboard.SetTarget(colorAnimation, currentBorder);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));
            storyboard.Begin(currentBorder);
        }
    }
}
