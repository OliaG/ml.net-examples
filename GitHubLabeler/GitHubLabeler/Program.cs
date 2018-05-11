using System;

namespace GitHubLabeler
{
    class Program
    {
        // ToDo: Insert your GitHub token with write permission to
        // repository you want to label issues for.
        const string GitHubToken = "";
        // ToDo: Insert User name and Repository name
        const string GitHubUserName = "";
        const string GitHubRepoName = "";

        static void Main(string[] args)
        {
            Predictor.Train();

            // Consider only issues with ID older tham minId. To look at all issues in the repo, set minId = 1.
            const int minId = 1;
            var labeler = new Labeler(GitHubUserName, GitHubRepoName, GitHubToken);
            
            labeler.LabelAllNewIssues(minId);
            
            Console.WriteLine("Labeling completed");
            Console.ReadLine();
        }
    }
}
