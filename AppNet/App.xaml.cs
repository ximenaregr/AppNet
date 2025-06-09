
using AppNet.Services;
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

            services.AddTransient<IPacientes, PacienteServices>();
            //ViewModels
            services.AddTransient<PacienteViewModels>();
            services.AddTransient<PacienteViewModels>();

            //Views
            services.AddSingleton<ListadoPacientes>();
            services.AddSingleton<Paciente>();

            //Services
            services.AddTransient<IPacientes, PacienteServices>();
            services.AddTransient<IDialogService, DialogService>();


            return services.BuildServiceProvider();
        }
    }
}
