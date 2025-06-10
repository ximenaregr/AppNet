namespace AppNet.Views;

public partial class ListadoPaciente : ContentPage
{
    public ListadoPaciente()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<PacientesViewModel>();
    }
}