using System;
using Microsoft.Extensions.Configuration;

namespace LibraryService.WebAPI.Data
{
    internal static class DatabaseConnection
    {
        public static string Resolve(IConfiguration configuration)
        {
            var connectionString =
                configuration["Supabase:ConnectionString"] ??
                configuration.GetConnectionString("DefaultConnection") ??
                Environment.GetEnvironmentVariable("DATABASE_URL");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "No hay cadena de conexion a PostgreSQL. En Railway configure la variable " +
                    "Supabase__ConnectionString (Npgsql) o DATABASE_URL.");
            }

            return Normalize(connectionString.Trim());
        }

        private static string Normalize(string connectionString)
        {
            if (connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
                connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
            {
                return ConvertPostgresUriToNpgsql(connectionString);
            }

            return connectionString;
        }

        private static string ConvertPostgresUriToNpgsql(string uriValue)
        {
            var uri = new Uri(uriValue);
            var userInfo = uri.UserInfo.Split(':', 2);
            var username = Uri.UnescapeDataString(userInfo[0]);
            var password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty;
            var database = uri.AbsolutePath.TrimStart('/');

            return
                $"Host={uri.Host};Port={uri.Port};Database={database};Username='{username}';Password='{password}';SSL Mode=Require;Trust Server Certificate=true";
        }
    }
}
