using DraftTool.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DraftTool.DataAccess
{
    public class SetRepo : DBInitiate, ISetRepo
    {
        //Sköter all kommunikation mellan Databasens Sets-tabell och resten av programmet
        public List<Set> GetSets()
        {
            List<Set> sets = new List<Set>();

            string sql = "Select * FROM [Sets]";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Set set = new Set()
                    {
                        Name = reader["Name"].ToString(),
                    };
                    sets.Add(set);
                }
                return sets;
            }
        }

        public void AddSet(Set set)
        {
            string sql = $"INSERT INTO [Sets]([Name]) VALUES('{set.Name}')";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSet(string name)
        {
            string sql = $"DELETE FROM [Sets] WHERE Name='{name}'";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }
    }
}
