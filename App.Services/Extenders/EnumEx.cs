using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Services.Extenders;
public static class EnumExtenders
{

    /// <summary>
    /// Belirtilen enum değerinin görüntü adını alır. Eğer enum değeri için bir DisplayAttribute tanımlanmışsa, 
    /// Name özelliğinin değerini döndürür. Aksi halde, enum değerinin kendisini string olarak döndürür.
    /// </summary>
    /// <param name="value">Enum değeri.</param>
    /// <returns>Enum değerinin görüntü adı.</returns>
    public static string GetDisplayName(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DisplayAttribute));
        return attribute?.Name ?? value.ToString();
    }

    /// <summary>
    /// Enum değerlerini temsil eden SelectListItem nesnelerinin bir listesini döndürür. 
    /// Her SelectListItem'in Name özelliği, enum değerinin görüntü adına ayarlanır 
    /// (GetDisplayName genişletme yöntemi kullanılarak elde edilir). 
    /// Value özelliği, enum değerinin integer değerine ayarlanır. 
    /// Varsayılan olarak, hiçbir öğe seçilmiş değildir.
    /// </summary>
    /// <param name="enumType">Enum türü.</param>
    /// <returns>Enum değerlerini temsil eden SelectListItem nesnelerinin bir listesi.</returns>
    public static List<SelectListItem> GetSelectList(this Enum enumType)
    {
        var enumList = new List<SelectListItem>();
        foreach (Enum value in Enum.GetValues(enumType.GetType()))
        {
            enumList.Add(new SelectListItem
            {
                Value = Convert.ToInt32(value).ToString(),
                Text = value.GetDisplayName(),
                Selected = false
            });
        }
        return enumList;
    }

}
