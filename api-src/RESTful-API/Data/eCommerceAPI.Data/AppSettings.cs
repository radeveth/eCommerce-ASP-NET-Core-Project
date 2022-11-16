namespace eCommerceAPI.Data
{
    using Newtonsoft.Json;

    public static class AppSettings
    {
        public static AppSettingsSchema GetApplicationSettings()
        {
            string jsonData = File.ReadAllText(@"appsettings.json");
            return JsonConvert.DeserializeObject<AppSettingsSchema>(jsonData);
        }

        public static ConnectionStringsSchema ConnectionStrings()
        {
            AppSettingsSchema appSettingsSchema = GetApplicationSettings();
            return appSettingsSchema.ConnectionStrings;
        }

        public static string DefaultConnection()
        {
            return ConnectionStrings().DefaultConnection;
        }

        public static string EcommerceApiDbContextConnection()
        {
            return ConnectionStrings().EcommerceApiDbContextConnection;
        }
    }

    public class AppSettingsSchema
    {
        public ConnectionStringsSchema ConnectionStrings { get; set; }
    }

    public class ConnectionStringsSchema
    {
        public string DefaultConnection { get; set; }

        public string EcommerceApiDbContextConnection { get; set; }
    }
}
