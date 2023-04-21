namespace GestureRecognizerView.MAUI;

public class PointerInfo
{
    public uint PointerId { get; internal set; } = 0;
    public Point StartPoint { get; internal set; }
    public Point PreviewsPoint { get; internal set; }
    public Point EndPoint { get; internal set; }
    public DateTime StartTime { get; internal set; }
    public DateTime PreviewsTime{ get; internal set; }
    public DateTime EndTime { get; internal set; }
    public bool Pressed { get; internal set; }
    internal GestureRecognizerView.GestureType State1 { get; set; }
    internal GestureRecognizerView.GestureType State2 { get; set; }

}
