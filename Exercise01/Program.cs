var lines = File.ReadAllLines("./Data/data.csv");

ProcessData(lines, ProcessSum, Console.WriteLine);
ProcessData(lines, ProcessAvg, Console.WriteLine);


// Skip the header and process each line
void ProcessData(string[] lines, Func<string, string> Process, Action<string> PrintMethod)
{
    for (int i = 1; i < lines.Length; i++)
        PrintMethod(Process(lines[i]));
}

string ProcessSum(string csvLine)
{
    var data = csvLine.Split(',');
    int sum = 0;
    foreach (var number in data)
    {
        sum += int.Parse(number);
    }
    return $"Sum: {sum}";
}

string ProcessAvg(string csvLine)
{
    var data = csvLine.Split(',');
    int sum = 0;
    foreach (var number in data)
    {
        sum += int.Parse(number);
    }
    return $"Average: {((float)sum) / data.Length}";
}