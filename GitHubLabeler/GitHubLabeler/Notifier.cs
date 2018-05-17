using System.Net;
using System.Net.Mail;
using Octokit;

namespace GitHubLabeler
{
    class Notifier
    {
        public static void Send(Issue issue, string label)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress("from_email@mail.com"); // ToDo: add email address
                mail.To.Add(GetEmailByLabel(label));
                mail.Subject = $"Issue {issue.Number} was labeled as {label}";
                mail.Body = $"The issue {issue.Number}: \"{issue.Title}\" was labeled as {label}. You are receiving this email because you are assigned as a contact person for {label}";
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("", ""); // ToDo: add gmail username and password
                    smtp.Send(mail);
                }
            }
        }

        public static string GetEmailByLabel(string label)
        {
            // ToDo: Add logic to get contact email for the label
            return "to_email@mail.com";
        }
    }
}
