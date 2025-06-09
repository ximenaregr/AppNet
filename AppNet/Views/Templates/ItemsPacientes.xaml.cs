namespace AppNet.Views.Templates;

public partial class ItemsPacientes : ContentView
{
    private readonly PacienteViewModels viewmodel;
    public ItemsPacientes()
    {
        try
        {



            viewmodel = App.Current.Services.GetService<PacienteViewModels>();
            InitializeComponent();
            //  BindingContext = viewmodel;

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
}