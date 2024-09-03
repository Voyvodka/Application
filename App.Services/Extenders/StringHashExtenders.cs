using System.Text;

namespace App.Services.Extenders;

/// <summary>
/// Bu sınıf, metinleri Base64 formatına çevirme ve geri dönüştürme işlemleri için uzantı metotları sağlar.
/// </summary>
public static class StringHashExtenders
{
    /// <summary>
    /// Verilen metni ASCII kodlaması kullanarak Base64 formatına çevirir.
    /// </summary>
    /// <param name="text">Base64 formatına çevrilecek metin.</param>
    /// <returns>Base64 formatındaki metin.</returns>
    public static string ToBase64(this string text)
    {
        if (text.IsEmpty())
            throw new ArgumentException("Metin boş veya null olamaz.", nameof(text));

        byte[] textBytes = Encoding.ASCII.GetBytes(text);
        return Convert.ToBase64String(textBytes);
    }

    /// <summary>
    /// Verilen Base64 formatındaki metni ASCII kodlaması kullanarak çözümleyip orijinal metne dönüştürür.
    /// </summary>
    /// <param name="text">Çözümleme işlemi yapılacak Base64 formatındaki metin.</param>
    /// <returns>Orijinal metin.</returns>
    public static string FromBase64(this string text)
    {
        if (text.IsEmpty())
            throw new ArgumentException("Metin boş veya null olamaz.", nameof(text));

        byte[] textBytes = Convert.FromBase64String(text);
        return Encoding.ASCII.GetString(textBytes);
    }

    /// <summary>
    /// Verilen metni UTF-8 kodlaması kullanarak Base64 formatına çevirir.
    /// </summary>
    /// <param name="text">Base64 formatına çevrilecek metin.</param>
    /// <returns>Base64 formatındaki metin.</returns>
    public static string ToUtfBase64(this string text)
    {
        if (text.IsEmpty())
            throw new ArgumentException("Metin boş veya null olamaz.", nameof(text));

        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(textBytes);
    }

    /// <summary>
    /// Verilen Base64 formatındaki metni UTF-8 kodlaması kullanarak çözümleyip orijinal metne dönüştürür.
    /// </summary>
    /// <param name="text">Çözümleme işlemi yapılacak Base64 formatındaki metin.</param>
    /// <returns>Orijinal metin.</returns>
    public static string FromUtfBase64(this string text)
    {
        if (text.IsEmpty())
            throw new ArgumentException("Metin boş veya null olamaz.", nameof(text));

        byte[] textBytes = Convert.FromBase64String(text);
        return Encoding.UTF8.GetString(textBytes);
    }
}