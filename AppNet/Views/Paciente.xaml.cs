using AppNet.ViewModels;

namespace AppNet.Views;

public partial class Paciente : ContentPage
{
	public Paciente()
	{
		BindingContext = App.Current.Services.GetService<PacienteViewModels>();
		InitializeComponent();
	}

	private async void OnCancelarClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}

