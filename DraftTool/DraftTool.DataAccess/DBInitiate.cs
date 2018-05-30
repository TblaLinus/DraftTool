using System.Data.SQLite;
using System.IO;

namespace DraftTool.DataAccess
{
    public class DBInitiate
    {
        private const string DBPath = "../../../database.db";
        protected const string DBSource = "data source=" + DBPath;

        protected DBInitiate()
        {
            if (!File.Exists(DBPath))
            {
                SQLiteConnection.CreateFile(DBPath);
            }

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
                                    );
                        CREATE TABLE IF NOT EXISTS [Sets] (                                              
                                    [Name] NVARCHAR(50)  NOT NULL
                                    );
                        CREATE TABLE IF NOT EXISTS [Cubes] (                                              
                                    [Name] NVARCHAR(50)  NOT NULL
                                    );
                        CREATE TABLE IF NOT EXISTS [CubeCards] (                                              
                                    [CardName] NVARCHAR(50)  NOT NULL,
                                    [CubeName] NVARCHAR(50)  NOT NULL
                                    );";

            using (SQLiteConnection conn = new SQLiteConnection(DBSource))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand { CommandText = sql, Connection = conn };
                cmd.ExecuteNonQuery();
            }
        }
    }
}
