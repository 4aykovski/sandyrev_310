namespace Core.service;

public interface IGuessService
{
    public void RandomNumber();
    public bool CheckNumber(int number);
    public void Configure(ConfigureInput input);
}

public class ConfigureInput
{
    public int Min { get; set; }
    public int Max { get; set; }
}
