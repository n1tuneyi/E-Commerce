namespace Ecommerce;

public class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection()
                                    .RegisterServices();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var programManager = serviceProvider.GetService<ProgramManager>();

        programManager.Run();
    }
}