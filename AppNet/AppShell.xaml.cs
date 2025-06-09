using AppNet.Views;

namespace AppNet;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Paciente), typeof(Paciente));
        Routing.RegisterRoute(nameof(ListadoPaciente), typeof (ListadoPaciente));
    }
}
