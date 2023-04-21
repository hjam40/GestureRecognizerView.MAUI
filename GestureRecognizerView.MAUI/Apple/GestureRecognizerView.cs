#if IOS || MACCATALYST
using UIKit;
using MauiView = GestureRecognizerView.MAUI.GestureRecognizerView;

namespace GestureRecognizerView.MAUI.Apple;
public class GestureRecognizerView : UIView
{
    private readonly MauiView mauiView;
    
    public GestureRecognizerView(MauiView mauiView)
    {
        this.mauiView = mauiView;
    }
}
#endif
