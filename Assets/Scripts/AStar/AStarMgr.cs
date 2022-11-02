using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Star way finder manager 
/// </summary>
public class AStarMgr 
{
    private static AStarMgr instance;
    public static AStarMgr Instance
    {
        get
        {
            if (instance == null)
                instance = new AStarMgr();
            return instance;
        }
    }

    private int mapW;
    private int mapH;

    //The container of all nodes class about the map
    public AStarNode[,] nodes;

    private List<AStarNode> openList = new List<AStarNode>();
    private List<AStarNode> closeList = new List<AStarNode>();

    /// <summary>
    /// Initalize map info
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w, int h)
    {
        this.mapW = w;
        this.mapH = h;

        nodes = new AStarNode[w, h];
        // create nodes according to the wight and height; random set block since we do not have map info now
        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
            {
               if (Random.value < 0.2)
                    nodes[i, j] = new AStarNode(i, j, E_Node_Type.Stop);
               else
                    nodes[i, j] = new AStarNode(i, j, E_Node_Type.Walk);
            }               
    }

    /// <summary>
    /// Find Path Function
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {
        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];

        // judge whether the inputted positions are valid: 1. in the range of map; 2. not block node 
        if ((startPos.x < 0 || startPos.x >= mapW || startPos.y < 0 || startPos.y >= mapH) || start.type == E_Node_Type.Stop)
        {
            Debug.Log("Start position or end position is out of range");
            return null;
        }
        if ((endPos.x < 0 || endPos.x >= mapW && endPos.y < 0 || endPos.y >= mapH) || end.type == E_Node_Type.Stop)
        {
            Debug.Log("Start position or end position is unwalkable");
            return null;
        }
        // clear the data from the last time 
        // clear open and close list
        closeList.Clear();
        openList.Clear();
        // add start node into closeList
        start.parent = null;        
        start.f = 0;        
        start.g = 0;        
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            // find nodes around and judge whether they are in the open or close list
            // up-left
           FindNearNode(start.x - 1, start.y - 1, 1.4f, start, end);
            //up
            FindNearNode(start.x, start.y - 1, 1f, start, end);
            //up-right
            FindNearNode(start.x + 1, start.y - 1, 1.4f, start, end);
            //left
            FindNearNode(start.x - 1, start.y, 1f, start, end);
            //right
            FindNearNode(start.x + 1, start.y, 1f, start, end);
            //down-left
            FindNearNode(start.x - 1, start.y + 1, 1.4f, start, end);
            //down
            FindNearNode(start.x, start.y + 1, 1f, start, end);
            //down-right
            FindNearNode(start.x + 1, start.y + 1, 1.4f, start, end);

            if (openList.Count == 0)
            {
                Debug.Log("Dead way");
                return null;
            }

            // select the node with least f in the openList out and put it into the closeList
            openList.Sort(SortOpenList);
            /*for(int i = 0; i < openList.Count; i++)
            {
                Debug.Log(openList[i].f + " , ");
            }
            Debug.Log(" _____________________________________");*/
            closeList.Add(openList[0]);
            start = openList[0];
            openList.RemoveAt(0);

            if (start == end)
            {
                //have found the way
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.parent != null)
                {
                    path.Add(end.parent);
                    end = end.parent;
                }
                path.Reverse();
                return path; 
            }
        }
      
    }

    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f > b.f)
            return 1;
        else if (a.f == b.f)
            return 1;
        else
            return -1;
    }

    // find the node  and judge whether it is in the open or close list
    private void FindNearNode(int x, int y, float g, AStarNode father, AStarNode end)
    {
        // judge whether the node is valid 
        if (x < 0 || x >= mapW || y < 0 || y >= mapH)
            return;
        AStarNode node = nodes[x, y];
        if (node == null || node.type == E_Node_Type.Stop || closeList.Contains(node) || openList.Contains(node))
            return;
        // compute the value of f
        node.parent = father;
        node.g = father.g + g;
        node.h = Mathf.Abs(x - end.x) + Mathf.Abs(y - end.y);
        node.f = node.g + node.h;
        openList.Add(node);
    }
}