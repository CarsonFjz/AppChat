using Topshelf;

namespace AppChat.QueService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AppChatService>(s =>
                {
                    s.ConstructUsing(name => new AppChatService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("AppChat Service");
                x.SetDisplayName("AppChat Service");
                x.SetServiceName("AppChat");
            });
        }
    }
}
