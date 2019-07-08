using System;

namespace CrawlerApp.DataStore
{
    /*
     this class serve as datatype for the links of the crawler
         */
    public class Link
    {
        public string TheLink { get; set; }
        public int CrawledOrNot { get; set; }
    }
}
