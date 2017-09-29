using UnityEngine;
using System.Collections;

public class Datapoint : MonoBehaviour {

    public string country;
    public string countryname; // this should be country and country should be continent
    bool showing = false;
    GameObject label = null;
    float timer;
    bool stopTimer = true;
    Color oldColor;
    Color hoverColor;
    bool forceShowing = false;
    public float GDP = 0;

	// Use this for initialization
	void Start () {
        /*tallPosition = this.transform.localPosition.y;
        tallScale = this.transform.localScale;
        x = this.transform.localPosition.x;
        z = this.transform.localPosition.z;
        this.transform.localPosition = new Vector3(x, 0, z);
        this.transform.localScale = new Vector3(tallScale.x, 0.01f, tallScale.z);*/
        oldColor = this.GetComponent<Renderer>().material.color;
        hoverColor = new Color(1.0f, 0.0f, 0.0f);
        label = this.transform.FindChild("Label").gameObject;
        if (label != null)
            label.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if (showing || (forceShowing)/*&& GetComponent<Renderer>().enabled == true*/)
        {
            GetComponent<Renderer>().enabled = true;
        } else
        {
            GetComponent<Renderer>().enabled = false;
        }

        if (stopTimer == false)
        {
            timer -= 0.01f;
            if (timer < 0)
            {
                stopTimer = true;
                //this.transform.parent.parent.BroadcastMessage("HideLabel");
                HideLabel();
                //Debug.Log("TurnOff!");
            }
        }        
	}

    void ShowPoints(string filter)
    {
        if (filter == country || filter == "force")
            showing = true;
    }

    void filterExample(int filter)
    {
        if (filter < GDP)
            showing = true;
    }

    void Hide()
    {
        showing = false;
    }

    void TogglePoints(string filter)
    {
        if (filter == country)
            showing = !showing;
    }

    void OnHover()
    {
           
        this.transform.parent.parent.BroadcastMessage("HideLabel");
        
        ShowLabel();
        //Debug.Log("Turnon!");
    }

    void ShowLabel()
    {
        timer = 0.7f;
        stopTimer = false;
        if (label != null) label.SetActive(true);
        this.GetComponent<Renderer>().material.color = hoverColor;
        forceShowing = true;
    }

    void HideLabel()
    {
        stopTimer = true;
        if (label != null) label.SetActive(false);        
        forceShowing = false;
        this.GetComponent<Renderer>().material.color = oldColor;
    }

}
