using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Repositories;

namespace Toshi.Backend.Infraestructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToshiDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToshiDBContext")!)
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICampaniaRepository, CampaniaRepository>();
            services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<IEstandarRepository, EstandarRepository>();
            services.AddScoped<IIngresoPolloRepository, IngresoPolloRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IOfflineRepository, OfflineRepository>();
            services.AddScoped<IPlantelRepository, PlantelRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<ISalidaProductoRepository, SalidaProductoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IIngresoProductoRepository, IngresoProductoRepository>();

            return services;
        }
    }
}
