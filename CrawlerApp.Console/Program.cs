using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Data;
using System.Data.SqlClient;
using CrawlerApp.DataStore;

namespace CrawlerApp.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            //the link to crawl and the empty list to store all the crawled links
            string testlink = "https://www.ksl.com/";
            List<string> testList = new List<string>();

            //create the crawler
            DHCrawler testCrawler = new DHCrawler(testList, testlink);

            testCrawler.Crawl(testlink);
            
            //testCrawler.printAllLink(); //test if the crawler get any data

            //create a database connection for crawler
            CrawlerAppConsoleDatabaseConnection crawlerDatabase = new CrawlerAppConsoleDatabaseConnection(testCrawler._pagesVisited, testCrawler._pagesToVisit);

            /*
             these lines are used for testing if DataStore gets same value of the the Console App
             */
            //System.Console.WriteLine("Links in database");
            //crawlerDatabase.printAllLinkOfDatabase();

            //input links from Crawler to Database
            if (crawlerDatabase.TestDatabaseConnection() == 1)
            {
                crawlerDatabase.InsertDataToDatabase();
            }

            ////testing purpose
            //crawlerDatabase.printAllLinkOfDatabase();

            System.Console.WriteLine("Successfully crawl and end crawling now");
            System.Console.ReadKey();

        }
    }
}
