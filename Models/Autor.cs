namespace BreidyVasquez_p1_p4.Api.Models;

public class Autor
{
    public int IdAutor { get; set; }

    public string Nombres { get; set; } = string.Empty;

    public string Nacionalidad { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public decimal Sueldo { get; set; }
}