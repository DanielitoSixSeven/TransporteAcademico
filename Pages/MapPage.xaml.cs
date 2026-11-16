using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using TransporteAcademico.Models;

namespace TransporteAcademico.Pages;

public partial class MapPage : ContentPage
{
    // TODO: cambia estas coordenadas por las de tu campus.
    private const double CampusLatitud = 19.4326;
    private const double CampusLongitud = -99.1332;

    // TODO: reemplaza estas paradas de ejemplo por las reales.
    private static readonly List<Parada> Paradas =
    [
        new("Campus Central", "Parada principal - Línea 1", CampusLatitud, CampusLongitud),
        new("Biblioteca", "Parada Línea 1", CampusLatitud + 0.0030, CampusLongitud + 0.0020),
        new("Facultad de Ingeniería", "Parada Línea 1", CampusLatitud - 0.0025, CampusLongitud + 0.0035),
    ];

    public MapPage()
    {
        InitializeComponent();
        CargarParadas();
        CentrarEnCampus();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ActivarUbicacionUsuarioAsync();
    }

    private void CargarParadas()
    {
        MapaTransporte.Pins.Clear();

        foreach (var parada in Paradas)
        {
            MapaTransporte.Pins.Add(new Pin
            {
                Label = parada.Nombre,
                Address = parada.Descripcion,
                Location = parada.Ubicacion,
                Type = PinType.Place
            });
        }
    }

    private void CentrarEnCampus()
    {
        var centro = new Location(CampusLatitud, CampusLongitud);
        MapaTransporte.MoveToRegion(MapSpan.FromCenterAndRadius(centro, Distance.FromKilometers(1)));
    }

    private async Task ActivarUbicacionUsuarioAsync()
    {
        try
        {
            var estado = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (estado != PermissionStatus.Granted)
                estado = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            MapaTransporte.IsShowingUser = estado == PermissionStatus.Granted;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"No se pudo activar la ubicación: {ex.Message}");
        }
    }
}