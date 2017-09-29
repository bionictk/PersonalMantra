
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DatapointCreater : MonoBehaviour
{
    public TextAsset csvFile;
    public GameObject pointPrimitive;
    public GameObject Asia, EU, ME, NA, SA, SSA;
    public GameObject subVis;
    private string[,] grid;
    private GameObject newGroup;

    public void Start()
    {
        grid = SplitCsvGrid(csvFile.text);
        //Debug.Log("size = " + (1 + grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1)));

        createDatapoints(grid);        
        //movingShape = GameObject.Instantiate(shapePrimitive) as GameObject;
        //movingShape.SetActive(true);
        //lemon.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        
        //DebugOutputGrid(grid); 
    }

    void createDatapoints(string[,] grid)
    {
        IDictionary<string, Countries> data = new Dictionary<string, Countries>();
        for (int row = 1; row < grid.GetUpperBound(1); row++)
        {
            GameObject newPoint = GameObject.Instantiate(pointPrimitive) as GameObject;
            newPoint.SetActive(true);            
            /*
            if (row == 1)
            {
                Debug.Log(grid[1, row]);
                Debug.Log(int.Parse(grid[1, row]));
                Debug.Log((100.0f + int.Parse(grid[1, row])));
                Debug.Log((100.0f + int.Parse(grid[1, row])) * 0.004945f);
            }*/
            newPoint.transform.Find("Label").gameObject.GetComponent<TextMesh>().text = "Country: " + grid[0, row] + "\nTrust: " + grid[1, row] + "\nEase of doing business: " + grid[2, row] + "\nGDP: " + grid[3, row] + "\nRegion: " + grid[4, row];
            newPoint.GetComponent<Datapoint>().country = grid[4, row];
            //newPoint.GetComponent<Datapoint>().countryname = grid[0, row];
            if (grid[0, row] == "United States")
            {
                GameObject.Find("MainVis").GetComponent<SpeechManager>().usa = newPoint;
            }
            newPoint.GetComponent<Datapoint>().GDP = float.Parse(grid[3, row]);

            if (grid[4, row] == "Asia") 
            {
                newPoint.transform.parent = Asia.transform;                
            }
            else if (grid[4, row] == "Europe")
            {
                newPoint.transform.parent = EU.transform;
            }
            else if (grid[4, row] == "N. America")
            {
                newPoint.transform.parent = NA.transform;
            }
            else if (grid[4, row] == "S. America")
            {
                newPoint.transform.parent = SA.transform;
            }
            else if (grid[4, row] == "Middle East")
            {
                newPoint.transform.parent = ME.transform;
            }
            else if (grid[4, row] == "Sub-Saharan Africa")
            {
                newPoint.transform.parent = SSA.transform;
            }

            newPoint.GetComponent<Renderer>().material.color = newPoint.transform.parent.Find("Button").gameObject.GetComponent<Renderer>().material.color;
            /* if (grid[4, row] == "Asia") // Try grey colors for filtering. Result: Not good.
            {
                newPoint.GetComponent<Renderer>().material.color = new Color(0.1f, 0.1f, 0.1f, 1.0f);
                newPoint.transform.localScale *= 1.1f;
            } */
            newPoint.transform.parent.Find("Button").gameObject.GetComponent<FilterButton>().country = grid[4, row];
            newPoint.transform.localPosition = new Vector3((100.0f + int.Parse(grid[1, row])) * 0.004945f, 0.0f, (180.0f - int.Parse(grid[2, row])) * 0.00397f);
            /* if (row == 1)
             {
                 Debug.Log(newPoint.transform.position);
                 Debug.Log(newPoint.transform.localPosition);
             }    */

            //collect data for bar chart
            if (data.ContainsKey(grid[4, row]))
            {
                data[grid[4, row]].addCountry(grid[0, row], float.Parse(grid[3, row]));
            }
            else
            {
                data.Add(grid[4, row], new Countries(grid[0, row], float.Parse(grid[3, row])));
            }            
        }
        GameObject.Find("SubVis").GetComponent<SubVisManager>().createBars(data);
    }
    // outputs the content of a 2D array, useful for checking the importer
    
    void DebugOutputGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {

                textOutput += grid[x, y];
                textOutput += "|";
            }
            textOutput += "\n";
        }
        //Debug.Log(textOutput);
    }

    // splits a CSV file into a 2D string array
    public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);
        //Debug.Log(lines.Length);
        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);            
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];
                //Debug.Log(row[x]);
                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                //outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }
        //Debug.Log(outputGrid);
        return outputGrid;
    }

    // splits a CSV row 
    string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
                                                                                                            @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
                                                                                                            System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
   
    public void Update()
    {

    }
}

