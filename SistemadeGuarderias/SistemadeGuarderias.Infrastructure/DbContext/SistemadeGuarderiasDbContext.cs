using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Domain.Entities;

namespace SistemadeGuarderias.Infrastructure
{
    public class SistemadeGuarderiasDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-H3L6RGC;Database=SistemadeGuarderiasDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        public SistemadeGuarderiasDbContext(DbContextOptions<SistemadeGuarderiasDbContext> options) : base(options) { }

        public SistemadeGuarderiasDbContext() { }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Guarderia> Guarderias { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Nino> Ninos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Tutor> Tutores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Relaciones
            modelBuilder.Entity<Nino>()
                .HasOne(n => n.Actividad)
                .WithMany(a => a.Ninos)
                .HasForeignKey(n => n.ActividadId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Nino>()
                .HasOne(n => n.Tutor)
                .WithMany(t => t.Ninos)
                .HasForeignKey(n => n.TutorId);

            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.Guarderia)
                .WithMany(g => g.Actividades)
                .HasForeignKey(a => a.GuarderiaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Nino>()
                .HasMany(n => n.Asistencias)
                .WithOne(a => a.Nino)
                .HasForeignKey(a => a.NinoId);

            modelBuilder.Entity<Guarderia>()
                .HasMany(g => g.Asistencias)
                .WithOne(a => a.Guarderia)
                .HasForeignKey(a => a.GuarderiaId);

            modelBuilder.Entity<Guarderia>()
                .HasMany(g => g.Actividades)
                .WithOne(a => a.Guarderia)
                .HasForeignKey(a => a.GuarderiaId);

            modelBuilder.Entity<Guarderia>()
                .HasMany(g => g.Pagos)
                .WithOne(p => p.Guarderia)
                .HasForeignKey(p => p.GuarderiaId)
                .OnDelete(DeleteBehavior.Restrict);

            //Configuracion Actividad
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.Property(a => a.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(a => a.Fecha)
                    .IsRequired();

                entity.Property(a => a.Hora)
                    .IsRequired();

                entity.HasOne(a => a.Guarderia)
                    .WithMany(g => g.Actividades)
                    .HasForeignKey(a => a.GuarderiaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //Configuracion Asistencia
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.Property(a => a.Presente)
                    .IsRequired();

                entity.Property(a => a.Fecha)
                    .IsRequired();

                entity.HasOne(a => a.Nino)
                    .WithMany(n => n.Asistencias)
                    .HasForeignKey(a => a.NinoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Guarderia)
                    .WithMany(g => g.Asistencias)
                    .HasForeignKey(a => a.GuarderiaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //Configuracion Guarderia
            modelBuilder.Entity<Guarderia>(entity =>
            {
                entity.Property(g => g.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(g => g.Direccion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(g => g.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasMany(g => g.Ninos)
                    .WithOne(n => n.Guarderia)
                    .HasForeignKey(n => n.GuarderiaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(g => g.Asistencias)
                    .WithOne(a => a.Guarderia)
                    .HasForeignKey(a => a.GuarderiaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(g => g.Actividades)
                    .WithOne(a => a.Guarderia)
                    .HasForeignKey(a => a.GuarderiaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(g => g.Mensajes)
                    .WithOne(m => m.Guarderia)
                    .HasForeignKey(m => m.GuarderiaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(g => g.Nombre);
            });

            //Configuracion Mensajes
            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.Property(m => m.Contenido)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(m => m.Fecha)
                    .IsRequired();

                entity.Property(m => m.Hora)
                    .IsRequired();

            });

            //Configuracion Nino
            modelBuilder.Entity<Nino>(entity =>
            {
                entity.Property(n => n.Nombre)
                     .IsRequired()
                     .HasMaxLength(100);

                entity.Property(n => n.Apellido)
                     .IsRequired()
                     .HasMaxLength(100);

                entity.Property(n => n.Edad)
                     .IsRequired();

                entity.HasOne(n => n.Tutor)
                     .WithMany(t => t.Ninos)
                     .HasForeignKey(n => n.TutorId)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(n => n.Guarderia)
                     .WithMany(g => g.Ninos)
                     .HasForeignKey(n => n.GuarderiaId)
                     .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(n => n.Pagos)
                    .WithOne(p => p.Nino)
                    .HasForeignKey(p => p.NinoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(n => n.Mensajes)
                        .WithOne(m => m.Nino)
                        .HasForeignKey(m => m.NinoId)
                        .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(n => n.Nombre);
            });

            //Configuracion Pago
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(p => p.Monto)
                    .IsRequired()
                    .HasPrecision(18, 2);

                entity.Property(p => p.Fecha)
                    .IsRequired();

                entity.Property(p => p.Pagado)
                    .IsRequired();

                entity.HasOne(p => p.Nino)
                    .WithMany(n => n.Pagos)
                    .HasForeignKey(p => p.NinoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Tutor)
                    .WithMany(t => t.Pagos)
                    .HasForeignKey(p => p.TutorId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            //Configuracion Tutor
            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.Property(t => t.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(t => t.Cedula)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(t => t.CorreoElectronico)
                    .HasMaxLength(100);

                entity.HasMany(t => t.Mensajes)
                    .WithOne(m => m.Tutor)
                    .HasForeignKey(m => m.TutorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => t.Cedula).IsUnique();
            });

        }
    }
}




