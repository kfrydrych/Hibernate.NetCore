using Microsoft.Extensions.DependencyInjection;

namespace UnitOfWork.NetCore.NHibernate
{
    public static class DependencyInjection
    {
        public static void AddHibernateUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
