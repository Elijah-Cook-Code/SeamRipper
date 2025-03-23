using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SeamRipperData;
using SeamRipperData.Services;
using MudBlazor.Services; // ✅ Import MudBlazor
using SeamRipperData.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using SeamRipperData.Connections;
using MudBlazor;
using MudBlazor.Extensions;





namespace SeamRipper
{
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
                });

            builder.Services.AddMauiBlazorWebView();       
            builder.Services.AddMudServices(); // ✅ Add MudBlazor Services



            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "SeamRipper.db");
            Console.WriteLine($"📂 Database Path: {dbPath}"); // Print the actual location
            Debug.WriteLine($"📂 Database Path: {dbPath}"); // Print to Debug Output
            builder.Services.AddDbContext<ClientContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped<ClientServices>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>(); // ✅ Register repository


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();



            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ClientContext>();
                db.Database.EnsureCreated(); // ✅ This ensures tables exist
                db.Database.Migrate();
            }
            //RunTestAsync(app.Services).Wait();



            return app;
        }
        //private static async Task RunTestAsync(IServiceProvider services)
        //{
        //    await TestClientRepository.RunTests(services);
        //}
    }
}
