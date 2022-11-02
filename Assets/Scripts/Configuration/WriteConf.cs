using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteConf : MonoBehaviour
{
    private int mapW;
    private int mapH;

    public Material red;
    public Material normal;

    // Start is called before the first frame update
    void Start()
    {
        mapW = 103; //GameObject.Find("Main Camera").GetComponent<CreateCubesInEditor>().mapW;
        mapH = 210; // GameObject.Find("Main Camera").GetComponent<CreateCubesInEditor>().mapH;
       for(int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                GameObject obj = GameObject.Find(i + "_" + j);
                char type = obj.GetComponent<MeshRenderer>().material.name[0];
                Debug.Log(i + "_" + j + "_" + type);
                AddTxtText(i + "_" + j + "_" + type);
            }
        }
            
    }

    public void AddTxtText(string txtText)
    {
        string path = Application.dataPath + "/MapConfiguraion.txt"; //"/AStarNoOptimalTimeInfo.txt";
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
}
