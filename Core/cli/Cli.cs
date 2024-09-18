using Core.service;

namespace Core.cli;

public class Cli
{
    private IGuessService _guessService { get; set; }
    private string _exitMessage { get; set; }

    public Cli(IGuessService guessService, string exitMessage)
    {
        _guessService = guessService;
        _exitMessage = exitMessage;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("started CLI");
            Console.WriteLine("1 - start game;");
            Console.WriteLine("2 - configure game;");
            Console.WriteLine("3 - exit.");

            Console.Write("Enter your choice: ");
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

    private void Start()
    {
        _guessService.SetUpGame();
        Console.WriteLine(
            $"if you want to exit from game type {_exitMessage} in any moment");

        while (true)
        {
            try
            {
                Console.Write("Enter number: ");
                int number = GetNumberFromConsole();

                if (!_guessService.CheckNumber(number))
                {
                    Console.WriteLine("wrong number");
                    continue;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == _exitMessage)
                    return;

                if (ex.Message == "no tries left")
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine(ex.Message);
                continue;
            }

            Console.WriteLine("you win! congratulations!");

            break;
        }
    }

    private void Configure()
    {
        Console.WriteLine("configure game");
        Console.WriteLine(
            $"if you want to exit from configuration type {_exitMessage} in any moment");

        while (true)
        {
            int min, max, tries;
            try
            {
                Console.Write("Enter maximum of interval: ");
                max = GetNumberFromConsole();

                Console.Write("Enter minimum of interval: ");
                min = GetNumberFromConsole();

                Console.Write("Enter maximum of tries: ");
                tries = GetNumberFromConsole();

                if (tries < 1)
                    throw new Exception("invalid input");

                if (min > max)
                    throw new Exception("invalid input");
            }
            catch (Exception ex)
            {
                if (ex.Message == _exitMessage)
                    return;

                Console.WriteLine(ex.Message);
                continue;
            }

            _guessService.Configure(
                new ConfigureInput { Min = min, Max = max, Tries = tries });

            break;
        }
    }

    private int GetNumberFromConsole()
    {
        var numberInput = Console.ReadLine();
        if (numberInput == _exitMessage)
            throw new Exception(_exitMessage);

        if (!int.TryParse(numberInput, out int number))
            throw new Exception("invalid input");
        return number;
    }
}
