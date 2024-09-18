namespace Core.service;

public class GuessService : IGuessService
{

    private int _minInterval, _maxInterval;
    private int? _randomNumber;
    private int _maxTries;
    private int _currentTries;

    public GuessService(int minInterval, int maxInterval, int maxTries)
    {
        _minInterval = minInterval;
        _maxInterval = maxInterval;
        _maxTries = maxTries;
    }

    public void SetUpGame()
    {
        _randomNumber =
            _minInterval + new Random().Next(_maxInterval - _minInterval);

        _currentTries = _maxTries;
    }

    public bool CheckNumber(int number)
    {
        if (_randomNumber == null)
            throw new Exception("random number is not configured");

        _currentTries--;

        if (number == _randomNumber)
            return true;

        if (_currentTries == 0)
            throw new Exception("no tries left");

        return false;
    }

    public void Configure(ConfigureInput input)
    {
        _minInterval = input.Min;
        _maxInterval = input.Max;
        _maxTries = input.Tries;
    }
}
