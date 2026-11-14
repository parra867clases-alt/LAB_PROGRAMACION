using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;

namespace SistemaEscolarApi.Data;

public class EscuelaContext : DbContext
{
    public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options) { }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Estudiante>(e =>
        {
            e.ToTable("Estudiantes");
            e.Property(x => x.Matricula).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            e.Property(x => x.Correo).HasMaxLength(150).IsRequired();
            e.Property(x => x.FechaRegistro).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            e.HasIndex(x => x.Matricula).IsUnique();
            e.HasIndex(x => x.Correo).IsUnique();
        });

        mb.Entity<Materia>(e =>
        {
            e.ToTable("Materias", t => t.HasCheckConstraint("CHK_Materias_Cupo", "CupoDisponible >= 0"));
            e.Property(x => x.Clave).HasMaxLength(15).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.Clave).IsUnique();
        });

        mb.Entity<Inscripcion>(e =>
        {
            e.ToTable("Inscripciones");
            e.Property(x => x.Periodo).HasMaxLength(10).IsRequired();
            e.Property(x => x.FechaInscripcion).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            e.HasIndex(x => new { x.IdEstudiante, x.IdMateria, x.Periodo }).IsUnique();
            e.HasOne(x => x.Estudiante).WithMany(x => x.Inscripciones)
                .HasForeignKey(x => x.IdEstudiante).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Materia).WithMany(x => x.Inscripciones)
                .HasForeignKey(x => x.IdMateria).OnDelete(DeleteBehavior.Restrict);
        });
    }
}