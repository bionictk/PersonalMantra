﻿using UnityEngine;
using System.Collections.Generic;

public class SubVisManager : MonoBehaviour {

    public GameObject barPrimitive;
    bool showing = false;
    string currentCountry = "";
    const float padding = 0.015f;
    public GameObject bc = null;
    public TextMesh vl;

	// Use this for initialization
	void Start () {     
	}
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        
        if (showing && bc != null)
        {
            bc.transform.LookAt(2 * bc.transform.position - headPosition);
        }
    }

    public void toggle(string country)
    {
        if (showing)
        {
            clear();
            if (country != currentCountry)
                show(country);
        }
        else
        {
            show(country);
        }        
    }

    public void show(string country)
    {
        bc.SetActive(true);
        vl.text = country;
        if (currentCountry != "") bc.transform.Find(currentCountry).gameObject.SetActive(false);
        if (country != "") bc.transform.Find(country).gameObject.SetActive(true);
        currentCountry = country;
        showing = true;
    }

    public void clear()
    {
        //this.transform.Find(currentCountry).gameObject.SetActive(false);        
        if (currentCountry != "") bc.transform.Find(currentCountry).gameObject.SetActive(false);
        bc.SetActive(false);
        showing = false;
    }

    public void createBars(IDictionary<string, Countries> grid)
    {

        /*Debug.Log(grid.Count);
        Debug.Log(grid["Asia"].count);
        Debug.Log(grid["Asia"].maxGDP);*/
        bc = this.transform.Find("BarChart").gameObject;
        vl = bc.transform.Find("VisLabel").GetComponent<TextMesh>();
        foreach (KeyValuePair<string, Countries> i in grid) {
            GameObject newGroup = new GameObject(i.Key);
            newGroup.transform.parent = bc.transform;
            newGroup.transform.localPosition = new Vector3(-0.1783f, 0, 0);
            newGroup.transform.localScale = new Vector3(0.7207208f, 1, 1);
            newGroup.transform.localRotation = new Quaternion(0, 0, 0, 1);

            for (int j = 0; j < i.Value.count; j++)
            {
                GameObject newItem = GameObject.Instantiate(barPrimitive) as GameObject;
                newItem.transform.parent = newGroup.transform;
                newItem.transform.localPosition = new Vector3(-0.312f, -0.311f, 0);
                newItem.transform.localScale = new Vector3(1, 1, 1);
                newItem.transform.localRotation = new Quaternion(0, 0, 0, 1);

                newItem.transform.Find("Label").gameObject.GetComponent<TextMesh>().text = i.Value.name[j];
                Vector3 oldScale = newItem.transform.Find("Bar").localScale;
                oldScale.y = 0.62f * (i.Value.gdp[j] / i.Value.maxGDP);
                oldScale.x = 0.8728f / i.Value.count - padding;
                newItem.transform.Find("Bar").localScale = oldScale;
                Vector3 oldPos = newItem.transform.Find("Bar").localPosition;
                oldPos.y = oldScale.y / 2.0f;
                newItem.transform.Find("Bar").localPosition = oldPos;

                Vector3 itemPos = newItem.transform.localPosition;
                itemPos.x += padding + oldScale.x / 2 + j * (padding + oldScale.x);
                newItem.transform.localPosition = itemPos;
            }
            newGroup.SetActive(false);
        }

        this.gameObject.SetActive(true);
        clear();
        //show("Europe");
        //clear();
        //show("Asia");
        //newBar.GetComponent<Renderer>().material.color = newBar.transform.parent.Find("Button").gameObject.GetComponent<Renderer>().material.color;        
    }
}
