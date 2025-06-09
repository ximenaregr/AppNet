using AppNet.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AppNet.ViewModels;
public partial class PacientesViewModel : ObservableObject
{
    private readonly IPacientes _pacientesservice;
    private readonly IDialogService _dialog;
    public ObservableCollection<Paciente2> Pacientes { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReady))]
    private bool isLoading;

    [ObservableProperty]
    bool isRefreshing;
    public bool IsReady => !IsLoading;


    public Paciente2()
    {
        _pacientesservice = App.Current.Services.GetService<IPacientes>();
        _dialog = App.Current.Services.GetService<IDialogService>();
        Task.Run(async () => await ListarPacientes());

    }


    [RelayCommand]
    public async Task ListarPacientes()
    {
        IsLoading = true;
        Pacientes.Clear();
        var lista = await _service.GetAll();
        foreach (var item in lista) Pacientes.Add(item);
        IsLoading = false;
        IsRefreshing = false;
    }


    [RelayCommand]
    public async Task EditarPaciente(Paciente2 paciente)
    {

        await Shell.Current.GoToAsync($"/Paciente?Id={paciente.Id}&Nombre={paciente.Nombre}&Apellido={paciente.Apellido}", false);

    }

    [RelayCommand]
    public async Task EliminarAlumno(Paciente2 paciente)
    {

        IsLoading = true;
        var res = await _dialog.ShowAlertAsync("Eliminar", $"Desea eliminiar el registro {paciente.Id}", "Aceptar", "Cancelar");
        if (!res) return;
        var A = await _pacientesservice.DeleteAlumno(paciente);
        await Listar();

    }

    [RelayCommand]
    public async Task AddNew()
    {

        await Shell.Current.Navigation.PushAsync(new Paciente(), false);
    }


}

