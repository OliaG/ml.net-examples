using System;
using System.Threading.Tasks;

namespace GitHubLabeler
{
    class Program
    {
        // ToDo: Insert your GitHub token with write permission to
        // repository you want to label issues for.
        private const string GitHubToken = "";
        // ToDo: Insert User name and Repository name
        private const string GitHubUserName = "";
        private const string GitHubRepoName = "";

        static async Task Main(string[] args)
        {
            Predictor.Train();

            // Consider only issues with ID older tham minId. To look at all issues in the repo, set minId = 1.
            const int minId = 1;
            var labeler = new Labeler(GitHubUserName, GitHubRepoName, GitHubToken);
            
            await labeler.LabelAllNewIssues(minId);
            
            Console.WriteLine("Labeling completed");
            Console.ReadLine();
        }
    }
}
