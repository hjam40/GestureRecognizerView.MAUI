namespace GestureRecognizerView.MAUI;

public class MouseEventArgs
{
    /// <summary>
    /// Indicates the mouse status.
    /// </summary>
    public MouseRecognizerStatus Status { get; internal set; }
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
    /// Indicates the wheel movement direction (negative/positive).
    /// </summary>
    public double MouseWheelDelta { get => Pointer.MouseWheelDelta; }
    /// <summary>
    /// Indicates if horizontal wheel has moved.
    /// </summary>
    public bool IsHorizontalMouseWheel { get => Pointer.IsHorizontalMouseWheel; }
    /// <summary>
    /// Indicates if right button is pressed.
    /// </summary>
    public bool IsRightButtonPressed { get => Pointer.IsRightButtonPressed; }
    /// <summary>
    /// Indicates if left button is pressed.
    /// </summary>
    public bool IsLeftButtonPressed { get => Pointer.IsLeftButtonPressed; }
    /// <summary>
    /// Indicates if middle button is pressed.
    /// </summary>
    public bool IsMiddleButtonPressed { get => Pointer.IsMiddleButtonPressed; }
}
