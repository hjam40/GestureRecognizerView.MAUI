using System.Diagnostics;

namespace GestureRecognizerView.MAUI.Test
{
    public partial class MainPage : ContentPage
    {
        double initX, initY, initScale = 0;
        DateTime lastTap = DateTime.MinValue;
        public MainPage()
        {
            InitializeComponent();
            recognizerView.TapGestureListener += RecognizerView_TapGestureListener;
            recognizerView.PointerGestureListener += RecognizerView_PointerGestureListener;
            recognizerView.PanGestureListener += RecognizerView_PanGestureListener;
            recognizerView.PinchGestureListener += RecognizerView_PinchGestureListener;
        }

        private void RecognizerView_PinchGestureListener(object sender, PinchGestureEventArgs args)
        {
            switch (args.Status)
            {
                case GestureRecognizerStatus.Started:
                    initScale = img.Scale;
                    break;
                case GestureRecognizerStatus.Running:
                    img.Scale += args.ScaleIncrement;
                    break;
                case GestureRecognizerStatus.Complete:
                    img.Scale += args.ScaleIncrement;
                    initScale = img.Scale;
                    break;
                case GestureRecognizerStatus.Cancel:
                    img.Scale = initScale;
                    break;
            }
            Debug.WriteLine($"Pinch status={args.Status} Scale={args.Scale} ScaleIncrement={args.ScaleIncrement}");
        }

        private void RecognizerView_PanGestureListener(object sender, PanGestureEventArgs args)
        {
            if (args.Status != GestureRecognizerStatus.Running)
                Debug.WriteLine($"Pan status={args.Status} TotalX={args.TotalX} TotalY={args.TotalY} IncX={args.IncX} IncY={args.IncY}");
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
                }
                lastTap = DateTime.Now;
            }
            Debug.WriteLine($"Tap status={args.Status} X={args.X} Y={args.Y}");
        }
    }
}