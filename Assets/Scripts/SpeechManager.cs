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
        keywords.Add("Show trends", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowBars");
        });

        keywords.Add("Clear", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("Hide");
        });

        keywords.Add("Filter Asia", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Asia");
        });

        keywords.Add("Filter Europe", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Europe");
        });

        keywords.Add("Filter Middle East", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Middle East");
        });

        keywords.Add("Filter North America", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "N. America");
        });

        keywords.Add("Filter South America", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "S. America");
        });

        keywords.Add("Filter Sub-Saharan Africa", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Sub-Saharan Africa");
        });

        keywords.Add("Filter countries with GDP larger than 5000", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("filterExample", 5000);
        });

        keywords.Add("Filter GDP", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("filterExample", 16000);
        });

        keywords.Add("Show all", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "force");
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