namespace GestureRecognizerView.MAUI;

public class PointerGestureEventArgs
{
    /// <summary>
    /// Indicates the gesture recognizer status.
    /// </summary>
    public PointerRecognizerStatus Status { get; internal set; }
    /// <summary>
    /// Pointer associated to this event.
    /// </summary>
    public PointerInfo Pointer { get; internal set; }
    /// <summary>
    /// X pointer coordinate relative to the view.
    /// </summary>
    public double X { get => Pointer.EndPoint.X; }
    /// <summary>
    /// Y pointer coordinate relative to the view.
    /// </summary>
    public double Y { get => Pointer.EndPoint.Y; }
    /// <summary>
    /// Indicates if pointer is pressed.
    /// </summary>
    public bool Pressed { get => Pointer.Pressed; }
    /// <summary>
    /// Indicates the pointer pressure.
    /// </summary>
    public float Pressure { get => Pointer.EndPressure; }

}
