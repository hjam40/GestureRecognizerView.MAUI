#if IOS || MACCATALYST
using Foundation;
using UIKit;
using MauiView = GestureRecognizerView.MAUI.GestureRecognizerView;

namespace GestureRecognizerView.MAUI.Apple;
public class GestureRecognizerView : UIView
{
    private readonly MauiView mauiView;
    
    public GestureRecognizerView(MauiView mauiView)
    {
        this.mauiView = mauiView;
        MultipleTouchEnabled = true;
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);
        foreach (var touch in touches)
        {
            var t = touch as UITouch;
            t.GetHashCode();
            var location = t.LocationInView(this);
            PointerInfo pointerInfo = new()
            {
                PointerId = (uint)t.GetHashCode(),
                StartTime = DateTime.Now,
                StartPoint = new Point(location.X, location.Y),
                State1 = MauiView.GestureType.Press,
                StartPressure = (float)t.Force,
                PointerType = t.Type switch
                {
                    UITouchType.Direct => PointerType.Touch,
                    UITouchType.Stylus => PointerType.Pencil,
                    UITouchType.IndirectPointer => PointerType.Mouse,
                    _ => PointerType.Other
                }
            };
            mauiView.UpdateGestures(pointerInfo);
        }
    }
    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        base.TouchesMoved(touches, evt);
        foreach (var touch in touches)
        {
            var t = touch as UITouch;
            t.GetHashCode();
            var location = t.LocationInView(this);
            PointerInfo pointerInfo = new()
            {
                PointerId = (uint)t.GetHashCode(),
                StartTime = DateTime.Now,
                StartPoint = new Point(location.X, location.Y),
                State1 = MauiView.GestureType.Move,
                StartPressure = (float)t.Force,
                PointerType = t.Type switch
                {
                    UITouchType.Direct => PointerType.Touch,
                    UITouchType.Stylus => PointerType.Pencil,
                    UITouchType.IndirectPointer => PointerType.Mouse,
                    _ => PointerType.Other
                }
            };
            mauiView.UpdateGestures(pointerInfo);
        }
    }
    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);
        foreach (var touch in touches)
        {
            var t = touch as UITouch;
            t.GetHashCode();
            var location = t.LocationInView(this);
            PointerInfo pointerInfo = new()
            {
                PointerId = (uint)t.GetHashCode(),
                StartTime = DateTime.Now,
                StartPoint = new Point(location.X, location.Y),
                State1 = MauiView.GestureType.Release,
                StartPressure = (float)t.Force,
                PointerType = t.Type switch
                {
                    UITouchType.Direct => PointerType.Touch,
                    UITouchType.Stylus => PointerType.Pencil,
                    UITouchType.IndirectPointer => PointerType.Mouse,
                    _ => PointerType.Other
                }
            };
            mauiView.UpdateGestures(pointerInfo);
        }
    }
    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);
        foreach (var touch in touches)
        {
            var t = touch as UITouch;
            t.GetHashCode();
            var location = t.LocationInView(this);
            PointerInfo pointerInfo = new()
            {
                PointerId = (uint)t.GetHashCode(),
                StartTime = DateTime.Now,
                StartPoint = new Point(location.X, location.Y),
                State1 = MauiView.GestureType.Cancel,
                StartPressure = (float)t.Force,
                PointerType = t.Type switch
                {
                    UITouchType.Direct => PointerType.Touch,
                    UITouchType.Stylus => PointerType.Pencil,
                    UITouchType.IndirectPointer => PointerType.Mouse,
                    _ => PointerType.Other
                }
            };
            mauiView.UpdateGestures(pointerInfo);
        }
    }
}
#endif
