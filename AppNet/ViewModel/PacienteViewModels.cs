using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AppNet.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppNet.ViewModels;

[QueryProperty("Id", "Id")]
[QueryProperty("Nombre", "Nombre")]
[QueryProperty("Apellido", "Apellido")]
[QueryProperty("Edad", "Edad")]
[QueryProperty("Peso", "Peso")]
[QueryProperty("Estatura", "Estatura")]
[QueryProperty("Sexo", "Sexo")]
[QueryProperty("NivelActividad", "NivelActividad")]
public partial class PacienteViewModels : ObservableValidator
{
    private readonly IPacientes pacienteService;

    public ObservableCollection<string> Errores { get; set; } = new();

    [ObservableProperty]
    private string resultado = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEnabled))]
    private bool isVisible;

    public bool IsEnabled => !IsVisible;

    [ObservableProperty]
    private int id;

    // Campos del paciente
    private string nombre = string.Empty;
    [Required(ErrorMessage = "El campo nombre es obligatorio")]
    [SQLite.MaxLength(50)]
    public string Nombre
    {
        get => nombre;
        set => SetProperty(ref nombre, value, true);
    }

    private string apellido = string.Empty;
    [Required(ErrorMessage = "El campo apellido es obligatorio")]
    [SQLite.MaxLength(50)]
    public string Apellido
    {
        get => apellido;
        set => SetProperty(ref apellido, value, true);
    }

    private int edad;
    [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120 años")]
    public int Edad
    {
        get => edad;
        set => SetProperty(ref edad, value, true);
    }

    private double peso;
    [Range(1, 500, ErrorMessage = "El peso debe estar entre 1 y 500 kg")]
    public double Peso
    {
        get => peso;
        set => SetProperty(ref peso, value, true);
    }

    private double estatura;
    [Range(50, 250, ErrorMessage = "La estatura debe estar entre 50 y 250 cm")]
    public double Estatura
    {
        get => estatura;
        set => SetProperty(ref estatura, value, true);
    }

    private string sexo = string.Empty;
    [Required(ErrorMessage = "El sexo es requerido")]
    public string Sexo
    {
        get => sexo;
        set => SetProperty(ref sexo, value, true);
    }

    private int nivelActividad = 1;
    [Range(1, 5, ErrorMessage = "El nivel de actividad debe estar entre 1 y 5")]
    public int NivelActividad
    {
        get => nivelActividad;
        set => SetProperty(ref nivelActividad, value, true);
    }

    // Opciones para los Pickers
    public List<string> OpcionesSexo { get; } = new() { "M", "F" };

    public List<NivelActividadModel> OpcionesActividad { get; } = new()
    {
        new() { Valor = 1, Descripcion = "Sedentario (poco o nada de ejercicio)" },
        new() { Valor = 2, Descripcion = "Ligero (ejercicio ligero 1-3 días/semana)" },
        new() { Valor = 3, Descripcion = "Moderado (ejercicio moderado 3-5 días/semana)" },
        new() { Valor = 4, Descripcion = "Activo (ejercicio intenso 6-7 días/semana)" },
        new() { Valor = 5, Descripcion = "Muy activo (ejercicio muy intenso, trabajo físico)" }
    };

    public PacienteViewModels()
    {
        pacienteService = App.Current.Services.GetService<IPacientes>();
    }

    [RelayCommand]
    public async Task GuardarPaciente()
    {
        IsBusy = true;
        IsVisible = false;
        ValidateAllProperties();

        Errores.Clear();
        GetErrors(nameof(Nombre)).ToList().ForEach(f => Errores.Add("Nombre: " + f.ErrorMessage));
        GetErrors(nameof(Apellido)).ToList().ForEach(f => Errores.Add("Apellido: " + f.ErrorMessage));
        GetErrors(nameof(Edad)).ToList().ForEach(f => Errores.Add("Edad: " + f.ErrorMessage));
        GetErrors(nameof(Peso)).ToList().ForEach(f => Errores.Add("Peso: " + f.ErrorMessage));
        GetErrors(nameof(Estatura)).ToList().ForEach(f => Errores.Add("Estatura: " + f.ErrorMessage));
        GetErrors(nameof(Sexo)).ToList().ForEach(f => Errores.Add("Sexo: " + f.ErrorMessage));
        GetErrors(nameof(NivelActividad)).ToList().ForEach(f => Errores.Add("Nivel Actividad: " + f.ErrorMessage));

        IsBusy = false;
        if (Errores.Count > 0) return;

        IsBusy = true;

        var paciente = new Paciente2()
        {
            Id = Id,
            Nombre = Nombre,
            Apellido = Apellido,
            Edad = Edad,
            Peso = Peso,
            Estatura = Estatura,
            Sexo = Sexo,
            NivelActividad = NivelActividad
        };

        try
        {
            if (Id == 0)
            {
                Id = await pacienteService.InsertPaciente(paciente);
                Resultado = $"Paciente guardado exitosamente. ID: {Id}";
            }
            else
            {
                await pacienteService.UpdatePaciente(paciente);
                Resultado = $"Paciente actualizado exitosamente. ID: {Id}";
            }
        }
        catch (Exception ex)
        {
            Resultado = $"Error al guardar: {ex.Message}";
        }

        IsBusy = false;
        IsVisible = true;

        await Task.Delay(2000);
        await Shell.Current.Navigation.PopToRootAsync();
    }

    [RelayCommand]
    public void LimpiarFormulario()
    {
        Id = 0;
        Nombre = string.Empty;
        Apellido = string.Empty;
        Edad = 0;
        Peso = 0;
        Estatura = 0;
        Sexo = string.Empty;
        NivelActividad = 1;
        Errores.Clear();
        Resultado = string.Empty;
        IsVisible = false;
    }
}

public class NivelActividadModel
{
    public int Valor { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}