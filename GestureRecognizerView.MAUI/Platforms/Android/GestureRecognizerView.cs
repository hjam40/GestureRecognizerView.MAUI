using View = Android.Views.View;
using MauiView = GestureRecognizerView.MAUI.GestureRecognizerView;
using Android.Content;
using MotionEventActions = Android.Views.MotionEventActions;
using Android.Views;
using Android.Print;

namespace GestureRecognizerView.MAUI.Android;
internal class GestureRecognizerView : View
{
    private readonly MauiView mauiView;
    public GestureRecognizerView(Context context, MauiView mauiView) : base(context)
    {
        this.mauiView = mauiView;
        //this.Touch += GestureRecognizerView_Touch;
    }

    public override bool OnTouchEvent(MotionEvent e)
    {
        uint id = (uint)e.GetPointerId(e.ActionIndex);
        float scale = Width / (float)mauiView.Width;
        var x = e.GetX(e.ActionIndex) / scale;
        var y = e.GetY(e.ActionIndex) / scale;
        MotionEvent.PointerProperties properties = new();
        
        e.GetPointerProperties(e.ActionIndex, properties);
        PointerInfo pointerInfo = new()
        {
            PointerId = id,
            StartTime = DateTime.Now,
            StartPoint = new Point(x, y),
            StartPressure = e.Pressure,
            PointerType = e.GetToolType(e.ActionIndex) switch
            {
                MotionEventToolType.Finger => PointerType.Touch,
                MotionEventToolType.Stylus => PointerType.Pencil,
                MotionEventToolType.Mouse => PointerType.Mouse,
                _ => PointerType.Other
            }
        };
        if (pointerInfo.PointerType == PointerType.Mouse)
        {
            pointerInfo.MouseWheelDelta = 0;
            pointerInfo.IsHorizontalMouseWheel = false;
            pointerInfo.IsLeftButtonPressed = e.IsButtonPressed(MotionEventButtonState.Primary);
            pointerInfo.IsRightButtonPressed = e.IsButtonPressed(MotionEventButtonState.Secondary);
            pointerInfo.IsMiddleButtonPressed = e.IsButtonPressed(MotionEventButtonState.Tertiary);
        }
        switch (e.ActionMasked)
        {
            case MotionEventActions.Down:
            case MotionEventActions.Pointer1Down:
            case MotionEventActions.Pointer2Down:
            case MotionEventActions.Pointer3Down:
                pointerInfo.State1 = MauiView.GestureType.Press;
                pointerInfo.Pressed = true;
                mauiView.UpdateGestures(pointerInfo);
                break;
            case MotionEventActions.Move:
            case MotionEventActions.HoverMove:
                if (e.PointerCount > 1)
                {
                    var pointers = new List<PointerInfo>();
                    for (int i = 0; i < e.PointerCount; i++) 
                    {
                        id = (uint)e.GetPointerId(i);
                        x = e.GetX((int)id) / scale;
                        y = e.GetY((int)id) / scale;
                        pointers.Add(new PointerInfo
                        {
                            PointerId = id,
                            StartTime = DateTime.Now,
                            StartPoint = new Point(x, y),
                            State1 = MauiView.GestureType.Move
                        });
                    }
                    mauiView.UpdateMoveGestures(pointers);
                }
                else
                {
                    pointerInfo.State1 = MauiView.GestureType.Move;
                    mauiView.UpdateGestures(pointerInfo);
                }
                break;
            case MotionEventActions.Up:
            case MotionEventActions.Pointer1Up:
            case MotionEventActions.Pointer2Up:
            case MotionEventActions.Pointer3Up:
                pointerInfo.State1 = MauiView.GestureType.Release;
                pointerInfo.Pressed = false;
                mauiView.UpdateGestures(pointerInfo);
                break;
            case MotionEventActions.HoverEnter:
                pointerInfo.State1 = MauiView.GestureType.Enter;
                mauiView.UpdateGestures(pointerInfo);
                break;
            case MotionEventActions.HoverExit:
                pointerInfo.State1 = MauiView.GestureType.Exit;
                mauiView.UpdateGestures(pointerInfo);
                break;
            case MotionEventActions.Cancel:
                pointerInfo.State1 = MauiView.GestureType.Cancel;
                mauiView.UpdateGestures(pointerInfo);
                break;
            case MotionEventActions.Scroll:
                if (pointerInfo.PointerType == PointerType.Mouse)
                {
                    float scroll = e.GetAxisValue(MotionEvent.AxisFromString("AXIS_VSCROLL"));
                    if (scroll == 0)
                    {
                        scroll = e.GetAxisValue(MotionEvent.AxisFromString("AXIS_HSCROLL"));
                        if (scroll != 0)
                            pointerInfo.IsHorizontalMouseWheel = true;
                    }
                    pointerInfo.State1 = MauiView.GestureType.Wheel;
                    pointerInfo.MouseWheelDelta = scroll;
                    mauiView.UpdateGestures(pointerInfo);
                }
                break;
        }
        return true;
    }
}
