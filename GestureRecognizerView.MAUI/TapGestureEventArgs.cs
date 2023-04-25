namespace GestureRecognizerView.MAUI;

public class TapGestureEventArgs
{
    /// <summary>
    /// Indicates the gesture recognizer status.
    /// </summary>
    public GestureRecognizerStatus Status { get; internal set; }
    /// <summary>
    /// Pointer associated to this tap gesture recognizer.
    /// </summary>
    public PointerInfo Pointer { get; internal set; }
    /// <summary>
    /// X tap coordinate relative to the view.
    /// </summary>
    public double X { get => Pointer.EndPoint.X; }
    /// <summary>
    /// Y tap coordinate relative to the view.
    /// </summary>
    public double Y { get => Pointer.EndPoint.Y; }
    /// <summary>
    /// Indicates the tap pressure.
    /// </summary>
    public float Pressure { get => Pointer.EndPressure; }
}
