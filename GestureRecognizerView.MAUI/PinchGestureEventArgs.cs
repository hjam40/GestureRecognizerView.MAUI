namespace GestureRecognizerView.MAUI;

public class PinchGestureEventArgs
{
    public GestureRecognizerStatus Status { get; internal set; }
    public List<PointerInfo> Pointers { get; internal set; }
    public double Scale { get; internal set; }
    public double ScaleIncrement { get; internal set; }
}
