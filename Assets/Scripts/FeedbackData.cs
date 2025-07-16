using System.Collections.Generic;

[System.Serializable]
public class FeedbackEntry
{
    public string question;
    public float rating;
}

[System.Serializable]
public class FeedbackData
{
    public List<FeedbackEntry> responses = new List<FeedbackEntry>();
    public string comments;
}
