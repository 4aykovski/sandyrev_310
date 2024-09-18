namespace Core.service;

public interface IGuessService
{
    public void SetUpGame();
    public bool CheckNumber(int number);
    public void Configure(ConfigureInput input);
}

public class ConfigureInput
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int Tries { get; set; }
}
