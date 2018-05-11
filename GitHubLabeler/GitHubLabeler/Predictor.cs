using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Threading.Tasks;

namespace GitHubLabeler
{
    class Predictor
    {
        private const string DataPath = @"..\..\..\Data\corefx_issues.tsv";
        private const string ModelPath = @"..\..\..\Models\Model.zip";

        private static PredictionModel<CoreFxIssue, CoreFxIssuePrediction> _model;

        public static void Train()
        {
            var pipeline = new LearningPipeline();

            pipeline.Add(new TextLoader<CoreFxIssue>(DataPath, useHeader: true));

            pipeline.Add(new Dictionarizer(("Area", "Label")));

            pipeline.Add(new TextFeaturizer("Title", "Title"));

            pipeline.Add(new TextFeaturizer("Description", "Description"));
            
            pipeline.Add(new ColumnConcatenator("Features", "Title", "Description"));

            pipeline.Add(new StochasticDualCoordinateAscentClassifier());
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

            Console.WriteLine("=============== Training model ===============");
            var model = pipeline.Train<CoreFxIssue, CoreFxIssuePrediction>();

            model.WriteAsync(ModelPath);

            Console.WriteLine("=============== End training ===============");
            Console.WriteLine("The model is saved to {0}", ModelPath);
        }

        public static async Task<string> Predict(CoreFxIssue issue)
        {
            if (_model == null)
            {
                _model = await PredictionModel.ReadAsync<CoreFxIssue, CoreFxIssuePrediction>(ModelPath);
            }

            var prediction = _model.Predict(issue);

            return prediction.Area;
        }
    }
}
