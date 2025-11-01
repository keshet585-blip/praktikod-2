using System;
using System.IO;
using System.Text.Json;

internal class HtmlHelper
{
    private readonly static HtmlHelper instance = new HtmlHelper();

    public static HtmlHelper Instance => instance;

    public string[] HtmlTags { get; private set; }
    public string[] HtmlVoidTags { get; private set; }

    private HtmlHelper()
    {
        HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlTags.json"));
        HtmlVoidTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlVoidTags.json"));
    }

    public bool IsTag(string tagName) => Array.Exists(HtmlTags, t => t.Equals(tagName, StringComparison.OrdinalIgnoreCase));
    public bool IsHtmlVoidTag(string tagName) => Array.Exists(HtmlVoidTags, t => t.Equals(tagName, StringComparison.OrdinalIgnoreCase));
}
