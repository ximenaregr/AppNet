using AppNet.ViewModels;

namespace AppNet.Views;

public partial class Paciente : ContentPage
{
	public Paciente()
	{
		BindingContext = App.Current.Services.GetService<PacienteViewModels>();
		InitializeComponent();
	}
}