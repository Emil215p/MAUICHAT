using Microsoft.Extensions.Logging;
//using MySqlConnector;
//using var connection = new MySqlConnection();
////connection.ConnectionString = "Server=localhost;User ID=root;Database=test"; //XAMPP connection
//connection.ConnectionString = ""; //connection to server
//await connection.OpenAsync();

//using var command = new MySqlCommand("SELECT name FROM table;", connection);
//using var reader = command.ExecuteReader();
//while (reader.Read())
//    Console.WriteLine(reader.GetString(0));

namespace MAUICHAT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
