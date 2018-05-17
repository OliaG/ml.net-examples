using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubLabeler
{
    public class Labeler
    {
        private const string GitHubProductIdentifier = "Autolabeler";
        
        private readonly GitHubClient _client;
        private readonly string _owner;
        private readonly string _name;

        public Labeler(string owner, string name, string accessToken)
        {
            _owner = owner;
            _name = name;
            _client = new GitHubClient(new ProductHeaderValue(GitHubProductIdentifier))
            {
                Credentials = new Credentials(accessToken)
            };
        }

        //Label all issues that are not labeled yet starting from Issue.Number = minId and later
        public async Task LabelAllNewIssues(int minId)
        {
            var newIssues = await GetNewIssues(minId);
            foreach (var issue in newIssues.Where(issue => issue.Labels.Count == 0))
            {
                var label = await GetLabel(issue);
                UpdateLabels(issue, label);
                NotifyAssignee(issue, label);
            }
        }

        public async Task<IReadOnlyList<Issue>> GetNewIssues(int minId)
        {
            var issueRequest = new RepositoryIssueRequest
            {
                State = ItemStateFilter.Open,
                Filter = IssueFilter.All,
                Since = DateTime.Now.AddMinutes(-10)
            };

            var issues = await _client.Issue.GetAllForRepository(_owner, _name, issueRequest);
            
            //Filter out pull requests and issues that are older than minId
            issues = new List<Issue>(issues.Where(i => i.Number >= minId && !i.HtmlUrl.Contains("/pull/")));
            
            return issues;
        }

        private async Task<string> GetLabel(Issue issue)
        {
            var corefxIssue = new CoreFxIssue
            {
                ID = issue.Number.ToString(),
                Title = issue.Title,
                Description = issue.Body
            };

            var predictedLabel = await Predictor.Predict(corefxIssue);

            return predictedLabel;
        }

        private void UpdateLabels(Issue issue, string label)
        {
            var issueUpdate = new IssueUpdate();
            issueUpdate.AddLabel(label);
            
            _client.Issue.Update(_owner, _name, issue.Number, issueUpdate);
        }

        private void NotifyAssignee(Issue issue, string label)
        {
            // To send out email to assigned person add data (emails, credentials) in Notifier.cs and uncomment the next line
            //Notifier.Send(issue, label);
            Console.WriteLine($"Issue {issue.Number} : \"{issue.Title}\" \t was labeled as: {label}");
        }
    }
}
