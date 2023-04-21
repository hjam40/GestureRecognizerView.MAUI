namespace GestureRecognizerView.MAUI;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseGestureRecognizerView(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(h =>
        {
            h.AddHandler(typeof(GestureRecognizerView), typeof(GestureRecognizerViewHandler));
        });
        return builder;
    }
}
