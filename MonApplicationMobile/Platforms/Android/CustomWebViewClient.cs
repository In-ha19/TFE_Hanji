using Android.Webkit;
using Android.Net.Http;
using Android.Graphics;
using WebView = Android.Webkit.WebView;


namespace MonApplicationMobile.Platforms.Android
{
    public class CustomWebViewClient : WebViewClient
    {
        public override void OnReceivedSslError(WebView view, SslErrorHandler handler, SslError error)
        {
            handler.Proceed(); // Ignore les erreurs SSL (Seulement pour test)
        }
    }
}
