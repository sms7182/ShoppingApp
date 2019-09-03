using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using ShoppingApp.Helpers;
using ShoppingApp.iOS;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISQLiteDbInterface_iOS))]
namespace ShoppingApp.iOS
{
    public class ISQLiteDbInterface_iOS : ISQLiteInterface
    {
        public SQLiteConnection GetSQLiteConnection()
        {
            var dbName = "shopping_db.sqlite";
            var folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "Library");
            var fullPath = Path.Combine(folderPath, dbName);

            var connection = new SQLiteConnection(fullPath);
            return connection;
        }
    }
}