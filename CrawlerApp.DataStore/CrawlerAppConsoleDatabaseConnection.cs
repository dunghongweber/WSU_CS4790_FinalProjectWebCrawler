using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace CrawlerApp.DataStore
{
    public class CrawlerAppConsoleDatabaseConnection
    {
        private List<string> _linksVisited; //for storing all the links for database
        private List<string> _linksNotVisited; //for storing all the links for database
        private List<Link> _linkForDatabase =  new List<Link>();

        //set up data for inserting
        public CrawlerAppConsoleDatabaseConnection(List<string> visitedLinks, List<string> notYetVisitedLinks)
        {
            _linksVisited = visitedLinks;
            _linksNotVisited = notYetVisitedLinks;
            foreach (var e in _linksVisited)
            {
                Link linkToAdd = new Link();
                linkToAdd.TheLink = e;
                linkToAdd.CrawledOrNot = 1;

                _linkForDatabase.Add(linkToAdd);
            }

            foreach (var e in _linksNotVisited)
            {
                Link linkToAdd = new Link();
                linkToAdd.TheLink = e;
                linkToAdd.CrawledOrNot = 0;

                _linkForDatabase.Add(linkToAdd);
            }
        }

        //try to establish database connection
        public int TestDatabaseConnection()
        {
            string connetionString = null;
            SqlConnection cnn;

            connetionString = "Server= (LocalDB)\\MSSQLLocalDB; Database= CS2550Tutor;Integrated Security = SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                System.Console.WriteLine("Successfully Establish Database Connection!");
                cnn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("*** Cannot Connect to Database ****");
                System.Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public void InsertDataToDatabase()
        {
            string connetionString = null;
            string sql = null;
            SqlCommand command;
            SqlConnection cnn;
            connetionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=CS2550Tutor;Integrated Security = SSPI;";
            
            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();

                
                sql = "INSERT INTO MyLinks (Link,Crawled) VALUES (@Link,@Crawled)";

                command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@Link", DbType.String);
                command.Parameters.AddWithValue("@Crawled", DbType.Int32);

                foreach (var e in _linkForDatabase)
                {
                    command.Parameters[0].Value = e.TheLink;
                    command.Parameters[1].Value = e.CrawledOrNot;

                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        System.Console.WriteLine("Error inserting data into Database!");
                        cnn.Close();
                    }
                    else
                    {
                        //use for testing success insert
                        //System.Console.WriteLine("SUCCESSFULLY insert data into Database!");
                    }
                }

                System.Console.WriteLine("SUCCESSFULLY insert data into Database!");
                cnn.Close();
            }
        }

        //testing purpose
        public void printAllLinkOfDatabase()
        {
            foreach (var e in _linkForDatabase)
            {
                System.Console.WriteLine("THe Link: "+e.TheLink);
                System.Console.WriteLine("Crawled: " + e.CrawledOrNot);
                System.Console.WriteLine();
            }
        }

    }    
}
