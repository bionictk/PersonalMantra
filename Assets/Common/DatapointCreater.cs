
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class DatapointCreater : MonoBehaviour
{
    public TextAsset csvFile;
    public GameObject shapePrimitive;
    public int speed = 5;
    public bool showAllPoints = true;

    private string[,] grid;
    private int time = 0;
    GameObject movingShape;
    GameObject lemon;
    private float lastLemonID = -98765;
    private GameObject newGroup;
    public ArrayList lemonIDList = new ArrayList();

    public void Start()
    {
        grid = SplitCsvGrid(csvFile.text);
        Debug.Log("size = " + (1 + grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1)));
        
        //movingShape = GameObject.Instantiate(shapePrimitive) as GameObject;
        //movingShape.SetActive(true);
        //lemon.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        
        DebugOutputGrid(grid); 
    }

    // outputs the content of a 2D array, useful for checking the importer
    static public void DebugOutputGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0) - 1; x++)
            {

                textOutput += grid[x, y];
                textOutput += "|";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }

    // splits a CSV file into a 2D string array
    static public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

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

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    // splits a CSV row 
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
                                                                                                            @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
                                                                                                            System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
    /*
    void DrawPoints()
    {
        Vector3 nextPosition = new Vector3();
        Vector3 prevPosition = new Vector3();
        int pointCount = 0, buttonLines = 0;
        for (int line = 0; line < grid.GetUpperBound(1); line++)
        {
            switch (grid[0, line])
            {
                case "AIM":
                    //Debug.Log(grid[2, time]);
                    float lemonID = float.Parse(grid[2, line]);
                    if (lemonID == -999) break;
                    if (lastLemonID != lemonID)
                    {

                        shapeColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                        lastLemonID = lemonID;
                        GameObject newButton = GameObject.Instantiate(buttonShape) as GameObject;
                        newButton.transform.parent = groupbutton.transform;
                        newButton.transform.localPosition = new Vector3((lemonIDList.Count % 5) * 0.05f, buttonLines * (-0.05f), 0);
                        newButton.transform.Find("Canvas/MidGraphics/MidText").gameObject.GetComponent<Text>().text = "" + (lemonIDList.Count + 1);
                        newButton.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        newButton.SetActive(true);
                        newGroup = new GameObject("Group" + lemonID);
                        //newButton.transform.Find("Button").gameObject.GetComponent<GroupButtonScript>().pointGroup = newGroup;
                        newGroup.transform.parent = transform;
                        newGroup.transform.localPosition = new Vector3(0, 0, 0);
                        //newGroup.AddComponent<SpeechBubble>();
                        lemonIDList.Add(lemonID);
                        if (lemonIDList.Count % 5 == 0) buttonLines++;
                    }
                    pointCount++;
                    nextPosition = new Vector3(float.Parse(grid[6, line]), float.Parse(grid[7, line]), float.Parse(grid[8, line]));
                    nextPosition = nextPosition * scale;
                    GameObject shape = GameObject.Instantiate(shapePrimitive) as GameObject;
                    shape.transform.parent = newGroup.transform;
                    shape.transform.localScale = Vector3.one * thickness;
                    shape.transform.localPosition = nextPosition;
                    shape.SetActive(true);
                    shape.GetComponent<Renderer>().material.color = new Color(shapeColor.r, shapeColor.g, shapeColor.b, shapeColor.a);
                    /*if (pointCount > 0) {
                    //int n = Mathf.CeilToInt (180 * Mathf.Abs (Vector3.Distance (prevPosition, nextPosition))); //change n proportional to distance
                        int n=1;
                        //Debug.Log(n);
                    /	for (int i=1; i<n; i++) {
                            //lerp and draw
                            GameObject shape1 = GameObject.Instantiate (shapePrimitive) as GameObject;
                            shape1.transform.parent = transform;
                            shape1.transform.localScale = Vector3.one * thickness;
                            shape1.transform.localPosition = Vector3.Lerp (prevPosition, nextPosition, (float)i / n);
                            shape1.SetActive (true);
                            shape1.GetComponent<Renderer> ().material.color = new Color (shapeColor.r, shapeColor.g, shapeColor.b, shapeColor.a);
                        }
                    }
                    prevPosition = nextPosition;
                    break;
                default:
                    //Debug.Log (grid[0,line]);
                    break;
            }
        }
        Debug.Log("Points drawn : " + pointCount);
    }
    */
    public void Update()
    {

    }
}