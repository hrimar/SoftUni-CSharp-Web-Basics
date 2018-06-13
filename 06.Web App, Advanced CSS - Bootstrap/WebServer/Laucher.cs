namespace WebServer
{
    using ByTheCakeApplication;
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using WebServer.GameStoreApplication;

    public class Launcher : IRunnable
    {
        private const int Port = 8000;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {            
            //    var mainApplication = new ByTheCakeApp();

            var mainApplication = new GameStoreApp();
           mainApplication.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(Port, appRouteConfig);
            webServer.Run();
        }
    }
}
