using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Models
{
    public static class Constants
    {
        public static class StackManagement
        {
            public const string postgresql1 = "postgresql1";
        }

        public static class Tables
        {
            public const string Product = "Products";
            public const string Menu = "Menus";
            public const string User = "Users";
            public const string Company = "Companies";
            public const string ConnectionPool = "ConnectionPools";
        }

        public static class Secrets
        {
            public const string DevelopmentSeq = "Development_Seq";
            public const string DevelopmentRedis = "Development_Redis";
            public const string DevelopmentSecretManagement = "Development_SecretManagement";

            public const string DevelopmentPostgresAuthHost = "Development_PostgresAuth_Host";
            public const string DevelopmentPostgresAuthPort = "Development_PostgresAuth_Port";
            public const string DevelopmentPostgresAuthUserName = "Development_PostgresAuth_UserName";
            public const string DevelopmentPostgresAuthPassword = "Development_PostgresAuth_Password";
            public const string DevelopmentPostgresAuthDatabaseName = "Development_PostgresAuth_DatabaseName";
        }

    }

}
