using System.Diagnostics;

namespace GestureRecognizerView.MAUI;

public class GestureRecognizerView : View
{
    public static readonly BindableProperty SelfProperty = BindableProperty.Create(nameof(Self), typeof(GestureRecognizerView), typeof(GestureRecognizerView), null);

    public List<PointerInfo> ActivePointers { get; private set; } = new List<PointerInfo>();
    /// <summary>
    /// Binding property for use this control in MVVM.
    /// </summary>
    public GestureRecognizerView Self
    {
        get { return (GestureRecognizerView)GetValue(SelfProperty); }
        set { SetValue(SelfProperty, value); }
    }
    public delegate void TapGestureHandler(object sender, TapGestureEventArgs args);
    public event TapGestureHandler TapGestureListener;
    public delegate void PanGestureHandler(object sender, PanGestureEventArgs args);
    public event PanGestureHandler PanGestureListener;
    public delegate void PinchGestureHandler(object sender, PinchGestureEventArgs args);
    public event PinchGestureHandler PinchGestureListener;
    public delegate void PointerGestureHandler(object sender, PointerGestureEventArgs args);
    public event PointerGestureHandler PointerGestureListener;
    private bool tapping = false;
    private bool panning = false;
    private bool pinching = false;
    private double panTotalX = 0, panTotalY = 0;
    private Point panEndPoint = new(-100, -100);

    public GestureRecognizerView()
    {
        Background = Brush.Transparent;
        Self = this;
    }
    internal void UpdateGestures(PointerInfo pointer)
    {
        var p = ActivePointers.FirstOrDefault(p => p.PointerId == pointer.PointerId);
        if (p != null) 
        {
            p.PreviewsTime = p.EndTime;
            p.EndTime = pointer.StartTime;
            p.PreviewsPoint = p.EndPoint;
            p.EndPoint = pointer.StartPoint;
            p.State1 = p.State2;
            p.State2 = pointer.State1;
        }
        else
        {
            p = pointer;
            p.EndTime = p.PreviewsTime = p.StartTime;
            p.EndPoint = p.PreviewsPoint = p.StartPoint;
            p.State2 = p.State1;
            ActivePointers.Add(p);
        }
        PointerGestureEventArgs pargs = new() { Pointer = p };
        TapGestureEventArgs targs = new() { Pointer = p };
        switch (pointer.State1)
        {
            case GestureType.Enter:
                pargs.Status = PointerRecognizerStatus.Enter;
                PointerGestureListener?.Invoke(this, pargs);
                break;
            case GestureType.Exit:
                pargs.Status = PointerRecognizerStatus.Exit;
                PointerGestureListener?.Invoke(this, pargs);
                if (tapping)
                {
                    tapping = false;
                    targs.Status = GestureRecognizerStatus.Cancel;
                    TapGestureListener?.Invoke(this, targs);
                }
                if (panning)
                {
                    panning = false;
                    PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Complete));
                }
                if (pinching)
                {
                    pinching = false;
                    PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Complete));
                }
                ActivePointers.Remove(p);
                break;
            case GestureType.Press:
                p.Pressed = true;
                pargs.Status = PointerRecognizerStatus.Press;
                PointerGestureListener?.Invoke(this, pargs);
                if (tapping)
                {
                    tapping = false;
                    targs.Status = GestureRecognizerStatus.Cancel;
                    TapGestureListener?.Invoke(this, targs);
                }
                else if (ActivePointers.Where(p => p.Pressed).Count() <= 1)
                {
                    tapping = true;
                    targs = new() { Status = GestureRecognizerStatus.Started, Pointer = p };
                    TapGestureListener?.Invoke(this, targs);
                }
                break;
            case GestureType.Release:
                p.Pressed = false;
                pargs.Status = PointerRecognizerStatus.Release;
                PointerGestureListener?.Invoke(this, pargs);
                if (tapping)
                {
                    tapping = false;
                    targs.Status = GestureRecognizerStatus.Complete;
                    TapGestureListener?.Invoke(this, targs);
                }
                p.Pressed = true;
                if (panning)
                {
                    if (ActivePointers.Where(p => p.Pressed).Count() <= 1)
                    {
                        panning = false;
                        PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Complete));
                    }else
                        PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Running));
                }
                if (pinching)
                {
                    if (ActivePointers.Where(p => p.Pressed).Count() <= 2)
                    {
                        pinching = false;
                        PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Complete));
                    }else
                        PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Running));
                }
                ActivePointers.Remove(p);
                break;
            case GestureType.Move:
                pargs.Status = PointerRecognizerStatus.Move;
                PointerGestureListener?.Invoke(this, pargs);
                if (tapping && (ActivePointers.Where(p => p.Pressed).Count() > 1 || (p.Pressed && p.StartPoint.Distance(p.EndPoint) > 5)))
                {
                    tapping = false;
                    targs.Status = GestureRecognizerStatus.Cancel;
                    TapGestureListener?.Invoke(this, targs);
                }
                if (IsPanMovement())
                {
                    if (panning)
                        PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Running));
                    else
                    {
                        panning = true;
                        panTotalX = panTotalY = 0;
                        PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Started));
                    }
                }
                if (IsPinchMovement())
                {
                    if (pinching)
                        PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Running));
                    else
                    {
                        pinching = true;
                        PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Started));
                    }
                }
                break;
            case GestureType.Cancel:
                pargs.Status = PointerRecognizerStatus.Cancel;
                PointerGestureListener?.Invoke(this, pargs);
                if (tapping)
                {
                    tapping = false;
                    targs.Status = GestureRecognizerStatus.Cancel;
                    TapGestureListener?.Invoke(this, targs);
                }
                if (panning)
                {
                    panning = false;
                    PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Cancel));
                }
                if (pinching)
                {
                    pinching = false;
                    PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Cancel));
                }
                ActivePointers.Remove(p);
                break;
        }
    }
    private bool IsPanMovement()
    {
        bool xSame = false;
        bool ySame = false;
        var pointers = ActivePointers.Where(p => p.Pressed).ToArray();
        if (pointers.Length > 0 && pointers[0].StartPoint.Distance(pointers[0].EndPoint) > 5)
        {
            double prevX = ActivePointers[0].EndPoint.X - pointers[0].PreviewsPoint.X;
            double prevY = ActivePointers[0].EndPoint.Y - pointers[0].PreviewsPoint.Y;
            foreach (var p in pointers)
            {
                double dirX = p.EndPoint.X - p.PreviewsPoint.X;
                double dirY = p.EndPoint.Y - p.PreviewsPoint.Y;
                xSame = (dirX > 0 && prevX > 0) || (dirX < 0 && prevX < 0);
                ySame = (dirY > 0 && prevY > 0) || (dirY < 0 && prevY < 0);
                if (!xSame && !ySame) break;
            }
        }
        return xSame || ySame;
    }
    private bool IsPinchMovement()
    {
        bool result = false;
        var pointers = ActivePointers.Where(p => p.Pressed).ToArray();
        if (pointers.Length > 1)
        {
            result = Math.Abs(pointers[0].EndPoint.Distance(pointers[1].EndPoint) - pointers[0].PreviewsPoint.Distance(pointers[1].PreviewsPoint)) > 0;
            Debug.WriteLine($"Pointers={pointers.Length} enddist={pointers[0].EndPoint.Distance(pointers[1].EndPoint)} prevdist={pointers[0].PreviewsPoint.Distance(pointers[1].PreviewsPoint)}");
        }
        return result;
    }
    private PinchGestureEventArgs CalculatePinchArgs(GestureRecognizerStatus status)
    {
        var pointers = ActivePointers.Where(p => p.Pressed).ToArray();
        var pointer1 = pointers[0];
        var pointer2 = pointers[1];
        var startDistance = pointer1.StartPoint.Distance(pointer2.StartPoint);
        var previewsDistance = pointer1.PreviewsPoint.Distance(pointer2.PreviewsPoint);
        var endDistance = pointer1.EndPoint.Distance(pointer2.EndPoint);
        PinchGestureEventArgs pargs = new()
        {
            Status = status,
            Pointers = ActivePointers,
            Scale = endDistance / startDistance,
            ScaleIncrement = (endDistance / startDistance) - (previewsDistance / startDistance)
        };

        return pargs;
    }
    private PanGestureEventArgs CalculePanArgs(GestureRecognizerStatus status)
    {
        var pointer = ActivePointers.Where(p => p.Pressed).First();
        double incX = 0, incY = 0;
        if (panEndPoint.Distance(pointer.EndPoint) != 0)
        {
            incX = pointer.EndPoint.X - pointer.PreviewsPoint.X;
            incY = pointer.EndPoint.Y - pointer.PreviewsPoint.Y;
            panTotalX += incX;
            panTotalY += incY;
            panEndPoint = pointer.EndPoint;
        }
        PanGestureEventArgs panargs = new()
        {
            Status = status,
            Pointers = ActivePointers,
            TotalX = panTotalX,
            TotalY = panTotalY,
            IncX = incX,
            IncY = incY
        };

        return panargs;
    }
    internal void UpdateMoveGestures(List<PointerInfo> pointers)
    {
        foreach (var pointer in pointers)
        {
            var p = ActivePointers.FirstOrDefault(p => p.PointerId == pointer.PointerId);
            if (p != null)
            {
                p.PreviewsTime = p.EndTime;
                p.EndTime = pointer.StartTime;
                p.PreviewsPoint = p.EndPoint;
                p.EndPoint = pointer.StartPoint;
                p.State1 = p.State2;
                p.State2 = pointer.State1;
            }
            else
            {
                p = pointer;
                p.EndTime = p.PreviewsTime = p.StartTime;
                p.EndPoint = p.PreviewsPoint = p.StartPoint;
                p.State2 = p.State1;
                ActivePointers.Add(p);
            }
            if (p.PreviewsPoint != p.EndPoint)
            {
                PointerGestureEventArgs pargs = new()
                {
                    Pointer = p,
                    Status = PointerRecognizerStatus.Move
                };
                PointerGestureListener?.Invoke(this, pargs);
            }
            if (tapping && (ActivePointers.Where(p => p.Pressed).Count() > 1 || (p.Pressed && p.StartPoint.Distance(p.EndPoint) > 5)))
            {
                tapping = false;
                TapGestureEventArgs targs = new() { Status = GestureRecognizerStatus.Cancel, Pointer = p };
                TapGestureListener?.Invoke(this, targs);
            }
        }
        if (IsPanMovement())
        {
            if (panning)
                PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Running));
            else
            {
                panning = true;
                panTotalX = panTotalY = 0;
                PanGestureListener?.Invoke(this, CalculePanArgs(GestureRecognizerStatus.Started));
            }
        }
        if (IsPinchMovement())
        {
            if (pinching)
                PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Running));
            else
            {
                pinching = true;
                PinchGestureListener?.Invoke(this, CalculatePinchArgs(GestureRecognizerStatus.Started));
            }
        }
    }

    internal enum GestureType
    {
        Press,
        Release,
        Move,
        Enter,
        Exit,
        Cancel,
        None
    }
}