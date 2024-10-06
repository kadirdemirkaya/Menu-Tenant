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
            public const string MenuDatabase = "MenuDatabases";
        }

        public static class Secrets
        {
            public const string DevelopmentSeq = "Development_Seq";
            public const string DevelopmentRedis = "Development_Redis";
            public const string DevelopmentRedis2 = "Development_Redis2";
            public const string DevelopmentSecretManagement = "Development_SecretManagement";
            public const string DevelopmentXabarihealthcheck = "Development_Xabarihealthcheck";

            public const string DevelopmentPOSTGRES_Host = "Development_POSTGRES_Host";
            public const string DevelopmentPOSTGRES_Port = "Development_POSTGRES_Port";
            public const string DevelopmentPOSTGRES_POSTGRES_User = "Development_POSTGRES_User";
            public const string DevelopmentPOSTGRES_POSTGRES_Password = "Development_POSTGRES_Password";
            public const string DevelopmentPOSTGRES_POSTGRES_AuthDb = "Development_POSTGRES_AuthDb";
            public const string DevelopmentPOSTGRES_POSTGRES_SharedDb = "Development_POSTGRES_SharedDb";
            public const string DevelopmentPOSTGRES_POSTGRES_SharedDb_TenantId = "23B7F25B-D97F-4B20-8608-7F2C55ACFA4E";
            public const string DevelopmentPOSTGRES_POSTGRES_Database = "Development_POSTGRES_Database";
            public const string DevelopmentPOSTGRES_POSTGRES_Auth_Url = "Development_POSTGRES_Auth_Url";
            public const string DevelopmentPOSTGRES_POSTGRES_Database_Url = "Development_POSTGRES_Database_Url";
            public const string DevelopmentPOSTGRES_POSTGRES_Shared_Url = "Development_POSTGRES_Shared_Url";

            public const string DevelopmentAuthApi = "Development_AuthApi";
            public const string DevelopmentTenantApi = "Development_TenantApi";
            public const string DevelopmentDatabaseApi = "Development_DatabaseApi";
            public const string DevelopmentHealtCheckApi = "Development_HealtCheckApi";
        }

        public static class Stream
        {
            public const string StreamKey = "mystream";
            public const string GroupName = "mygroup";
            public const string ConsumerName = "worker-consumer";
        }
    }
}
