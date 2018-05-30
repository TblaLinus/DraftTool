using DraftTool.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DraftTool.DataAccess
{
    public class CubeRepo : DBInitiate, ICubeRepo
    {
        public List<Cube> GetCubes()
        {
            List<Cube> cubes = new List<Cube>();

            string sql = "Select * FROM [Cubes]";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cube cube = new Cube()
                    {
                        Name = reader["Name"].ToString(),
                    };
                    cubes.Add(cube);
                }
                return cubes;
            }
        }

        public void AddCube(Cube cube)
        {
            string sql = $"INSERT INTO [Cubes]([Name]) VALUES('{cube.Name}')";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCube(string name)
        {
            string sql = $"DELETE FROM [Cubes] WHERE Name='{name}'";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetCards(string cube)
        {
            List<string> cards = new List<string>();

            string sql = $"Select [CardName] FROM [CubeCards] WHERE CubeName='{cube}'";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cards.Add(reader["CardName"].ToString());
                }
                return cards;
            }
        }

        public void AddCard(string cardName, string cubeName)
        {
            string sql = $"INSERT INTO [CubeCards]([CardName], [CubeName]) VALUES('{cardName}', '{cubeName}')";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCard(string cardName, string cubeName)
        {
            string sql = $"DELETE FROM [CubeCards] WHERE CardName='{cardName}' AND CubeName='{cubeName}'";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }
    }
}
