namespace Domain
{
    public class Statistics
    {
        public int TotalRequests { get; set; }
        public int CompletedRequests { get; set; }
        public double AverageCompletionTime { get; set; }
        public string MostCommonProblem { get; set; }

        public Statistics() { }
    }
}