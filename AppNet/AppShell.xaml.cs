using AppNet.Views;

namespace AppNet;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.Paciente), typeof(Views.Paciente));
        Routing.RegisterRoute(nameof(ListadoPaciente), typeof(ListadoPaciente));
    }
}