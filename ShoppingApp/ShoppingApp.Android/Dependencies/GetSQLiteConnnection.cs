using ShoppingApp.Droid.Dependancies;
using ShoppingApp.Helpers;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetSQLiteConnnection))]
namespace ShoppingApp.Droid.Dependancies
{
    public class GetSQLiteConnnection : ISQLiteInterface
    {
        public GetSQLiteConnnection()
        {
        }

        public SQLiteConnection GetSQLiteConnection()
        {

            var dbName = "shopping_db.sqlite";
            var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fullPath = Path.Combine(folderPath, dbName);      
            var connection = new SQLiteConnection(fullPath);
            return connection;
        }
    }
}