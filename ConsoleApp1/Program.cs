using System;
using System.Linq;
 
      

        Console.WriteLine("📦 Parsing HTML...");
        var parser = new HtmlParser();
        HtmlElement root =await parser.Parse(" https://forum.netfree.link:20907/");

        Console.WriteLine("🔎 Querying...");
        var selector = Selector.Parse("li.nav-item a span");
        var results = root.Query(selector);

        Console.WriteLine($"Found {results.Count()} results:");
        foreach (var el in results)
            Console.WriteLine(el);


