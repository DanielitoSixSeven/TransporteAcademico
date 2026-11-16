namespace TransporteAcademico.Models;

/// <summary>
/// Una parada del transporte académico. Más adelante las rutas
/// serán listas ordenadas de paradas.
/// </summary>
public record Parada(string Nombre, string Descripcion, double Latitud, double Longitud)
{
    public Location Ubicacion => new(Latitud, Longitud);
}