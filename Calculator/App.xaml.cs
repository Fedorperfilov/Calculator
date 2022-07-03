using CalculatorLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Хост.
        /// </summary>
        private readonly IHost _Host;

        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        public App()
        {
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();
            _Host = Host.CreateDefaultBuilder().ConfigureServices((context, services) => {
                ConfigureServices(services);
            }).Build();
        }

        /// <summary>
        /// Настройка сервисов для хоста.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CalcWindow>();
            services.AddTransient<BaseCalculatorModel>();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

        /// <summary>
        /// Событие при запуске приложения.
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _Host.StartAsync();

            var mainWindow = _Host.Services.GetRequiredService<CalcWindow>();
            mainWindow.Show(); 

            base.OnStartup(e);
        }

        /// <summary>
        /// Событие при выходе из приложения.
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnExit(ExitEventArgs e)
        {
            using (_Host)
            {
                await _Host.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
