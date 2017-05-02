using UnityEngine;
using System.Collections;

public class Countries : MonoBehaviour {
    public float maxGDP = 0.0f;
    public new string[] name = new string[15];
    public float[] gdp = new float[15];
    public int count = 0;
    public Color color = new Color(0, 0, 0);

    public Countries(string nm = "", float g = 0.0f)
    {
        addCountry(nm, g);
    }

    public void addCountry(string nm = "", float g = 0.0f)
    {
        name[count] = nm;
        gdp[count] = g;
        maxGDP = maxGDP < g ? g : maxGDP;
        count++;
    }
}