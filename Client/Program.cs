using Console.Helper;

internal class Program
{
    private static ConsoleController ConsoleController = new ConsoleController();

    private static void Main(string[] args)
    {
        ConsoleController.AddBehavior(new WagonService.Client.Services.WagonService("https://localhost:7169"));

        ConsoleController.Read();
    }
}