#if IOS || MACCATALYST
using PlatformView = GestureRecognizerView.MAUI.Apple.GestureRecognizerView;
#elif ANDROID
using PlatformView = GestureRecognizerView.MAUI.Android.GestureRecognizerView;
#elif WINDOWS
using PlatformView = GestureRecognizerView.MAUI.Windows.GestureRecognizerView;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using Microsoft.Maui.Handlers;

namespace GestureRecognizerView.MAUI;

internal class GestureRecognizerViewHandler : ViewHandler<GestureRecognizerView, PlatformView>
{
    public static IPropertyMapper<GestureRecognizerView, GestureRecognizerViewHandler> PropertyMapper = new PropertyMapper<GestureRecognizerView, GestureRecognizerViewHandler>(ViewMapper)
    {
    };
    public static CommandMapper<GestureRecognizerView, GestureRecognizerViewHandler> CommandMapper = new(ViewCommandMapper)
    {
    };
    public GestureRecognizerViewHandler() : base(PropertyMapper, CommandMapper)
    {
    }
    protected override PlatformView CreatePlatformView()
    {
#if ANDROID
        return new PlatformView(Context, VirtualView);
#elif IOS || MACCATALYST || WINDOWS
        return new PlatformView(VirtualView);
#else
        return new();
#endif
    }
}
