using UnityEngine;
using System.Collections;

public class FilterList : MonoBehaviour {
    string[] list = new string[20];
    int listlen = 0;
    public AudioClip addAudio, removeAudio;
    
	// Use this for initialization
	void Start () {        
        updateList();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void updateList()
    {
        string fullstring = "Applied Filters:\n";
        if (listlen == 0) fullstring += "None\n";
        for (int i = 0; i < listlen; i++)
            fullstring += list[i] + "\n";
        this.GetComponent<TextMesh>().text = fullstring;
    }

    public bool addList(string filter)
    {
        bool exists = false;
        for (int i=0; i< listlen; i++)
        {
            if (list[i] == filter)
            {
                exists = true;
                break;
            }
        }
        if (!exists)
        {
            list[listlen++] = filter;
            updateList();
            //SystemSounds.Beep.Play();            
            AudioSource.PlayClipAtPoint(addAudio, Camera.main.transform.position);
        }
        return !exists;
    }

    public void removeList(string filter)
    {
        bool exists = false;
        for (int i = 0; i < listlen; i++)
        {
            if (list[i] == filter)
            {
                exists = true;
                for (int j = i; j < listlen-1; j++)
                {
                    list[j] = list[j + 1];
                }
                listlen--;
            }
        }
        if (exists)
        {
            updateList();
            //SystemSounds.Exclamation.Play();
            AudioSource.PlayClipAtPoint(removeAudio, Camera.main.transform.position);
        }
    }

    public void clearList()
    {
        listlen = 0;
        updateList();
        //SystemSounds.Exclamation.Play();
        AudioSource.PlayClipAtPoint(removeAudio, Camera.main.transform.position);
    }
}
