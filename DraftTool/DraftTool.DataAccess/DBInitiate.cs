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
        }
    }
}
