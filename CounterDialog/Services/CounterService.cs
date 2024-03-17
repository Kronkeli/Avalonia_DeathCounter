using System;
using System.Collections;
using CounterDialog.Models;
using Npgsql;
using System.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterDialog.Services
{
    public class CounterService
    {
        private string _connectionString = ConfigurationManager.AppSettings["connectionstring"];

        public IEnumerable<CounterItem> GetItems()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);
            var dataSource = dataSourceBuilder.Build();

            var query = dataSource.CreateCommand("SELECT Name, Count FROM counter");

            var reader = query.ExecuteReader();

            IEnumerable<CounterItem> counterItems = [];

            while (reader.Read())
            {
                var counterItem = new CounterItem();
                counterItem.Name = reader.GetString(0);
                counterItem.Count = reader.GetInt32(1);
                counterItems = counterItems.Append(counterItem);
            }
            
            return counterItems;
        }

        public void IncrementCounter(string bossName) 
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);
            var dataSource = dataSourceBuilder.Build();

            //var query = dataSource.CreateCommand("UPDATE counter SET Count = Count + 1 WHERE Name = ");
            var conn = dataSource.OpenConnection();

            var cmd = new NpgsqlCommand("UPDATE counter SET Count = Count + 1 WHERE Name = ($1)", conn)
            {
                Parameters = {
                    new() {Value = bossName}
                }
            };
            cmd.ExecuteNonQuery();

        }
    }
}
