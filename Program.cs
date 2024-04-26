using System.Net;
using HtmlAgilityPack;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Scraping started.");

        string baseUrl = "https://books.toscrape.com/";
        string outputDirectory = @"C:\Downloads\ScrapedBooks\";

        // Delete a directory if it exist
        if (Directory.Exists(outputDirectory))
        {
            Directory.Delete(outputDirectory, true);
            Directory.CreateDirectory(outputDirectory);
        }
        // Create a directory if it doesn't exist
        else
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Load the main page
        var htmlWeb = new HtmlWeb();
        var mainPageDocument = htmlWeb.Load(baseUrl);

        // Download main page
        DownloadPage(baseUrl, outputDirectory, mainPageDocument);

        // Get links to other pages
        var links = mainPageDocument.DocumentNode.SelectNodes("//a[@href]");
        if (links != null)
        {
            int pageCount = 0;
            foreach (var link in links)
            {
                string href = link.Attributes["href"].Value;
                string absoluteUrl = new Uri(new Uri(baseUrl), href).AbsoluteUri;

                // Load the page
                var document = htmlWeb.Load(absoluteUrl);

                // Download the page
                DownloadPage(absoluteUrl, outputDirectory, document);

                Console.WriteLine($"Downloaded page {++pageCount} out of {links.Count}");
            }
        }

        Console.WriteLine("Scraping completed.");
    }

    static void DownloadPage(string url, string outputDirectory, HtmlDocument document)
    {
        // Create a directory for the page if it doesn't exist
        string pageName = url.Replace("://", "_").Replace("/", "_").Replace(".", "_");
        string pageDirectory = Path.Combine(outputDirectory, pageName);
        Directory.CreateDirectory(pageDirectory);

        // Save HTML content
        string htmlFilePath = Path.Combine(pageDirectory, "index.html");
        File.WriteAllText(htmlFilePath, document.DocumentNode.OuterHtml);

        // Download CSS files
        var cssLinks = document.DocumentNode.SelectNodes("//link[@rel='stylesheet']/@href");
        if (cssLinks != null)
        {
            foreach (var cssLink in cssLinks)
            {
                string cssUrl = cssLink.Attributes["href"].Value;
                DownloadFile(url, cssUrl, pageDirectory);
            }
        }

        // Download images
        var imgTags = document.DocumentNode.SelectNodes("//img[@src]");
        if (imgTags != null)
        {
            int imgCount = 0;
            foreach (var imgTag in imgTags)
            {
                string imgUrl = imgTag.Attributes["src"].Value;
                if (DownloadFile(url, imgUrl, pageDirectory))
                    Console.WriteLine($"Downloaded image {++imgCount} out of {imgTags.Count}");
            }
        }
    }

    static bool DownloadFile(string baseUrl, string fileUrl, string outputDirectory)
    {
        string absoluteUrl = new Uri(new Uri(baseUrl), fileUrl).AbsoluteUri;
        string fileName = Path.GetFileName(absoluteUrl);
        string filePath = Path.Combine(outputDirectory, fileName);

        try
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(absoluteUrl, filePath);
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to download file: {ex.Message}");
            return false;
        }
    }
}

