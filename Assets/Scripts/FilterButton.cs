using UnityEngine;
using System.Collections;

public class FilterButton : MonoBehaviour {

    GameObject parent;
    public string country;
    SubVisManager subVis;
    GameObject filterList;

	// Use this for initialization
	void Start () {
        parent = this.transform.parent.gameObject;
        subVis = GameObject.Find("SubVis").GetComponent<SubVisManager>();
        filterList = GameObject.Find("FilterList");
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
            filterList.GetComponent<FilterList>().clearList();
        } else
        {
            parent.BroadcastMessage("TogglePoints", country);
            subVis.toggle(country);
            if (filterList.GetComponent<FilterList>().addList(country) == false)
                filterList.GetComponent<FilterList>().removeList(country);
        }
    }
}
