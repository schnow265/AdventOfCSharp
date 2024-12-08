using AOCRunner.Attributes;
using Serilog;
using System.Diagnostics;
using System.Reflection;

namespace AOCRunner
{
    public class SolutionRunner
    {
        public static void Run(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Debug("Discovering solution methods...");

            // Get all methods in the current assembly with the [Solution] attribute
            var callingAssembly = Assembly.GetCallingAssembly();
            var solutionMethods = callingAssembly
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                .Where(m => m.GetCustomAttribute<SolutionAttribute>() != null)
                .ToList();

            if (!solutionMethods.Any())
            {
                Log.Warning("No solution methods found.");
                return;
            }

            Log.Information("Available Solutions:");
            for (int i = 0; i < solutionMethods.Count; i++)
            {
                Log.Information("{Index}. {Method}", i + 1, $"{solutionMethods[i].DeclaringType.Name}.{solutionMethods[i].Name}");
            }

            Log.Information("Enter the number of the solution to run, or 'all' to run all:");
            string input = Console.ReadLine();

            if (input?.ToLower() == "all")
            {
                foreach (var method in solutionMethods)
                {
                    ExecuteSolution(method);
                }
            }
            else if (int.TryParse(input, out int index) && index > 0 && index <= solutionMethods.Count)
            {
                ExecuteSolution(solutionMethods[index - 1]);
            }
            else
            {
                Log.Error("Invalid input. Exiting.");
            }
        }

        private static void ExecuteSolution(MethodInfo method)
        {
            Log.Information("Running {Method}...", $"{method.DeclaringType.Name}.{method.Name}");

            var instance = method.IsStatic ? null : Activator.CreateInstance(method.DeclaringType);
            var parameters = method.GetParameters();

            // Check if the method accepts ILogger as a parameter
            var args = parameters.Select(p =>
            {
                if (p.ParameterType == typeof(ILogger))
                    return (object)Log.Logger;
                return null; // Add handling for other types if needed
            }).ToArray();

            var stopwatch = Stopwatch.StartNew();

            try
            {
                method.Invoke(instance, args);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error running solution.");
                return;
            }

            stopwatch.Stop();
            Log.Information("{Method} completed in {ElapsedMilliseconds} ms.", method.Name, stopwatch.ElapsedMilliseconds);
        }
    }
}
