using System.Diagnostics;
using System.Numerics;

namespace GestureRecognizerView.MAUI.Test
{
    public partial class MainPage : ContentPage
    {
        double initX = 0, initY = 0, initScale = 0, initRotation = 0;
        DateTime lastTap = DateTime.MinValue;
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
        public MainPage()
        {
            InitializeComponent();
        }
        private void RecognizerView_MouseListener(object sender, MouseEventArgs args)
        {
            if (args.Status == MouseRecognizerStatus.WheelMoved)
            {
                if (args.MouseWheelDelta < 0)
                    img.Scale -= 0.05;
                else if (args.MouseWheelDelta > 0)
                    img.Scale += 0.05;
            }
            //Debug.WriteLine($"Mouse status={args.Status} X={args.X} Y={args.Y} WheelDelta={args.MouseWheelDelta} LeftPressed={args.IsLeftButtonPressed} RightPressed={args.IsRightButtonPressed}");
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
            //Debug.WriteLine($"Pinch status={args.Status} Scale={args.Scale} ScaleIncrement={args.ScaleIncrement}");
        }

        private void RecognizerView_PanGestureListener(object sender, PanGestureEventArgs args)
        {
            //if (args.Status != GestureRecognizerStatus.Running)
            //    Debug.WriteLine($"Pan status={args.Status} TotalX={args.TotalX} TotalY={args.TotalY} IncX={args.IncX} IncY={args.IncY}");
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
            //if (args.Status != PointerRecognizerStatus.Move)
            //    Debug.WriteLine($"Pointer status={args.Status} X={args.X} Y={args.Y} Pressed={args.Pressed}");
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
            //Debug.WriteLine($"Tap status={args.Status} X={args.X} Y={args.Y} Pressure={args.Pressure}");
        }
    }
}