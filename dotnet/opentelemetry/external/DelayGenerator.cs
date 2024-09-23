namespace FP.Monitoring.External;

public class DelayGenerator
{
    private int _min = 0;
    private int _max = 250;
    private readonly Random _random = new();
    public int Min
    {
        get => _min;
        set
        {
            if (value <= 10)
            {
                _min = 0;
            }
            else if (value > _max)
            {
                _min = _max - 10;
            }
            else
            {
                _min = value;    
            }
        }
    }

    public int Max
    {
        get => _max;
        set
        {
            if (value <= _min)
            {
                _max = _min + 10;
            }
            else
            {
                _max = value;
            }
        }
    }

    public async Task WaitAsync()
    {
        var nextValue = _random.Next(Min, Max);
        Console.WriteLine(nextValue);
        await Task.Delay(TimeSpan.FromMilliseconds(nextValue));
    }
}