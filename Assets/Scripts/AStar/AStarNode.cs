using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Node_Type
{
    Walk, // walkable place
    Stop, // unwalkable place
}
/// <summary>
/// A Star box/ node Class
/// </summary>
public class AStarNode : IComparer 
{
    //coordinate of the node 
    public int x;
    public int y;
    // comsumption of finding way: f = g + h
    public float f;
    // distance to the start position 
    public float g;
    //distance to the target position
    public float h;
    //parent class 
    public AStarNode parent;

    public E_Node_Type type;

    public AStarNode(int x, int y,  E_Node_Type type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    int IComparer.Compare(System.Object a, System.Object b)
    {
        if (((AStarNode)a).f== ((AStarNode)b).f) return 0;
        else if (((AStarNode)a).f > ((AStarNode)b).f) return 1;
        else return -1;
    }
}
