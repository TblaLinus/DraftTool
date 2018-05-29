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
    public class CardRepo : DBInitiate, ICardRepo
    {
        public List<Card> GetCards()
        {
            List<Card> cards = new List<Card>();

            string sql = "Select * FROM [Cards]";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
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

        public void AddCard(Card card)
        {
            string sql = $@"INSERT INTO [Cards]([Name], [ImageURL], [Side], [Set], [MaxNumberOfUses]) 
                         VALUES('{card.Name}', '{card.ImageURL}', '{card.Side}', '{card.Set}', '{card.MaxNumberOfUses}')";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCard(string name)
        {
            string sql = $@"DELETE FROM [Cards] WHERE Name='{name}'";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }
    }
}
