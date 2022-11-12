namespace WEB_053502_YUREV.Extensions;

public static class RequestExtension
{
    public static bool IsAjax(this HttpRequest request)
    {
        return request.Headers["x-requested-with"].Equals("XMLHttpRequest");
    }
}