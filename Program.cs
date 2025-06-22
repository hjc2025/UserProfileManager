using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using UserProfileManager.Presenters;
using UserProfileManager.Services;
using UserProfileManager.Services.Implementations;
using UserProfileManager.Strategies;
using UserProfileManager.Strategies.Implementations;
using UserProfileManager.Views;

namespace UserProfileManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Set up the generic host. This gives us DI, logging, and configuration for free.
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // Add our appsettings.json file to the configuration
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();

            // To make WinForms work with the generic host, we need to run it this way.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // We must resolve the Presenter from the container first.
            // This act of creation triggers its constructor, which in turn subscribes
            // to the (also created) singleton View's events.
            host.Services.GetRequiredService<ProfilePresenter>();

            // Now, we resolve the main form (which is already created as a singleton
            // dependency of the presenter) and run it.
            // Get the main form from the DI container and run it.
            var mainForm = host.Services.GetRequiredService<ProfileForm>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Registering Services and Repositories
            // Use Scoped or Transient for services that might hold state. Singleton is fine here.
            services.AddSingleton<INotificationService, MessageBoxNotificationService>();
            services.AddSingleton<IUserProfileRepository, InMemoryUserProfileRepository>();

            // Registering all Strategy implementations
            // The DI container will automatically find all of these when a collection is requested.
            services.AddSingleton<ISavingStrategy, JsonSavingStrategy>();
            services.AddSingleton<ISavingStrategy, XmlSavingStrategy>();

            // Registering the Factory that depends on the collection of strategies
            services.AddSingleton<SavingStrategyFactory>();

            // Registering the MVP components
            // The View (Form) is registered so the DI container can create it.
            // The Presenter needs to know about its View. To solve this chicken-and-egg problem,
            // we register the View and then use a factory function to create the Presenter.
            services.AddSingleton<ProfileForm>(); // Register the concrete form
            services.AddSingleton<IProfileView>(provider => provider.GetRequiredService<ProfileForm>()); // Register the form as its interface

            // Register the Presenter. The container will automatically inject the IProfileView
            // (which is the ProfileForm) and all other dependencies into its constructor.
            services.AddSingleton<ProfilePresenter>();
        }
    }
}