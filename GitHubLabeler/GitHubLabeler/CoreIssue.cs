using Microsoft.ML.Runtime.Api;

namespace GitHubLabeler
{
    class CoreFxIssue
    {
        [Column(ordinal: "0")]
        public string ID;
        [Column(ordinal: "1")]
        public string Area; // This is an issue label, for example "area-System.Threading"
        [Column(ordinal: "2")]
        public string Title;
        [Column(ordinal: "3")]
        public string Description;

        [Column(ordinal: "4")]
        [ColumnName("Label")]
        public string Label;
    }

    public class CoreFxIssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public string Area;
    }
}
