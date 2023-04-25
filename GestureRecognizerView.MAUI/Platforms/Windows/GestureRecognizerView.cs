using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using MauiView = GestureRecognizerView.MAUI.GestureRecognizerView;
using Color = Windows.UI.Color;
using Microsoft.UI.Input;

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
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Wheel,
            PointerType = PointerType.Mouse,
            StartPressure = point.Properties.Pressure,
            MouseWheelDelta = point.Properties.MouseWheelDelta,
            IsHorizontalMouseWheel = point.Properties.IsHorizontalMouseWheel,
            IsLeftButtonPressed = point.Properties.IsLeftButtonPressed,
            IsRightButtonPressed = point.Properties.IsRightButtonPressed,
            IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed
        };
        mauiView.UpdateGestures(pointerInfo);
    }

    private void GestureRecognizerView_PointerEntered(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        PointerInfo pointerInfo = new()
        {
            PointerId = point.PointerId,
            StartTime = DateTime.Now,
            StartPoint = new Point(point.Position.X, point.Position.Y),
            State1 = MauiView.GestureType.Enter,
            StartPressure = point.Properties.Pressure,
            PointerType = point.PointerDeviceType switch
            {
                PointerDeviceType.Touch => PointerType.Touch,
                PointerDeviceType.Pen => PointerType.Pencil,
                PointerDeviceType.Mouse => PointerType.Mouse,
                PointerDeviceType.Touchpad => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = point.Properties.IsLeftButtonPressed;
            pointerInfo.IsRightButtonPressed = point.Properties.IsRightButtonPressed;
            pointerInfo.IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed;
        }
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
            State1 = MauiView.GestureType.Exit,
            StartPressure = point.Properties.Pressure,
            PointerType = point.PointerDeviceType switch
            {
                PointerDeviceType.Touch => PointerType.Touch,
                PointerDeviceType.Pen => PointerType.Pencil,
                PointerDeviceType.Mouse => PointerType.Mouse,
                PointerDeviceType.Touchpad => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = point.Properties.IsLeftButtonPressed;
            pointerInfo.IsRightButtonPressed = point.Properties.IsRightButtonPressed;
            pointerInfo.IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed;
        }
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
            State1 = MauiView.GestureType.Cancel,
            StartPressure = point.Properties.Pressure,
            PointerType = point.PointerDeviceType switch
            {
                PointerDeviceType.Touch => PointerType.Touch,
                PointerDeviceType.Pen => PointerType.Pencil,
                PointerDeviceType.Mouse => PointerType.Mouse,
                PointerDeviceType.Touchpad => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = point.Properties.IsLeftButtonPressed;
            pointerInfo.IsRightButtonPressed = point.Properties.IsRightButtonPressed;
            pointerInfo.IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed;
        }
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
            State1 = MauiView.GestureType.Move,
            StartPressure = point.Properties.Pressure,
            PointerType = point.PointerDeviceType switch
            {
                PointerDeviceType.Touch => PointerType.Touch,
                PointerDeviceType.Pen => PointerType.Pencil,
                PointerDeviceType.Mouse => PointerType.Mouse,
                PointerDeviceType.Touchpad => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = point.Properties.IsLeftButtonPressed;
            pointerInfo.IsRightButtonPressed = point.Properties.IsRightButtonPressed;
            pointerInfo.IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed;
        }
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
            StartPressure = point.Properties.Pressure,
            Pressed = false,
            PointerType = point.PointerDeviceType switch
            {
                PointerDeviceType.Touch => PointerType.Touch,
                PointerDeviceType.Pen => PointerType.Pencil,
                PointerDeviceType.Mouse => PointerType.Mouse,
                PointerDeviceType.Touchpad => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = point.Properties.IsLeftButtonPressed;
            pointerInfo.IsRightButtonPressed = point.Properties.IsRightButtonPressed;
            pointerInfo.IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed;
        }
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
            StartPressure = point.Properties.Pressure,
            Pressed = true,
            PointerType = point.PointerDeviceType switch
            {
                PointerDeviceType.Touch => PointerType.Touch,
                PointerDeviceType.Pen => PointerType.Pencil,
                PointerDeviceType.Mouse => PointerType.Mouse,
                PointerDeviceType.Touchpad => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = point.Properties.IsLeftButtonPressed;
            pointerInfo.IsRightButtonPressed = point.Properties.IsRightButtonPressed;
            pointerInfo.IsMiddleButtonPressed = point.Properties.IsMiddleButtonPressed;
        }
        mauiView.UpdateGestures(pointerInfo);
    }
}
