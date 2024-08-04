using System.Text;
using App.Services.Extenders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace App.Services.TagHelpers;

[HtmlTargetElement("select2")]
public class Select2TagHelper : TagHelper
{
    private readonly IHtmlHelper _htmlHelper;


    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public Select2TagHelper(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper;
    }

    /// <summary>
    /// Modelden gelen ifade
    /// </summary>
    [HtmlAttributeName("v-field")]
    public ModelExpression ModelItem { get; set; }

    /// <summary>
    /// Verilerin yükleneceği URL
    /// </summary>
    [HtmlAttributeName("v-loadFrom")]
    public string Url { get; set; }

    /// <summary>
    /// List<SelectListItem> liste elemanları
    /// </summary>
    [HtmlAttributeName("v-items")]
    public List<SelectListItem> ListItems { get; set; }

    /// <summary>
    /// CSS stili
    /// </summary>
    [HtmlAttributeName("v-style")]
    public string CssStyle { get; set; }

    /// <summary>
    /// Placeholder metni
    /// </summary>
    [HtmlAttributeName("v-placeholder")]
    public string PlaceHolder { get; set; }

    /// <summary>
    /// Çoklu seçim özelliği
    /// </summary>
    [HtmlAttributeName("v-multiple")]
    public bool Multiple { get; set; }

    /// <summary>
    /// Seçimi temizleme özelliği
    /// </summary>
    [HtmlAttributeName("v-canClear")]
    public bool CanClear { get; set; }

    /// <summary>
    /// JavaScript'i devre dışı bırakma
    /// </summary>
    [HtmlAttributeName("v-disable-js")]
    public bool DisableJs { get; set; }

    /// <summary>
    /// Alanın salt okunur olup olmadığı
    /// </summary>
    [HtmlAttributeName("v-readonly")]
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Ebeveyn modülün ID'si
    /// </summary>
    [HtmlAttributeName("v-parentModalId")]
    public string ParentModal { get; set; }

    /// <summary>
    /// Tag'in devre dışı bırakılması
    /// </summary>
    [HtmlAttributeName("v-disableTag")]
    public bool DisableTag { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        (_htmlHelper as IViewContextAware).Contextualize(ViewContext);

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var prefix = ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;
        var htmlId = GetHtmlId(prefix);
        var htmlName = GetHtmlName(prefix);
        var labelText = GetLabelText();

        var optionsHtml = BuildOptionsHtml();
        var selectTag = BuildSelectTag(htmlName, htmlId, optionsHtml);

        var js = BuildJavaScript(htmlId);

        output.Attributes.Add("class", "form-floating mb-3");
        SetOutputContent(output, labelText, selectTag, js);
    }

    private string GetHtmlId(string prefix)
    {
        return string.IsNullOrEmpty(prefix)
            ? ModelItem.Metadata.PropertyName
            : _htmlHelper.IdForModel() + "_" + ModelItem.Metadata.PropertyName;
    }

    private string GetHtmlName(string prefix)
    {
        return string.IsNullOrEmpty(prefix)
            ? ModelItem.Metadata.PropertyName
            : prefix + "." + ModelItem.Metadata.PropertyName;
    }

    private string GetLabelText()
    {
        return ModelItem.Metadata.DisplayName ?? ModelItem.Metadata.PropertyName;
    }

    private string BuildOptionsHtml()
    {
        var sbOptions = new StringBuilder();
        if (ListItems != null)
        {
            foreach (var item in ListItems)
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", item.Value);
                if (item.Selected)
                {
                    option.Attributes.Add("selected", "selected");
                }
                option.InnerHtml.AppendHtml(item.Text);
                sbOptions.AppendLine(option.GetString());
            }
        }
        return sbOptions.ToString();
    }

    private TagBuilder BuildSelectTag(string htmlName, string htmlId, string optionsHtml)
    {
        var select = new TagBuilder("select");
        select.AddCssClass("form-select");
        if (!string.IsNullOrEmpty(CssStyle))
            select.AddCssClass(CssStyle);
        select.Attributes.Add("name", htmlName);
        select.Attributes.Add("id", htmlId);
        if (Multiple)
            select.Attributes.Add("multiple", "multiple");

        select.InnerHtml.AppendHtml(optionsHtml);
        return select;
    }

    private string BuildJavaScript(string htmlId)
    {
        var parentModalJs = !string.IsNullOrEmpty(ParentModal) ? $"dropdownParent: $('#{ParentModal}')," : string.Empty;

        return DisableJs
            ? string.Empty
            : $@"
<script type=""text/javascript"">
$(function () {{
    $(""#{htmlId}"").select2({{
        language: ""tr"",
        placeholder: ""{(string.IsNullOrEmpty(PlaceHolder) ? string.Empty : PlaceHolder)}"",
        {(Multiple ? "multiple:true," : string.Empty)}
        {(CanClear ? "allowClear: true" : string.Empty)}
        {parentModalJs}
    }});
}});
</script>";
    }


    private void SetOutputContent(TagHelperOutput output, string labelText, TagBuilder selectTag, string js)
    {
        var requiredHtml = !ModelItem.Metadata.IsNullableValueType
            ? "<span class=\"text-danger\">*</span>"
            : string.Empty;

        output.Content.SetHtmlContent($@"
{selectTag.GetString()}
<label for='{selectTag.Attributes["id"]}' class='form-label'>{labelText} {requiredHtml}</label>
{js}");
    }

}