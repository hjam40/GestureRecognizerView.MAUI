namespace GestureRecognizerView.MAUI;

public class PointerInfo
{
    /// <summary>
    /// Unique ID for this pointer.
    /// </summary>
    public uint PointerId { get; internal set; } = 0;
    /// <summary>
    /// Start position when the pointer is detected in the View.
    /// </summary>
    public Point StartPoint { get; internal set; }
    /// <summary>
    /// Pointer position in the previews event.
    /// </summary>
    public Point PreviewsPoint { get; internal set; }
    /// <summary>
    /// Last position of the pointer over the View.
    /// </summary>
    public Point EndPoint { get; internal set; }
    /// <summary>
    /// Start time when the pointer is detected in the View.
    /// </summary>
    public DateTime StartTime { get; internal set; }
    /// <summary>
    /// Previews pointer event time.
    /// </summary>
    public DateTime PreviewsTime{ get; internal set; }
    /// <summary>
    /// Last time when the pointer has raised an event in the view.
    /// </summary>
    public DateTime EndTime { get; internal set; }
    /// <summary>
    /// Indicates if the pointer is pressed.
    /// </summary>
    public bool Pressed { get; internal set; }
    /// <summary>
    /// Start pressure when the pointer is detected in the View.
    /// </summary>
    public float StartPressure { get; internal set; }
    /// <summary>
    /// Pointer pressure in the previews event.
    /// </summary>
    public float PreviewsPressure { get; internal set; }
    /// <summary>
    /// Last pressure of the pointer over the View.
    /// </summary>
    public float EndPressure { get; internal set; }
    /// <summary>
    /// Pointer device type.
    /// </summary>
    public PointerType PointerType { get; internal set; }
    /// <summary>
    /// For pointer of mouse devices indicates the wheel movement direction (negative/positive).
    /// </summary>
    public double MouseWheelDelta { get; internal set; } = 0;
    /// <summary>
    /// For pointer of mouse devices indicates if horizontal wheel has moved.
    /// </summary>
    public bool IsHorizontalMouseWheel { get; internal set; } = false;
    /// <summary>
    /// For pointer of mouse devices indicates if right button is pressed.
    /// </summary>
    public bool IsRightButtonPressed { get; internal set; } = false;
    /// <summary>
    /// For pointer of mouse devices indicates if left button is pressed.
    /// </summary>
    public bool IsLeftButtonPressed { get; internal set; } = false;
    /// <summary>
    /// For pointer of mouse devices indicates if middle button is pressed.
    /// </summary>
    public bool IsMiddleButtonPressed { get; internal set; } = false;
    internal GestureRecognizerView.GestureType State1 { get; set; }
    internal GestureRecognizerView.GestureType State2 { get; set; }

}
