using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.DataAccess
{
    public class DraftToolDBContext
    {
        private const string DBPath = "../../../database.db";
        private const string DBSource = "data source=" + DBPath;

        protected readonly SQLiteConnection _database;

        protected DraftToolDBContext()
        {
            if (!File.Exists(DBPath))
            {
                SQLiteConnection.CreateFile(DBPath);
            }
           
            _database = new SQLiteConnection(DBSource);
        }
    }
}
