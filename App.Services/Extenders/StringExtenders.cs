using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Services.Extenders;

public static class StringExtenders
{
    public static bool IsEmpty(this string text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    public static string GetString(this TagBuilder tag)
    {
        var writer = new StringWriter();
        tag.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    public static string GetString(this IHtmlContent content)
    {
        var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    public static string GetString(this IHtmlContentBuilder content)
    {
        var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}
