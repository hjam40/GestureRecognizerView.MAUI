namespace GestureRecognizerView.MAUI;

public class TapGestureEventArgs
{
    public GestureRecognizerStatus Status { get; internal set; }
    public PointerInfo Pointer { get; internal set; }
    public double X { get => Pointer.EndPoint.X; }
    public double Y { get => Pointer.EndPoint.Y; }
}
