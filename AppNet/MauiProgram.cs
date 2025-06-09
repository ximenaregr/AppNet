using AppNet.ViewModels;
using AppNet.Views;
using Microsoft.Extensions.DependencyInjection;
using AppNet.Interfaces;
using AppNet.Services;

namespace AppNet;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //SQL Lite
        string dbPath = FileAccessHelper.GetPathFile("pacientes.db3");
        //	builder.Services.AddSingleton<AlumnosModels>(s => ActivatorUtilities.CreateInstance<AlumnosModels>(s, dbPath));



        return builder.Build();
    }

}
