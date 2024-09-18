using Core.cli;
using Core.service;

public class App
{
    public static void Main(string[] args)
    {
        Cli cli = new Cli(new GuessService(), "quit");
        cli.Run();
    }
}
