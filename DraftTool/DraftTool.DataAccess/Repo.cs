using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.DataAccess
{
    public class Repo : DraftToolDBContext, IRepo
    {
        public Repo()
        {
            CreateTables();
        }

        private void CreateTables()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS [Cards] (                                              
                                    [Name] NVARCHAR(50)  NOT NULL,
                                    [ImageURL] NVARCHAR(200) NOT NULL,
                                    [Side] NVARCHAR(10) NOT NULL,
                                    [Set] NVARCHAR(50) NOT NULL,
                                    [MaxNumberOfUses] INT NOT NULL
                                    )";

            using (_database)
            {
                _database.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = _database };
                cmd.ExecuteNonQuery();
            }
        }

        public List<Card> GetCards()
        {
            List<Card> cards = new List<Card>();

            string sql = "Select * FROM [Cards]";

            using (_database)
            {
                _database.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = _database };
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Card card = new Card()
                    {
                        Name = reader["Name"].ToString(),
                        ImageURL = reader["ImageURL"].ToString(),
                        Side = reader["Side"].ToString(),
                        Set = reader["Set"].ToString(),
                        MaxNumberOfUses = (int)reader["MaxNumberOfUses"]
                    };
                    cards.Add(card);
                }
                return cards;
            }
        }
    }
}
