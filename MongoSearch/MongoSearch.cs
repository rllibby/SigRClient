using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSearch
{
    public class MongoSearch
    {
        
        private MongoServer _server;
        private MongoDatabase _database;
       // private MongoCollection _collection;

        
        /*
                         var enableTextSearchCommand = new CommandDocument
         *              {                    { "setParameter", 1 },                    { "textSearchEnabled", true }                };                var adminDatabase = _server.GetDatabase("admin");                adminDatabase.RunCommand(enableTextSearchCommand);                if (_collection.Exists()) { _collection.Drop(); }                _collection.Insert(new BsonDocument("x", "The quick brown fox"));                _collection.Insert(new BsonDocument("x", "jumped over the fence"));                _collection.EnsureIndex(new IndexKeysDocument("x", "text"));                var textSearchCommand = new CommandDocument                {                    { "text", _collection.Name },                    { "search", "fox" }                };                var commandResult = _database.RunCommand(textSearchCommand);                var response = commandResult.Response;                Assert.AreEqual(1, response["stats"]["nfound"].ToInt32());                Assert.AreEqual("The quick brown fox", response["results"][0]["obj"]["x"].AsString);
        */
        public MongoSearch()
        {   
            MongoInit();
            EnableTextSearch();
        }

        private void MongoInit()
        {
            _server = MongoServer.Create("mongodb://localhost/test");
            _database = _server.GetDatabase("test");

        }

        private void EnableTextSearch()
        {
            var enableTextSearchCommand = new CommandDocument
            {
                { "setParameter", 1},
                { "textSearchEnabled", true }
            };

            var adminDataBase = _server.GetDatabase("admin");
            adminDataBase.RunCommand(enableTextSearchCommand);

        }

        //public List<string> MongoSearchText(string textToSearch, out string context, out string entity, out string subEntity)
        public List<string> MongoSearchText(string textToSearch, out string context, out string entity)
        {
            List<string> result = new List<string>(); //this will be the key value for the top search

            //if (textToSearch.Contains("quantity"))
            context = "quantity";
            entity = "items";
            /*
            subEntity = "";
            //first we want to search for keywords that will determine the context
            // such as "stock check", "quantity", "sc", "qty", "how many" = ci_item_xxx for quantity lookups
            // "price check", "price", "pc" = ci_item_xxx for price - keyword "for xxxxx" (american business futures) indicates 
            // customer specific pricing
            var contextSearchCommand = new CommandDocument
            {
                { "text", "contextsearch"},
                { "search", textToSearch }
            };

            var contextCommandResult = _database.RunCommand(contextSearchCommand);
            var contextSearchResponse = contextCommandResult.Response;

            */


            //for now we will just search the items collection and assume we want quantities
            var searchCommand = new CommandDocument
            {
                { "text", "ci_item_abc"},
                { "search", textToSearch}
            };

            //string[] searchWords = textToSearch.Split(' ');
            //int numberOfWords = searchWords.Length;
            var commandResult = _database.RunCommand(searchCommand);
            var response = commandResult.Response;
            try
            {
                Int32 loop = 0;
                while (loop < 3)
                {
                    if (response["results"][loop]["score"] >= 2)
                    {
                        result.Add(response["results"][loop]["obj"]["itemcode"].AsString);

                    }
                    loop++;
                }
            }
            catch (Exception)
            {
                
            }
            //response["results"][0]["obj"]["x"].AsString



            if (result.Count == 0)
            {
                result.Add("Try again refine search using more keywords");
            }
            
            return result;
        }
    }
}
