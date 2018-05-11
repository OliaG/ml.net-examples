using Microsoft.ML.Runtime.Api;

namespace GitHubLabeler
{
    class CoreFxIssue
    {
        public string ID;
        public string Area; // This is an issue label, for example "area-System.Threading"
        public string Title;
        public string Description;

        [ColumnName("Label")]
        public string Label;
    }

    public class CoreFxIssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public string Area;
    }
}
