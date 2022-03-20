namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string claimDatabaseType = "databasetype";
        public EDatabaseType DatabaseType;
        public EDatabaseType GetDeafultDatabaseType => EDatabaseType.SqlServer;
    }

    public enum EDatabaseType
    {
        SqlServer=1,
        MongoDb=2
    }
}
