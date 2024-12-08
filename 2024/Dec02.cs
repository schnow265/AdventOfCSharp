using AOCRunner.Attributes;
using Serilog;

namespace _2024
{
    public class Dec02
    {
        [Solution(requiresLogger: true)]
        public void Part01(ILogger logger)
        {
            File.ReadAllLines(Path.Join(Environment.CurrentDirectory, "Resources", "Day02Input.txt"));


        }
    }
}
