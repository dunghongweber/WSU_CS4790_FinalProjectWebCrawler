using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrawlerApp.Console;
using System.Collections.Generic;
using CrawlerApp.DataStore;

namespace CrawlerApp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEstablishDatabaseConnection()
        {
            //  Arrange
            List<string> _pagesVisited = new List<string>();  //store all the links Crawler reached
            _pagesVisited.Add("www.visited1.com");
            _pagesVisited.Add("www.visited2.com");
            List<string> _pagesToVisit = new List<string>();  //store all the links Crawler has not reached
            _pagesToVisit.Add("www.TOvisited1.com");
            _pagesToVisit.Add("www.TOvisited2.com");
            _pagesToVisit.Add("www.TOvisited3.com");

            //  Act
            CrawlerAppConsoleDatabaseConnection crawlerDatabase = new CrawlerAppConsoleDatabaseConnection(_pagesVisited, _pagesToVisit);

            //  Assert
            Assert.AreEqual(crawlerDatabase.TestDatabaseConnection(), 1);
        }
    }
}
