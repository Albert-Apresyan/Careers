using System;
using Microsoft.AspNetCore.Http;

public static class RequestExtention
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.Headers != null)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        return false;
    }
}
