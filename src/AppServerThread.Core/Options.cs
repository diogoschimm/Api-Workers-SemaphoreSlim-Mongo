namespace AppServerThread.Core
{
    public class Options
    { 
        public int Page { get; set; } = 1;
        public int Total { get; set; } = 10;

        public int GetInitialPage() => (Page * Total) - Total;
    }
}
