using Microsoft.EntityFrameworkCore;
using Toshi.Backend.Domain;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data
{
    public class ToshiDBContext : DbContext, IDatabaseService
    {
        private readonly SessionStorage session;

        public ToshiDBContext(DbContextOptions<ToshiDBContext> options, SessionStorage sessionStorage) : base(options)
        {
            session = sessionStorage;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;

                        entry.Entity.cod_estado = false;
                        entry.Entity.fec_update = BaseDomainModel.GetNow();
                        entry.Entity.usu_update = session?.GetUser()?.CodUsuario;
                        break;
                    case EntityState.Modified:
                        entry.Entity.cod_estado = true;
                        entry.Entity.fec_update = BaseDomainModel.GetNow();
                        entry.Entity.usu_update = session?.GetUser()?.CodUsuario;
                        break;
                    case EntityState.Added:
                        entry.Entity.cod_estado = true;
                        entry.Entity.fec_insert = BaseDomainModel.GetNow();
                        entry.Entity.usu_insert = session?.GetUser()?.CodUsuario;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToshiDBContext).Assembly);
        }

        public virtual DbSet<CampaniaEntity> Campania { get; set; }
        public virtual DbSet<ConfiguracionEntity> Configuracion { get; set; }
        public virtual DbSet<ConfiguracionDetalleEntity> ConfiguracionDetalle { get; set; }
        public virtual DbSet<ContraseniaEntity> Contrasenia { get; set; }
        public virtual DbSet<EnvioDiarioEntity> EnvioDiario { get; set; }
        public virtual DbSet<EstandarEntity> Estandar { get; set; }
        public virtual DbSet<IngresoPolloEntity> IngresoPollo { get; set; }
        public virtual DbSet<IngresoPolloImagenEntity> IngresoPolloImagen { get; set; }
        public virtual DbSet<IngresoProductoEntity> IngresoProducto { get; set; }
        public virtual DbSet<IngresoProductoImagenEntity> IngresoProductoImagen { get; set; }
        public virtual DbSet<LicenciaEntity> Licencia { get; set; }
        public virtual DbSet<MenuEntity> Menu { get; set; }
        public virtual DbSet<PersonaEntity> Persona { get; set; }
        public virtual DbSet<PlantelEntity> Plantel { get; set; }
        public virtual DbSet<ProductoEntity> Producto { get; set; }
        public virtual DbSet<ProveedorEntity> Proveedor { get; set; }
        public virtual DbSet<ProveedorPersonalEntity> ProveedorPersonal { get; set; }
        public virtual DbSet<RolEntity> Rol { get; set; }
        public virtual DbSet<RolMenuEntity> RolMenu { get; set; }
        public virtual DbSet<SalidaProductoEntity> SalidaProducto { get; set; }
        public virtual DbSet<SalidaProductoImagenEntity> SalidaProductoImagen { get; set; }
        public virtual DbSet<SolContraseniaEntity> SolContrasenia { get; set; }
        public virtual DbSet<TokenEntity> Token { get; set; }
        public virtual DbSet<UsuarioEntity> Usuario { get; set; }
        public virtual DbSet<UsuarioPlantelEntity> UsuarioPlantel { get; set; }
    }
}
