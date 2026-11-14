// Models/Estudiante.cs
namespace SistemaEscolarApi.Models;

public class Estudiante
{
    public int IdEstudiante { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; } = true;
    public List<Inscripcion> Inscripciones { get; set; } = new();
}
// Models/Materia.cs
namespace SistemaEscolarApi.Models;

public class Materia
{
    public int IdMateria { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int CupoDisponible { get; set; }
    public bool Activa { get; set; } = true;
    public List<Inscripcion> Inscripciones { get; set; } = new();
}
// Models/Inscripcion.cs
namespace SistemaEscolarApi.Models;

public class Inscripcion
{
    public int IdInscripcion { get; set; }
    public int IdEstudiante { get; set; }
    public Estudiante Estudiante { get; set; } = null!;
    public int IdMateria { get; set; }
    public Materia Materia { get; set; } = null!;
    public string Periodo { get; set; } = string.Empty;
    public DateTime FechaInscripcion { get; set; }
}