using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using System.Text;

public class TestAStar : MonoBehaviour
{
    // The first box on the top left
    public int beginX = -3;
    public int beginY = 5;
    // Offset between every box
    public float offsetX = 2;
    public float offsetY = -2;
    // The wight and height of every map node
    public int mapW = 5;
    public int mapH = 5;

    List<AStarNode> list;

    public Material red;
    public Material yellow;
    public Material green;
    public Material normal;
    public GameObject taxi;
    public GameObject cubeParent;

    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    private Vector2 beginPos = Vector2.right * -1;

    // Start is called before the first frame update
    void Start()
    {
        AStarMgr_Optimal.Instance.InitMapInfo(mapW, mapH);

        for(int i = 0; i < mapW; i++)
            for(int j = 0; j < mapH; j++)
            {
                // Create cubes
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);
                obj.GetComponent<MeshRenderer>().material = normal;
                obj.name = i + "_" + j;
                obj.transform.parent = cubeParent.transform;

                cubes.Add(obj.name, obj);

                // Get nodes and judge whether it is block 
                AStarNode node = AStarMgr_Optimal.Instance.nodes[i, j];
                if(node.type == E_Node_Type.Stop)
                {
                    obj.GetComponent<MeshRenderer>().material = red; 
                }
            }
        // Modify the postion of the camera
        float cameraX = (mapW / 5 - 1) * 2.5f;
        float cameraY = 2 - (mapH / 5 -1f) * 2.5f;
        float cameraZ = (0 - 9) * mapW / 5;
        transform.position = new Vector3(cameraX,cameraY,cameraZ);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // ray test
            if(Physics.Raycast(ray, out info, 1000) && TaxiMove.isMoving == false)
            {
                if(beginPos == Vector2.right * -1)
                {
                    // clear path last time 
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = normal;
                        }
                        list.Clear();
                    }
                    if (info.collider.gameObject.name.Contains("_"))
                    {
                        string[] strs = info.collider.gameObject.name.Split('_');
                        beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                        info.collider.gameObject.GetComponent<MeshRenderer>().material = yellow;
                    }
                    else
                    {
                        Debug.Log("This position is not in service area.");
                    }
                }
                else if (TaxiMove.isMoving == false)
                {
                    string[] strs = info.collider.gameObject.name.Split('_');
                    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                    // Record time of finding
                    float strTime = Time.realtimeSinceStartup; 
                    // Find way
                    list = AStarMgr_Optimal.Instance.FindPath(beginPos, endPos);

                    float endTime = Time.realtimeSinceStartup;
                    Debug.Log("Time for finding: " + (endTime - strTime));
                    //AddTxtText(endTime - strTime + " "); // Record time 

                    // In case of dead way
                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material = normal;

                    if (list != null)
                    {
                        GameObject car = Instantiate(taxi, cubes[(int)beginPos.x + "_" + (int)beginPos.y].transform );
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = green;
                        }
                    }

                    // When animation ends, clear the inital position
                    beginPos = Vector2.right * -1;
                }
            } 
        }
    }

    // This function is used to record the time of find-route method 
    public void AddTxtText(string txtText)
    {
        string path = Application.dataPath + "/Temp.txt"; //"/AStarNoOptimalTimeInfo.txt";
        StreamWriter sw;
        FileInfo fi = new FileInfo(path);

        if (!File.Exists(path))
        {
            sw = fi.CreateText();
        }
        else
        {
            sw = fi.AppendText();   // Append text to the document  
        }
        sw.WriteLine(txtText);
        sw.Close();
        sw.Dispose();
    }

    public List<AStarNode> getList()
    {
        return list;
    }

    public Dictionary<string, GameObject>  getCubes()
    {
        return cubes;
    }
}
