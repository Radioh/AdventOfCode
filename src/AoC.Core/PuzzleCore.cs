namespace AoC.Core;

public abstract class PuzzleCore
{
    protected static string[] GetLineInput(string day)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Input", day + ".txt");

        if (!File.Exists(path))
            throw new DirectoryNotFoundException("Could not find input file for day in 'Input' folder");

        return File.ReadAllLines(path);
    }
}
