﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoSearch = new MongoSearch.MongoSearch();

            List<string> result;
            
            string ent = "item";
            result = mongoSearch.MongoSearchText("left hand drawer", ent);

            result.ForEach(res => Console.WriteLine(res.ToString()));


        }
    }
}
