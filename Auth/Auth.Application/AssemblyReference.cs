using System.Reflection;

namespace Auth.Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

        public static readonly Assembly[] Assemblies = new[]
        {
            Assembly,
            typeof(DependencyInjection).Assembly,
        };
    }
}
