using Core.service;

namespace Core.cli;

public class Cli
{
    private IGuessService GuessService { get; set; }
    private string ExitMessage { get; set; }

    public Cli(IGuessService guessService, string exitMessage)
    {
        GuessService = guessService;
        ExitMessage = exitMessage;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("started CLI");
            Console.WriteLine("1 - start game;");
            Console.WriteLine("2 - configure game;");
            Console.WriteLine("3 - exit.");

            Console.WriteLine("Enter your choice: ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Start();
                    continue;
                case "2":
                    Configure();
                    continue;
                case "3":
                    break;
                default:
                    Console.WriteLine("invalid input");
                    continue;
            }
            break;
        }
    }

    public void Start()
    {
        GuessService.RandomNumber();
        Console.WriteLine($"if you want to exit from game type {ExitMessage} in any moment");

        while (true)
        {
            int number;
            try
            {
                Console.Write("Enter number: ");
                number = GetNumberFromConsole();
            }
            catch (Exception ex)
            {
                if (ex.Message == ExitMessage)
                    return;

                Console.WriteLine(ex.Message);
                continue;
            }

            if (!GuessService.CheckNumber(number))
            {
                Console.WriteLine("wrong number");
                continue;
            }

            break;
        }
    }


    public void Configure()
    {
        Console.WriteLine("configure game");
        Console.WriteLine($"if you want to exit from configuration type {ExitMessage} in any moment");

        while (true)
        {
            int min, max;
            try
            {
                Console.Write("Enter maximum of interval: ");
                max = GetNumberFromConsole();

                Console.Write("Enter minimum of interval: ");
                min = GetNumberFromConsole();
            }
            catch (Exception ex)
            {
                if (ex.Message == ExitMessage)
                    return;

                Console.WriteLine(ex.Message);
                continue;
            }

            GuessService.Configure(new ConfigureInput
            {
                Min = min,
                Max = max
            });

            break;
        }
    }

    private int GetNumberFromConsole()
    {
        var numberInput = Console.ReadLine();
        if (numberInput == ExitMessage)
            throw new Exception(ExitMessage);

        if (!int.TryParse(numberInput, out int number))
            throw new Exception("invalid input");
        return number;
    }
}
