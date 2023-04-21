namespace GestureRecognizerView.MAUI;

public class PointerGestureEventArgs
{
    public PointerRecognizerStatus Status { get; internal set; }
    public PointerInfo Pointer { get; internal set; }
    public double X { get => Pointer.EndPoint.X; }
    public double Y { get => Pointer.EndPoint.Y; }
    public bool Pressed { get => Pointer.Pressed; }
}
