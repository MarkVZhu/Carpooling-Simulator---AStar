using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class GenerateCubesByConf : MonoBehaviour
{
    public int mapW = 103;
    public int mapH = 210;
    public AStarNode[,] nodes;

    /// <summary>
    /// Read the configuration file of the map
    /// </summary>
    /// <param name="mapW"></param> The width of the map 
    /// <param name="mapH"></param> The height of the map
    /// <param name="filename"></param> The name of configuration file 
    /// <returns> AStarNode[,] </returns> Two-dimensional array of AStarNode
    public static AStarNode[,] GetMapConf (int mapW, int mapH, string filename) // "/MapConfiguraion.txt"
    {
        AStarNode[,] nodes = new AStarNode[mapW, mapH];
        string path = Application.dataPath + filename;
        StreamReader reader;
        reader = new StreamReader(path); 

        string _text;
        string[] lineArray;
        int i, j;

        while ((_text = reader.ReadLine()) != null)
        {
            lineArray = _text.Split('_');
            i = int.Parse(lineArray[0]);
            j = int.Parse(lineArray[1]);
            if (lineArray[2] == "r")
                nodes[i, j] = new AStarNode(i, j, E_Node_Type.Stop);
            else
                nodes[i, j] = new AStarNode(i, j, E_Node_Type.Walk);
        }
        reader.Dispose();
        reader.Close();
        return nodes;
    }
}
