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
            public const string Seq = "Seq";
            public const string Redis = "Redis";
            public const string Postgresql1 = "Postgresql1";
            public const string Postgresql2 = "Postgresql2";
            public const string SecretManagement = "SecretManagement";
        }

    }

}
