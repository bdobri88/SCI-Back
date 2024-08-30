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
        public virtual DbSet<Arma> Armas { get; set; }
        public virtual DbSet<Automotor> Automotors { get; set; } 
        public virtual DbSet<Consulta> Consulta { get; set; } 
        public virtual DbSet<Dependencia> Dependencia { get; set; } 
        public virtual DbSet<Efectivo> Efectivos { get; set; } 
        public virtual DbSet<Persona> Personas { get; set; } 
        public virtual DbSet<TipoAutomotor> TipoAutomotors { get; set; } 
        public virtual DbSet<User> Users { get; set; }


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
                .WithMany(d => d.Efectivos)
                .HasForeignKey(e => e.IdDependencia);

            // Otros mapeos y configuraciones...

            base.OnModelCreating(modelBuilder);
        }

    }
}
