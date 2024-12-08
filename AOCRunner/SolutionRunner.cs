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
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Discovering solution classes...");

            // Get all classes in the calling assembly with the [Solution] attribute
            var callingAssembly = Assembly.GetCallingAssembly();
            var solutionClasses = callingAssembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<SolutionAttribute>() != null)
                .ToList();

            if (!solutionClasses.Any())
            {
                Log.Warning("No solution classes found in assembly {AssemblyName}.", callingAssembly.FullName);
                return;
            }

            Log.Information("Available Solution Classes:");
            for (int i = 0; i < solutionClasses.Count; i++)
            {
                Log.Information("{Index}. {ClassName}", i + 1, solutionClasses[i].Name);
            }

            Log.Information("Enter the number of the solution class to run, or '*' to run all:");
            string input = Console.ReadLine();

            if (input?.ToLower() == "*")
            {
                foreach (var solutionClass in solutionClasses)
                {
                    ExecuteSolutionClass(solutionClass);
                }
            }
            else if (int.TryParse(input, out int classIndex) && classIndex > 0 && classIndex <= solutionClasses.Count)
            {
                var selectedClass = solutionClasses[classIndex - 1];
                SelectAndExecuteMethodInClass(selectedClass);
            }
            else
            {
                Log.Error("Invalid input. Exiting.");
            }
        }

        private static void SelectAndExecuteMethodInClass(Type solutionClass)
        {
            Log.Information("Running solutions in {ClassName}...", solutionClass.Name);

            var constructor = solutionClass.GetConstructor(new[] { typeof(ILogger) });
            if (constructor == null)
            {
                Log.Warning("{ClassName} does not have a constructor accepting ILogger.", solutionClass.Name);
                return;
            }

            var instance = constructor.Invoke(new object[] { Log.Logger });

            // Get all public methods in the class (excluding inherited ones like ToString, GetType, etc.)
            var solutionMethods = solutionClass
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.DeclaringType == solutionClass)  // Only methods declared in the class, not inherited ones
                .Where(m => m.GetParameters().Length == 0)  // Optionally filter by methods that take no parameters
                .ToList();

            if (!solutionMethods.Any())
            {
                Log.Warning("No public methods found in {ClassName}.", solutionClass.Name);
                return;
            }

            Log.Information("Available methods in {ClassName}:", solutionClass.Name);
            for (int i = 0; i < solutionMethods.Count; i++)
            {
                Log.Information("{Index}. {MethodName}", i + 1, solutionMethods[i].Name);
            }

            Log.Information("Enter the number of the method to run, or '*' for all:");
            string methodInput = Console.ReadLine();

            if (int.TryParse(methodInput, out int methodIndex) && methodIndex > 0 && methodIndex <= solutionMethods.Count)
            {
                var selectedMethod = solutionMethods[methodIndex - 1];
                ExecuteSolutionMethod(instance, selectedMethod);
            }
            else if (methodInput == "*")
            {
                foreach (var method in solutionMethods)
                {
                    ExecuteSolutionMethod(instance, method);
                }
            }
            else
            {
                Log.Error("Invalid method selection. Exiting.");
            }
        }

        private static void ExecuteSolutionClass(Type solutionClass)
        {
            Log.Information("Running methods in class {ClassName}...", solutionClass.Name);

            var constructor = solutionClass.GetConstructor(new[] { typeof(ILogger) });
            if (constructor == null)
            {
                Log.Warning("{ClassName} does not have a constructor accepting ILogger.", solutionClass.Name);
                return;
            }

            var instance = constructor.Invoke(new object[] { Log.Logger });

            // Get all public methods in the class (excluding inherited ones)
            var solutionMethods = solutionClass
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.DeclaringType == solutionClass)  // Only methods declared in the class
                .Where(m => m.GetParameters().Length == 0)  // Optionally filter by methods that take no parameters
                .ToList();

            if (!solutionMethods.Any())
            {
                Log.Warning("No public methods found in {ClassName}.", solutionClass.Name);
                return;
            }

            foreach (var method in solutionMethods)
            {
                ExecuteSolutionMethod(instance, method);
            }
        }

        private static void ExecuteSolutionMethod(object instance, MethodInfo method)
        {
            Log.Information("Running method {MethodName}...", method.Name);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                method.Invoke(instance, null);  // No parameters
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error running method.");
                return;
            }

            stopwatch.Stop();
            Log.Information("{MethodName} completed in {ElapsedMilliseconds} ms.", method.Name, stopwatch.ElapsedMilliseconds);
        }
    }
}
