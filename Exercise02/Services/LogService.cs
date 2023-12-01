namespace Exercise02.Services;

public class LogService
{
    public void LogException(string exception)
    {
        using var writer = new StreamWriter("Exceptions.txt", append: true);
        writer.WriteLine(exception);
    }
}
