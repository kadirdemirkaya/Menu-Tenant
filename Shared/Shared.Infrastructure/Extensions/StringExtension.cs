﻿using System.Security.Cryptography;
using System.Text;

namespace Shared.Infrastructure.Extensions
{
    public static class StringExtension
    {
        private static Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private static string GenerateRandomWord(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        public static string GenerateRandomDbName(int length = 7)
        {
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(_chars.Length);
                result.Append(_chars[index]);
            }
            return result.ToString();
        }

        public static string GenerateTemporaryDatabaseName()
        {
            string randomWord = GenerateRandomWord(6);
            string tempDbName = $"personaldb_{randomWord}";
            return tempDbName;
        }

        public static bool IsTemporaryDatabaseName(string dbName)
        {
            return dbName.StartsWith("personaldb_");
        }

        public static string SetDbUrl(string host, string port, string userName, string password, string databaseName)
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
