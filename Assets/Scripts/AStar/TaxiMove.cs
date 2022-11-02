using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiMove : MonoBehaviour
{
    public static bool isMoving = false;
    public int delayTime = 1;

    private List<AStarNode> Movelist;
    private Dictionary<string, GameObject> cubes;
    public float speed = 150.0f;
    Vector3 nextPos;
    int i; // counter for movelist 
    // Start is called before the first frame update
    void Start()
    {
        Movelist = GameObject.Find("Main Camera").GetComponent<TestAStar>().getList();
        cubes = GameObject.Find("Main Camera").GetComponent<TestAStar>().getCubes();
        i = 1;
        nextPos = cubes[Movelist[i].x + "_" + Movelist[i].y].transform.position;
        modifyAngle(nextPos);

    }

    // Update is called once per frame
    void Update()
    {
        isMoving = true;

        if (this.transform.position != nextPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextPos, speed * Time.deltaTime);
        }
        else if (i + 1 < Movelist.Count)
        {
            i++;
            nextPos = cubes[Movelist[i].x + "_" + Movelist[i].y].transform.position;
            modifyAngle(nextPos);
        }
           
        else
        {
            Invoke("ReverseMoveState", delayTime);
            Destroy(gameObject, delayTime);
        }          
    }
    
    private void ReverseMoveState()
    {
        isMoving = false;
    }

    // Modify angle of the object and make its back up toward screen and face to the next position
    private void modifyAngle(Vector3 nextPos)
    {
        this.transform.rotation = Quaternion.LookRotation(nextPos - this.transform.position);
        Vector3 dirction = (nextPos - this.transform.position).normalized;
        if (dirction.x == 0 && dirction.y > 0) { }
        else if (dirction.y == -1) this.transform.Rotate(Vector3.forward * 180, Space.Self); 
        else if (dirction.x < 0) this.transform.Rotate(Vector3.forward * 90, Space.Self);
        else this.transform.Rotate(Vector3.back * 90, Space.Self);
    }

}
