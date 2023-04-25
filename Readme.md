
# GestureRecognizerView.MAUI

A View control for improve .net Maui gesturerecognizers.

## GestureRecognizerView

A view control that captures pointer events and translates them into gesture events. These are its main properties and events:

 ```csharp
    /// Binding property for use this control in MVVM.
    public GestureRecognizerView Self
 ```

Events:
|   | Android  | iOS/Mac  | Windows  |
|---|---|---|---|
| TapGestureListener  |  ✅ | ✅  | ✅  |
| PanGestureListener  | ✅  | ✅  | ✅  |
| PinchGestureListener  | ✅  | ✅  | ✅  |
| PointerGestureListener  | ✅  | ✅  | ✅  |
| MouseListener  | ✅  |   | ✅  |

All events have their own properties:

```csharp
public class TapGestureEventArgs
{
    /// Indicates the gesture recognizer status.
    public GestureRecognizerStatus Status
    /// Pointer associated to this tap gesture recognizer.
    public PointerInfo Pointer
    /// X tap coordinate relative to the view.
    public double X
    /// Y tap coordinate relative to the view.
    public double Y
    /// Indicates the tap pressure.
    public float Pressure
}
public class PanGestureEventArgs
{
    /// Indicates the gesture recognizer status.
    public GestureRecognizerStatus Status
    /// List of pointers associated to this event.
    public List<PointerInfo> Pointers
    /// Indicates the total change in the x direction since the beginning of the gesture.
    public double TotalX
    /// Indicates the total change in the y direction since the beginning of the gesture.
    public double TotalY
    /// Indicates the increment/decrement change in the x direction since the last update of the gesture.
    public double IncX
    /// Indicates the increment/decrement change in the y direction since the last update of the gesture.
    public double IncY
}
public class PinchGestureEventArgs
{
    /// Indicates the gesture recognizer status.
    public GestureRecognizerStatus Status
    /// List of pointers associated to this event.
    public List<PointerInfo> Pointers
    /// Indicates the total change in the scale since the beginning of the gesture.
    public double Scale
    /// Indicates the increment/decrement of the scale since the last update of the gesture.
    public double ScaleIncrement
    /// Indicates the total change in the vector roation between the 2 touch points since the beginning of the gesture.
    public double Rotation
    /// Indicates the increment/decrement of the vector rotation between the 2 touch points since the last update of the gesture.
    public double RotationIncrement
}
public class PointerGestureEventArgs
{
    /// Indicates the gesture recognizer status.
    public PointerRecognizerStatus Status
    /// Pointer associated to this event.
    public PointerInfo Pointer
    /// X pointer coordinate relative to the view.
    public double X
    /// Y pointer coordinate relative to the view.
    public double Y
    /// Indicates if pointer is pressed.
    public bool Pressed
    /// Indicates the pointer pressure.
    public float Pressure
}
public class MouseEventArgs
{
    /// Indicates the mouse status.
    public MouseRecognizerStatus Status
    /// Pointer associated to this event.
    public PointerInfo Pointer
    /// X pointer coordinate relative to the view.
    public double X
    /// Y pointer coordinate relative to the view.
    public double Y
    /// Indicates the wheel movement direction (negative/positive).
    public double MouseWheelDelta
    /// Indicates if horizontal wheel has moved.
    public bool IsHorizontalMouseWheel
    /// Indicates if right button is pressed.
    public bool IsRightButtonPressed
    /// Indicates if left button is pressed.
    public bool IsLeftButtonPressed
    /// Indicates if middle button is pressed.
    public bool IsMiddleButtonPressed
}
```

### Install and configure GestureRecognizerView

1. Download and Install [GestureRecognizerView.MAUI](https://www.nuget.org/packages/GestureRecognizerView.MAUI) NuGet package on your application.

1. Initialize the plugin in your `MauiProgram.cs`:

    ```csharp
    // Add the using to the top
    using GestureRecognizerView.MAUI;
    
    public static MauiApp CreateMauiApp()
    {
    	var builder = MauiApp.CreateBuilder();
    
    	builder
    		.UseMauiApp<App>()
    		.UseGestureRecognizerView(); // Add the use of the plugging
    
    	return builder.Build();
    }
    ```

### Using GestureRecognizerView

In XAML, make sure to add the right XML namespace:

`xmlns:grv="clr-namespace:GestureRecognizerView.MAUI;assembly=GestureRecognizerView.MAUI"`

Use the control:
```xaml
    <Grid>
        <VerticalStackLayout VerticalOptions="Center">
            <Border WidthRequest="300" HeightRequest="300">
                <Grid>
                    <Image x:Name="img" HorizontalOptions="Fill" VerticalOptions="Fill" Aspect="AspectFit" Source="dotnet_bot.png" />
                    <grv:GestureRecognizerView x:Name="recognizerView" BindingContext="{x:Reference mainPage}" HorizontalOptions="Fill" VerticalOptions="Fill" Self="{Binding GestureRecognizer}" />
                </Grid>
            </Border>
        </VerticalStackLayout>
    </Grid>
```

Configure the events:
```csharp
        private GestureRecognizerView gestureRecognizer;
        public GestureRecognizerView GestureRecognizer
        {
            get => gestureRecognizer;
            set
            {
                gestureRecognizer = value;
                if (gestureRecognizer != null)
                {
                    gestureRecognizer.TapGestureListener += RecognizerView_TapGestureListener;
                    gestureRecognizer.PointerGestureListener += RecognizerView_PointerGestureListener;
                    gestureRecognizer.PanGestureListener += RecognizerView_PanGestureListener;
                    gestureRecognizer.PinchGestureListener += RecognizerView_PinchGestureListener;
                    gestureRecognizer.MouseListener += RecognizerView_MouseListener;
                }
            }
        }
 ```
Use the events:
```csharp
        private void RecognizerView_MouseListener(object sender, MouseEventArgs args)
        {
            if (args.Status == MouseRecognizerStatus.WheelMoved)
            {
                if (args.MouseWheelDelta < 0)
                    img.Scale -= 0.05;
                else if (args.MouseWheelDelta > 0)
                    img.Scale += 0.05;
            }
            Debug.WriteLine($"Mouse status={args.Status} X={args.X} Y={args.Y} WheelDelta={args.MouseWheelDelta} LeftPressed={args.IsLeftButtonPressed} RightPressed={args.IsRightButtonPressed}");
        }

        private void RecognizerView_PinchGestureListener(object sender, PinchGestureEventArgs args)
        {
            switch (args.Status)
            {
                case GestureRecognizerStatus.Started:
                    initScale = img.Scale;
                    initRotation = img.Rotation;
                    break;
                case GestureRecognizerStatus.Running:
                    img.Scale += args.ScaleIncrement;
                    img.Rotation += args.RotationIncrement;
                    break;
                case GestureRecognizerStatus.Complete:
                    img.Scale += args.ScaleIncrement;
                    img.Rotation += args.RotationIncrement;
                    break;
                case GestureRecognizerStatus.Cancel:
                    img.Scale = initScale;
                    img.Rotation = initRotation;
                    break;
            }
        }

        private void RecognizerView_PanGestureListener(object sender, PanGestureEventArgs args)
        {
            switch(args.Status)
            {
                case GestureRecognizerStatus.Started:
                    initX = img.TranslationX;
                    initY = img.TranslationY;
                    break;
                case GestureRecognizerStatus.Running:
                    img.TranslationX = initX + args.TotalX;
                    img.TranslationY = initY + args.TotalY;
                    break;
                case GestureRecognizerStatus.Complete:
                    img.TranslationX = initX + args.TotalX;
                    img.TranslationY = initY + args.TotalY;
                    initX = img.TranslationX;
                    initY = img.TranslationY;
                    break;
                case GestureRecognizerStatus.Cancel:
                    img.TranslationX = initX;
                    img.TranslationY = initY;
                    break;
            }
        }

        private void RecognizerView_PointerGestureListener(object sender, PointerGestureEventArgs args)
        {
            if (args.Status != PointerRecognizerStatus.Move)
                Debug.WriteLine($"Pointer status={args.Status} X={args.X} Y={args.Y} Pressed={args.Pressed}");
        }

        private void RecognizerView_TapGestureListener(object sender, TapGestureEventArgs args)
        {
            if (args.Status == GestureRecognizerStatus.Complete)
            {
                if ((DateTime.Now - lastTap).TotalMilliseconds <= 500)
                {
                    img.TranslationX = img.TranslationY = 0;
                    img.Scale = 1;
                    img.Rotation = 0;
                }
                lastTap = DateTime.Now;
            }
        }

```
All events have a Pointer(s) propertie containing all pointers associated to them:
PointerInfo has the next properties:
```csharp
    /// Unique ID for this pointer.
    public uint PointerId
    /// Start position when the pointer is detected in the View.
    public Point StartPoint
    /// Pointer position in the previews event.
    public Point PreviewsPoint
    /// Last position of the pointer over the View.
    public Point EndPoint
    /// Start time when the pointer is detected in the View.
    public DateTime StartTime
    /// Previews pointer event time.
    public DateTime PreviewsTime
    /// Last time when the pointer has raised an event in the view.
    public DateTime EndTime
    /// Indicates if the pointer is pressed.
    public bool Pressed
    /// Start pressure when the pointer is detected in the View.
    public float StartPressure
    /// Pointer pressure in the previews event.
    public float PreviewsPressure
    /// Last pressure of the pointer over the View.
    public float EndPressure
    /// Pointer device type.
    public PointerType PointerType
    /// For pointer of mouse devices indicates the wheel movement direction (negative/positive).
    public double MouseWheelDelta
    /// For pointer of mouse devices indicates if horizontal wheel has moved.
    public bool IsHorizontalMouseWheel
    /// For pointer of mouse devices indicates if right button is pressed.
    public bool IsRightButtonPressed
    /// For pointer of mouse devices indicates if left button is pressed.
    public bool IsLeftButtonPressed
    /// For pointer of mouse devices indicates if middle button is pressed.
    public bool IsMiddleButtonPressed
```