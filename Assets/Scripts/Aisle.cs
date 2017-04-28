using UnityEngine;
using System.Collections;

public class Aisle : MonoBehaviour {

    Vector3 tallScale;
    Vector3 tallPos;
    bool showing = false;
	// Use this for initialization
	void Start () {
        /*tallScale = this.transform.localScale;
        tallPos = this.transform.localPosition;
        this.transform.localScale = new Vector3(-0.09f, 0.01f, 0.544f);
        this.transform.localPosition = new Vector3(tallPos.x, 0.01f, 0.0f);*/
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (showing/* && GetComponent<Renderer>().enabled == true*/)
        {
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    void ShowBars()
    {
        showing = true;
    }

    void Hide()
    {
        showing = false;
    }
}
