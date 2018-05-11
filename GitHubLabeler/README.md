## Goal
This is just a simple prototype application to demonstrate how to use [ML.NET](https://www.nuget.org/packages/Microsoft.ML/) APIs. The main focus is on creating, training, and using ML (Machine Learning) model. The rest of the code was created just for the sake of end-to-end scenario and can be optimized.

## Overview
GitHubLabeler is a sample console application that illustrates how you can auto-label GitHub issues with ML model by using multi-class classification algorithm and text capabilities of [ML.NET](https://www.nuget.org/packages/Microsoft.ML/).

## Training 
To train a model, issues from public [corefx](https://github.com/dotnet/corefx) repository were exported in .tsv file: `corefx_issues.tsv`. After training the model, it is saved as a .zip file in `GitHubLabeler\Models\Model.zip`.
>Training should be performed only once for the same training data (`corefx_issues.tsv`).

## Inferencing
After training, the model is used for predicting new issues label. For testing convenience only open not labeled issues that were created in the past 10 minutes are subject to labeling:
```csharp
Since = DateTime.Now.AddMinutes(-10)
```
After predicting the label, the program updates the issue with the predicted label on GitHub and sends an email to an assigned person.

## To make the program work
Fill in following fields:
```csharp
// ToDo: Insert your GitHub token with write permission to
// repository you want to label issues for.
const string GitHubToken = "";
// ToDo: Insert User name and Repository name
const string GitHubUserName = "";
const string GitHubRepoName = "";
```
```csharp
mail.From = new MailAddress("from_email@mail.com"); // ToDo: add email address
```
```csharp
smtp.Credentials = new NetworkCredential("", ""); // ToDo: add gmail username and password
```
```csharp
// ToDo: Add logic to get contact email for the label
return "to_email@mail.com";
```

## Ask questions!
If you have any questions or struggling with any part of ML.NET APIs usage in this sample, post your question as an issue in this repository. I'll be happy to help!