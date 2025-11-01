using System;
using System.Linq;
 
        Console.WriteLine("🔍 Loading HTML...");
        string html = await Load(" https://forum.netfree.link:20907/");

        Console.WriteLine("📦 Parsing HTML...");
        var parser = new HtmlParser();
        HtmlElement root = parser.Parse(html);

        Console.WriteLine("🔎 Querying...");
        var selector = Selector.Parse("li.nav-item a span");
        var results = root.Query(selector);

        Console.WriteLine($"Found {results.Count()} results:");
        foreach (var el in results)
            Console.WriteLine(el);

 static async Task<string> Load(string url)
{
    using HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception("Failed to load HTML");
    }
    return await response.Content.ReadAsStringAsync();
}



