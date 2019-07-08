using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using HtmlAgilityPack;
using System.Data.SqlClient;
using CrawlerApp.DataStore;


namespace CrawlerApp.Console
{
    public class DHCrawler
    {
        public List<string> _pagesVisited = new List<string>();  //store all the links Crawler reached
        public List<string> _pagesToVisit = new List<string>();  //store all the links Crawler has not reached

        
        public DHCrawler(List<string> listToStoreURLs, string inputLink)
        {
            _pagesToVisit.Add(inputLink);
        }
        

        public void Crawl(string inputLinkToCrawl)
        {
            //set the total number of pages to be crawled
            while(_pagesVisited.Count < 30)
            {
                //the Try Catch statement here is to prevent somewhere pages that ksl gives back
                try
                {
                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = web.Load(inputLinkToCrawl, "GET");
                    // selectnode //a[@href]
                    var selectedDocNode = doc.DocumentNode.SelectNodes("//a[@href]");
                    if (selectedDocNode != null)
                    {
                        foreach (HtmlNode node in selectedDocNode)
                        {
                            //get the link
                            var extractedUrl = node.GetAttributeValue("href", string.Empty);

                            if (!extractedUrl.Contains('#'))
                            {
                                //add the link to pagesToVisit
                                _pagesToVisit.Add(GetAbsoluteUrlString(inputLinkToCrawl, extractedUrl));
                            }

                        }
                    }

                    //test if the crawler works
                    System.Console.WriteLine("Crawl link number " + _pagesVisited.Count);
                    System.Console.WriteLine("Crawled link: " + _pagesVisited.Count);
                    System.Console.WriteLine("Start crawling next link");

                    string newLink = NextLink();

                    //test if there is a new link
                    System.Console.WriteLine(newLink);

                    Crawl(newLink);
                }
                //continue crawling other pages if KSL.com gives back weird pages or responses
                catch (System.Net.WebException e)
                {

                    System.Console.WriteLine(e.Message);
                    string newLink = NextLink();

                    //test if there is a new link
                    System.Console.WriteLine(newLink);

                    Crawl(newLink);
                }
                
            }
            
        }
        /*
         This method makes the crawler crawls through sub-link and return it
             */
        public string NextLink()
        {
            string nextUrl;
            do
            {
                nextUrl = _pagesToVisit.ElementAt(0);
                _pagesToVisit.RemoveAt(0);
            } while (_pagesVisited.Contains(nextUrl));

            _pagesVisited.Add(nextUrl);

            return nextUrl;
        }

        /*
         this method takes in the Base input url and also the url that the crawler crawls
         then makes sure to give out the full html link 
             */
        static string GetAbsoluteUrlString(string baseUrl, string url)
        {

            Uri uriResult;

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uriResult))
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                {
                    uri = new Uri(new Uri(baseUrl), uri);
                }
                return uri.ToString();
            }
            else
            {
                return url;
            }
        }
        
        //testing purpose
        public void printAllLink()
        {
            foreach(string e in _pagesVisited)
            {
                System.Console.WriteLine(e);
            }
        }


    }
}
