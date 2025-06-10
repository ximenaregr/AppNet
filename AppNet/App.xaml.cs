using AppNet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppNet
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        public App()
        {
            var services = new ServiceCollection();
            Services = ConfigureServices(services);
            InitializeComponent();
            MainPage = new AppShell();
        }

        private static IServiceProvider ConfigureServices(ServiceCollection services)
        {
            // Services
            services.AddTransient<IPacientes, PacienteServices>();
            services.AddTransient<IDialogService, DialogService>();

            // ViewModels
            services.AddTransient<PacienteViewModels>();
            services.AddTransient<PacientesViewModel>();

            // Views
            services.AddSingleton<ListadoPaciente>();
            services.AddSingleton<Views.Paciente>();

            return services.BuildServiceProvider();
        }
    }
}