using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace NHibernate.NetCore.DependencyInjection
{
    public static class Builder
    {
        private static IConfiguration _configuration;
        private static IServiceCollection _services;

        public static FluentConfiguration AddHibernate(this IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            return Fluently.Configure();
        }

        public static FluentConfiguration UseSqlDatabaseWithConnectionStringName(this FluentConfiguration configuration, string connectionStringName)
        {
            var connectionString = _configuration.GetConnectionString(connectionStringName);

            return configuration.Database(() =>
                FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
                    .ShowSql()
                    .ConnectionString(connectionString));
        }

        public static FluentConfiguration WithMappingsFromAssemblyOf<T>(this FluentConfiguration configuration) where T : class
        {
            return configuration.Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>());
        }

        public static FluentConfiguration WithMappingsFromAssembly(this FluentConfiguration configuration, Assembly assembly)
        {
            return configuration.Mappings(m => m.FluentMappings.AddFromAssembly(assembly));
        }

        public static FluentConfiguration ConfigureMappings(this FluentConfiguration configuration, Action<MappingConfiguration> mappings)
        {
            return configuration.Mappings(mappings);
        }

        public static FluentConfiguration Build(this FluentConfiguration configuration)
        {
            _services.AddSingleton(f => configuration.BuildSessionFactory());

            _services.AddScoped(s => s.GetService<ISessionFactory>().OpenSession());

            return configuration;
        }
    }

}
