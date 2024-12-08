namespace AOCRunner.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SolutionAttribute : Attribute
    {
        public bool RequiresLogger { get; }

        public SolutionAttribute(bool requiresLogger = false)
        {
            RequiresLogger = requiresLogger;
        }
    }
}
