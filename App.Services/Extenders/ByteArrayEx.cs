namespace App.Services.Extenders;

public static class ByteArrayEx
{
    /// <summary>
    /// Bir <see cref="Stream"/> örneğini byte dizisine dönüştürür.
    /// </summary>
    /// <param name="stream">Dönüştürülecek <see cref="Stream"/> örneği.</param>
    /// <returns>Byte dizisi.</returns>
    public static byte[] ToByteArray(this Stream stream)
    {
        MemoryStream memoryStream = new();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Byte dizisini base64 formatında bir resim yoluna dönüştürür.
    /// </summary>
    /// <param name="value">Dönüştürülecek byte dizisi.</param>
    /// <returns>Base64 formatında resim yolunu içeren bir string.</returns>
    public static string GetBase64ImagePath(this byte[] value)
    {
        if (value == null || value.Length == 0)
        {
            return string.Empty;
        }

        string base64Image = Convert.ToBase64String(value);
        return $"data:image/jpeg;base64,{base64Image}";
    }

}