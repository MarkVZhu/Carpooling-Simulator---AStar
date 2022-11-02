using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CreateCubesInEditor : MonoBehaviour
{
    // The first box on the top left
    public int beginX = -3;
    public int beginY = 5;
    // Offset between every box
    private float offsetX = 1;
    private float offsetY = -1;
    // The wight and height of every map node
    public int mapW = 103;
    public int mapH = 210;

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
    void Awake()
    {
        AStarMgr_Optimal.Instance.InitMapInfo(mapW, mapH);

        for (int i = 0; i < mapW; i++)
            for (int j = 0; j < mapH; j++)
            {
                // Create cubes
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);
                obj.GetComponent<MeshRenderer>().material = normal;
                obj.name = i + "_" + j;
                obj.transform.parent = cubeParent.transform;
                obj.GetComponent<MeshRenderer>().material = red;

                cubes.Add(obj.name, obj);

                /*// Get nodes and judge whether it is block 
                AStarNode node = AStarMgr_Optimal.Instance.nodes[i, j];
                if (node.type == E_Node_Type.Stop)
                {
                    obj.GetComponent<MeshRenderer>().material = red;
                }*/
            }
        // Modify the postion of the camera
        float cameraX = (mapW / 5 - 1) * 2.5f;
        float cameraY = 2 - (mapH / 5 - 1f) * 2.5f;
        float cameraZ = (0 - 9) * mapW / 5;
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }
}
