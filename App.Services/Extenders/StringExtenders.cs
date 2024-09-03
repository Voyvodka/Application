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

    public static string SecondToTime(this int seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours + (timeSpan.Days * 24),
            timeSpan.Minutes,
            timeSpan.Seconds);
    }

    public static bool IsValidEmail(this string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

}
