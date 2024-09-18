using Core.cli;
using Core.service;
using Core.config;

public class App
{
    public static void Main(string[] args)
    {
        Config config = Config.ParseConfig("config.yml");

        GuessService guessService =
            new GuessService(config.Min, config.Max, config.Tries);

        Cli cli = new Cli(guessService, "quit");
        cli.Run();
    }
}
