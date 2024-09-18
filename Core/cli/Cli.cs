using Core.service;

namespace Core.cli;

public class Cli
{
    private IGuessService GuessService { get; set; }

    public Cli(IGuessService guessService)
    {
        GuessService = guessService;
    }

    public void Run()
    {
        Console.WriteLine("started CLI");
        Console.WriteLine()
    }
}