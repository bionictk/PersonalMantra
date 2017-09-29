using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject cursor;
    SubVisManager subVis;
    public GameObject filterlist;
    public GameObject usa;
    
    // Use this for initialization
    void Start()
    {
        subVis = GameObject.Find("SubVis").GetComponent<SubVisManager>();
        keywords.Add("Show trends", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowBars");
        });

        keywords.Add("Hide trends", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("HideBars");
        });

        keywords.Add("Clear filter", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("Hide");
            subVis.clear();
            filterlist.GetComponent<FilterList>().clearList();
        });

        keywords.Add("Clear highlights", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("Hide");
            subVis.clear();
            filterlist.GetComponent<FilterList>().clearList();
        });

        string[] countryList = { "Asia", "Europe", "Middle East", "North America", "South America", "Sub-Saharan Africa" };
        string[] countryListShort = { "Asia", "Europe", "Middle East", "N. America", "S. America", "Sub-Saharan Africa" };

        for (int i = 0; i<countryList.Length; i++)
        {
            keywords.Add("Filter " + countryList[i], () =>
            {
                // Call the OnReset method on every descendant object.
                this.BroadcastMessage("ShowPoints", countryListShort[i]);
                subVis.show(countryListShort[i]);
                filterlist.GetComponent<FilterList>().addList(countryListShort[i]);
            });
        }

        keywords.Add("Highlight Asia", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Asia");
            subVis.show("Asia");
            filterlist.GetComponent<FilterList>().addList("Asia");
        });


        keywords.Add("Highlight Europe", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Europe");
            subVis.show("Europe");
            filterlist.GetComponent<FilterList>().addList("Europe");
        });

        keywords.Add("Highlight Middle East", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Middle East");
            subVis.show("Middle East");
            filterlist.GetComponent<FilterList>().addList("Middle East");
        });

        keywords.Add("Highlight North America", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "N. America");
            subVis.show("N. America");
            filterlist.GetComponent<FilterList>().addList("N. America");
        });

        keywords.Add("Highlight South America", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "S. America");
            subVis.show("S. America");
            filterlist.GetComponent<FilterList>().addList("S. America");
        });

        keywords.Add("Highlight Sub-Saharan Africa", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "Sub-Saharan Africa");
            subVis.show("Sub-Saharan Africa");
            filterlist.GetComponent<FilterList>().addList("Sub-Saharan Africa");
        });

        /// OTHER COMMANDS
        /// 
        keywords.Add("Highlight countries with GDP larger than 5000", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("filterExample", 5000);
            filterlist.GetComponent<FilterList>().addList("GDP > 5,000");
        });

        keywords.Add("Filter GDP below 10000", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("filterExample", 10000);
            filterlist.GetComponent<FilterList>().addList("GDP > 10,000");
        });

        keywords.Add("Show all", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowPoints", "force");
            filterlist.GetComponent<FilterList>().clearList();
        });

        keywords.Add("Toggle hovering", () =>
        {
            // Call the OnReset method on every descendant object.
            cursor.SendMessage("hovering");
        });

        keywords.Add("Find the United States", () =>
        {
            usa.SendMessage("OnHover");
        });

        keywords.Add("Find U.S.A.", () =>
        {
            usa.SendMessage("OnHover");
        });

        keywords.Add("Find the United States of America", () =>
        {
            usa.SendMessage("OnHover");
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