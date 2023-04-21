using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using MauiView = GestureRecognizerView.MAUI.GestureRecognizerView;
using Color = Windows.UI.Color;

namespace GestureRecognizerView.MAUI.Windows;

public class GestureRecognizerView : Panel
{
    private readonly MauiView mauiView;

    public GestureRecognizerView(MauiView mauiView)
    {
        this.mauiView = mauiView;
        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Stretch;
        Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        PointerPressed += GestureRecognizerView_PointerPressed;
        PointerReleased += GestureRecognizerView_PointerReleased;
        PointerMoved += GestureRecognizerView_PointerMoved;
        PointerCanceled += GestureRecognizerView_PointerCanceled;
        PointerExited += GestureRecognizerView_PointerExited;
        PointerEntered += GestureRecognizerView_PointerEntered;
        PointerWheelChanged += GestureRecognizerView_PointerWheelChanged;
    }

    private void GestureRecognizerView_PointerWheelChanged(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        //point.Properties.MouseWheelDelta;
        
    }

    private void GestureRecognizerView_PointerEntered(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Enter
        };
        mauiView.UpdateGestures(pointerInfo);
    }

    private void GestureRecognizerView_PointerExited(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Exit
        };
        mauiView.UpdateGestures(pointerInfo);
    }

    private void GestureRecognizerView_PointerCanceled(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Cancel
        };
        mauiView.UpdateGestures(pointerInfo);
    }

    private void GestureRecognizerView_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Move
        };
        mauiView.UpdateGestures(pointerInfo);
    }

    private void GestureRecognizerView_PointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Release,
            Pressed = false
        };
        mauiView.UpdateGestures(pointerInfo);
    }

    private void GestureRecognizerView_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Press,
            Pressed = true
        };
        mauiView.UpdateGestures(pointerInfo);
    }
}
