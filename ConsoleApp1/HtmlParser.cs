using System.Collections.Generic;
using System.Text.RegularExpressions;

internal class HtmlParser
{
    private readonly HtmlHelper helper = HtmlHelper.Instance;

    public HtmlElement Parse(string html)
    {
        var tags = Regex.Split(html, "(?=<)|(?<=>)")
                   ?? new string[0];
        HtmlElement root = new HtmlElement { Name = "root" };
        HtmlElement current = root;

        foreach (var tag in tags)
        {
            string trimmed = tag.Trim();
            if (string.IsNullOrEmpty(trimmed)) continue;

            if (trimmed.StartsWith("</"))
            {
                current = current.Parent ?? root;
            }
            else if (trimmed.StartsWith("<"))
            {
                string tagName = Regex.Match(trimmed, @"<(\w+)").Groups[1].Value;
                if (!helper.IsTag(tagName)) continue;

                var element = new HtmlElement
                {
                    Name = tagName,
                    Parent = current
                };

                // Attributes
                var attrMatches = Regex.Matches(trimmed, @"(\w+)=""(.*?)""");
                foreach (Match match in attrMatches)
                {
                    string key = match.Groups[1].Value;
                    string val = match.Groups[2].Value;
                    element.Attributes[key] = val;
                    if (key == "id") element.Id = val;
                    if (key == "class") element.Classes.AddRange(val.Split(' '));
                }

                current.Children.Add(element);

                if (!trimmed.EndsWith("/>") && !helper.IsHtmlVoidTag(tagName))
                    current = element; // יורדים רמה
            }
            else
            {
                current.InnerHtml += trimmed;
            }
        }

        return root;
    }
}
