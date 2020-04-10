namespace UniLink.Client.Site.Helper
{
    public class DataHelper
    {
        public const string URLBase = "localhost:5050/api";

        public const string DarkTheme = "background-color: #272727; color: white;";

        public const string LightTheme = "background-color: white; color: black;";

        public static string Theme { get; set; } = DarkTheme;
    }
}