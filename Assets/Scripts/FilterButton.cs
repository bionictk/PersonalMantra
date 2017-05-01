using UnityEngine;
using System.Collections;

public class FilterButton : MonoBehaviour {

    GameObject parent;
    public string country;
    SubVisManager subVis;

	// Use this for initialization
	void Start () {
        parent = this.transform.parent.gameObject;
        subVis = GameObject.Find("SubVis").GetComponent<SubVisManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnHit()
    {        
        if (country == "clear")
        {
            parent.BroadcastMessage("Hide");
            subVis.clear();
        } else
        {
            parent.BroadcastMessage("TogglePoints", country);
            subVis.toggle(country);
        }
    }
}
