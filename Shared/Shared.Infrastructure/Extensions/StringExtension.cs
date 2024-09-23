namespace Shared.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string SetDbUrl(this string str, string host, string port, string userName, string password, string databaseName)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host), "Host cannot be null or empty.");

            if (string.IsNullOrEmpty(port))
                throw new ArgumentNullException(nameof(port), "Port cannot be null or empty.");

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "User Id cannot be null or empty.");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

            if (string.IsNullOrEmpty(databaseName))
                throw new ArgumentNullException(nameof(databaseName), "Database name cannot be null or empty.");

            string connectionString = "".SetServer(host).SetPort(port).SetDatabase(databaseName).SetUserId(userName).SetPassword(password).BuildConnectionString();

            return connectionString;
        }
        public static string SetServer(this string connectionString, string host)
        {
            return $"{connectionString}Server={host};";
        }

        public static string SetPort(this string connectionString, string port)
        {
            return $"{connectionString}Port={port};";
        }

        public static string SetDatabase(this string connectionString, string databaseName)
        {
            return $"{connectionString}Database={databaseName};";
        }

        public static string SetUserId(this string connectionString, string userId)
        {
            return $"{connectionString}User Id={userId};";
        }

        public static string SetPassword(this string connectionString, string password)
        {
            return $"{connectionString}Password={password};";
        }

        public static string BuildConnectionString(this string connectionString)
        {
            return connectionString.TrimEnd(';'); // Sonundaki noktalı virgülü kaldır
        }

    }
}
