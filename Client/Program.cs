using Console.Helper;

internal class Program
{
    private static ConsoleController ConsoleController = new ConsoleController();

    private static void Main(string[] args)
    {
        System.Console.WriteLine("Введите строку подключения");

        var connection = System.Console.ReadLine();

        ConsoleController.AddBehavior(new WagonService.Client.Services.WagonService(connection!));

        ConsoleController.Read();
    }
}