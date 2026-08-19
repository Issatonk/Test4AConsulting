using System.Xml.Linq;

namespace Test4AConsulting.MVC.Helpers;

public static class XmlContentHelper
{
    public static string? ToXml(string? html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return null;

        var xml = $"<contents>{html}</contents>";

        XDocument.Parse(xml);

        return xml;
    }

    public static string? FromXml(string? xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return null;

        var document = XDocument.Parse(xml);

        return string.Concat(
            document.Root?
                .Nodes()
                .Select(x => x.ToString())
            ?? Enumerable.Empty<string>());
    }
}