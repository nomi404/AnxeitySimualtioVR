using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class FeedbackForm : MonoBehaviour
{
    [Header("Sliders")]
    public Slider rating;
    public Slider vrInteractionEaseSlider;
    public Slider immersionSlider;
    public Slider physicalComfortSlider;
    public Slider realism;
    public Slider heartbeatSlider;

    [Header("Optional Comment")]
    public TMP_InputField commentField;

    public void SubmitFeedback()
    {
        FeedbackData data = new FeedbackData();
        data.responses = new List<FeedbackEntry>
        {
            new FeedbackEntry { question = "Rating", rating = rating.value },
            new FeedbackEntry { question = "I found the VR interaction easy to use and intuitive.", rating = vrInteractionEaseSlider.value },
            new FeedbackEntry { question = "I was immersed and present in the simulation.", rating = immersionSlider.value },
            new FeedbackEntry { question = "The experience felt physically comfortable (no motion sickness, dizziness, etc.).", rating = physicalComfortSlider.value },
            new FeedbackEntry { question = "Realism", rating = realism.value },
            new FeedbackEntry { question = "The heart beat effect scared me.", rating = heartbeatSlider.value },

        };

        //data.comments = commentField.text;

        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + "/feedback_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";
        File.WriteAllText(path, json);

        Debug.Log("Feedback saved to: " + path);
        SceneManager.LoadScene("menu");
    }
}
