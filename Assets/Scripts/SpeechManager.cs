using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject cursor;

    // Use this for initialization
    void Start()
    {
        keywords.Add("Show trend", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowBars");
        });

        keywords.Add("Clear", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("Hide");
        });

        keywords.Add("Filter asia", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "asia");
        });

        keywords.Add("Filter europe", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "europe");
        });


        keywords.Add("Toggle hovering", () =>
        {
            // Call the OnReset method on every descendant object.
            cursor.SendMessage("hovering");
        });


        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}