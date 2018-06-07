namespace WebServer
{
    using ByTheCakeApplication;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
    {
        private const int Port = 8000;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            // NOTE: To Start working with your DB - put '.' in the connection string   
            // instead of my DB server address !!!

            var mainApplication = new ByTheCakeApp();
            mainApplication.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(Port, appRouteConfig);
            webServer.Run();
        }
    }
}
