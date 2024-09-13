using Microsoft.EntityFrameworkCore;

namespace WSInformatica.Models
{
    public class InfoContext : DbContext
    {
        public InfoContext(DbContextOptions<InfoContext> options) : base(options)
        {
        }
        public InfoContext()
        {
            
        }
        public virtual DbSet<Arma> Arma { get; set; }
        public virtual DbSet<Automotor> Automotor { get; set; } 
        public virtual DbSet<Consulta> Consulta { get; set; } 
        public virtual DbSet<Dependencia> Dependencia { get; set; } 
        public virtual DbSet<Efectivo> Efectivo { get; set; } 
        public virtual DbSet<Persona> Persona { get; set; } 
        public virtual DbSet<TipoAutomotor> Tipo_Automotor { get; set; } 
        public virtual DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-J6464T0\\SQLEXPRESS;DataBase=Informatica;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Efectivo>()
                .HasOne(e => e.Dependencia)
                .WithMany(d => d.Efectivo)
                .HasForeignKey(e => e.IdDependencia);

            // Otros mapeos y configuraciones...

            base.OnModelCreating(modelBuilder);
        }

    }
}
