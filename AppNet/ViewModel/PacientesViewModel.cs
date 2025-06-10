using AppNet.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AppNet.ViewModels;

public partial class PacientesViewModel : ObservableObject
{
    private readonly IPacientes _pacientesService;
    private readonly IDialogService _dialog;

    public ObservableCollection<Paciente2> Pacientes { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReady))]
    private bool isLoading;

    [ObservableProperty]
    bool isRefreshing;

    public bool IsReady => !IsLoading;

    public PacientesViewModel()
    {
        _pacientesService = App.Current.Services.GetService<IPacientes>();
        _dialog = App.Current.Services.GetService<IDialogService>();
        Task.Run(async () => await ListarPacientes());
    }

    [RelayCommand]
    public async Task ListarPacientes()
    {
        IsLoading = true;
        Pacientes.Clear();

        try
        {
            var lista = await _pacientesService.GetAll();
            foreach (var item in lista)
                Pacientes.Add(item);
        }
        catch (Exception ex)
        {
            await _dialog.ShowAlertAsync("Error", $"Error al cargar pacientes: {ex.Message}", "OK", "");
        }

        IsLoading = false;
        IsRefreshing = false;
    }

    [RelayCommand]
    public async Task EditarPaciente(Paciente2 paciente)
    {
        var navigationParameter = $"Id={paciente.Id}&Nombre={paciente.Nombre}&Apellido={paciente.Apellido}" +
                                $"&Edad={paciente.Edad}&Peso={paciente.Peso}&Estatura={paciente.Estatura}" +
                                $"&Sexo={paciente.Sexo}&NivelActividad={paciente.NivelActividad}";

        await Shell.Current.GoToAsync($"/Paciente?{navigationParameter}");
    }

    [RelayCommand]
    public async Task EliminarPaciente(Paciente2 paciente)
    {
        IsLoading = true;
        var resultado = await _dialog.ShowAlertAsync(
            "Eliminar",
            $"¿Desea eliminar el registro de {paciente.NombreCompleto}?",
            "Eliminar",
            "Cancelar");

        if (resultado)
        {
            try
            {
                await _pacientesService.DeletePaciente(paciente);
                await ListarPacientes();
                await _dialog.ShowAlertAsync("Éxito", "Paciente eliminado correctamente", "OK", "");
            }
            catch (Exception ex)
            {
                await _dialog.ShowAlertAsync("Error", $"Error al eliminar: {ex.Message}", "OK", "");
            }
        }

        IsLoading = false;
    }

    [RelayCommand]
    public async Task AgregarNuevo()
    {
        await Shell.Current.GoToAsync("/Paciente");
    }

    [RelayCommand]
    public async Task VerDetalles(Paciente2 paciente)
    {
        var mensaje = $"Nombre: {paciente.NombreCompleto}\n" +
                     $"Edad: {paciente.Edad} años\n" +
                     $"Peso: {paciente.Peso} kg\n" +
                     $"Estatura: {paciente.Estatura} cm\n" +
                     $"Sexo: {(paciente.Sexo == "M" ? "Masculino" : "Femenino")}\n" +
                     $"Actividad: {paciente.DescripcionActividad}\n\n" +
                     $"=== RESULTADOS ===\n" +
                     $"IMC: {paciente.IMC} ({paciente.ClasificacionIMC})\n" +
                     $"% Grasa: {paciente.PorcentajeGrasa}% ({paciente.ClasificacionGrasa})\n" +
                     $"Peso Ideal: {paciente.PesoIdeal} kg\n" +
                     $"BMR: {paciente.BMR} kcal/día\n" +
                     $"TDEE: {paciente.TDEE} kcal/día";

        await _dialog.ShowAlertAsync($"Detalles - {paciente.NombreCompleto}", mensaje, "Cerrar", "");
    }
}