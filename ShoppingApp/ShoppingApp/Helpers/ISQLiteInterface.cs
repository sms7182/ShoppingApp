using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ShoppingApp.Helpers
{
    public interface ISQLiteInterface
    {
        SQLiteConnection GetSQLiteConnection();
    }
}
