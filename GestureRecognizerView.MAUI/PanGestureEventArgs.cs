namespace GestureRecognizerView.MAUI;

public class PanGestureEventArgs
{
    public GestureRecognizerStatus Status { get; internal set; }
    public List<PointerInfo> Pointers { get; internal set; }
    public double TotalX { get; internal set; }
    public double TotalY { get; internal set; }
    public double IncX { get; internal set; }
    public double IncY { get; internal set; }
}
