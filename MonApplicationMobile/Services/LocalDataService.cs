using MonApplicationMobile.Models;

public class LocalDataService
{
    private readonly string _databasePath;

    public LocalDataService(string databasePath)
    {
        _databasePath = databasePath;
    }

    public void InitializeDatabase()
    {
        using (var db = new SQLite.SQLiteConnection(_databasePath))
        {
            db.CreateTable<LocalArticle>();
        }
    }
}
