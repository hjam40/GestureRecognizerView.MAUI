namespace GestureRecognizerView.MAUI;

public class PanGestureEventArgs
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
    /// Indicates the total change in the x direction since the beginning of the gesture.
    /// </summary>
    public double TotalX { get; internal set; }
    /// <summary>
    /// Indicates the total change in the y direction since the beginning of the gesture.
    /// </summary>
    public double TotalY { get; internal set; }
    /// <summary>
    /// Indicates the increment/decrement change in the x direction since the last update of the gesture.
    /// </summary>
    public double IncX { get; internal set; }
    /// <summary>
    /// Indicates the increment/decrement change in the y direction since the last update of the gesture.
    /// </summary>
    public double IncY { get; internal set; }
}
