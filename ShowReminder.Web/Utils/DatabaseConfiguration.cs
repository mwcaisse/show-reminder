namespace ShowReminder.Web.Utils
{
    public class DatabaseConfiguration
    {
        public string Host { get; set; }
        
        public string User { get; set; }
        
        public string Password { get; set; }
        
        public string Port { get; set; }
        
        public string Schema { get; set; }
        
        public string SslMode { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"server={Host};userid={User};pwd={Password};port={Port};database={Schema};sslmode={SslMode};";
            }
        }
    }
}