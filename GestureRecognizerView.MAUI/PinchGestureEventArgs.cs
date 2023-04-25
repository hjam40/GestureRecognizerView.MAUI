namespace GestureRecognizerView.MAUI;

public class PinchGestureEventArgs
{
    /// <summary>
    /// Indicates the gesture recognizer status.
    /// </summary>
    public GestureRecognizerStatus Status { get; internal set; }
    /// <summary>
    /// List of pointers associated to this event.
    /// </summary>
    public List<PointerInfo> Pointers { get; internal set; }
    /// <summary>
    /// Indicates the total change in the scale since the beginning of the gesture.
    /// </summary>
    public double Scale { get; internal set; }
    /// <summary>
    /// Indicates the increment/decrement of the scale since the last update of the gesture.
    /// </summary>
    public double ScaleIncrement { get; internal set; }
    /// <summary>
    /// Indicates the total change in the vector roation between the 2 touch points since the beginning of the gesture.
    /// </summary>
    public double Rotation { get; internal set; }
    /// <summary>
    /// Indicates the increment/decrement of the vector rotation between the 2 touch points since the last update of the gesture.
    /// </summary>
    public double RotationIncrement { get; internal set; }
}
