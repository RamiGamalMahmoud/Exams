using Dotmim.Sync;
using Exams.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Windows;

namespace Exams
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string fireBaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");

                    var url = context.Configuration.GetValue<string>("SUPABASE_URL");
                    var key = context.Configuration.GetValue<string>("SUPABASE_KEY");

                    var options = new Supabase.SupabaseOptions
                    {
                        AutoRefreshToken = true,
                        AutoConnectRealtime = true,
                        // SessionHandler = new SupabaseSessionHandler() <-- This must be implemented by the developer
                    };

                    services.AddSingleton(provider => new Supabase.Client(url, key, options));

                    services.AddSingleton<MainWindow>(services => new MainWindow());

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var serverProvider = new Dotmim.Sync.PostgreSql.NpgsqlSyncProvider("User Id=postgres.ptwapmvfpausfvhdvhng;Password=YGtJQKD5uJHJcT6I;Server=aws-0-ap-south-1.pooler.supabase.com;Port=5432;Database=postgres;");
            var clientProvider = new Dotmim.Sync.Sqlite.SqliteSyncProvider("Data Source=data.db;");

            var setup = new SyncSetup("categories", "questions");

            var agent = new SyncAgent(clientProvider, serverProvider);
            var syncContext = await agent.SynchronizeAsync(setup);

            //AppDbContextFactory factory = new AppDbContextFactory("User Id=postgres.ptwapmvfpausfvhdvhng;Password=YGtJQKD5uJHJcT6I;Server=aws-0-ap-south-1.pooler.supabase.com;Port=5432;Database=postgres;");
            //AppDbContext context = factory.CreateDbContext();

            //bool canConnect = context.Database.CanConnect();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            //Supabase.Client client = _host.Services.GetRequiredService<Supabase.Client>();
            //var response = await client
            //    .From<Question>()
            //    .Get();
            //var models = response.Models;

            MainWindow.Show();
            base.OnStartup(e);
        }

        private readonly IHost _host;
    }

}
