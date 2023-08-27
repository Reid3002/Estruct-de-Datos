using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
   private Queue<GameObject> snake = new Queue<GameObject>();
   public int index;
   private GameObject lastObjectAdded;
    private float offSet = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        snake.Enqueue(GameObject.Find("Player"));
        lastObjectAdded = GameObject.Find("Player");
    }

  

    // Update is called once per frame
    void Update()
    {
         
    }

    public void AddToQueue(GameObject npc)
    {        
        //GameObject temp = snake.Dequeue();
        //snake.Enqueue(temp);

        snake.Enqueue(npc); 
        
        if(npc.TryGetComponent<NpcController>(out NpcController controller))
        {
            npc.GetComponent<NpcController>().index = snake.Count;
            //npc.GetComponent<NpcController>().nextInLine = temp;
            npc.GetComponent<NpcController>().inQeue = true;
                   
        }

            npc.GetComponent<NpcController>().nextInLine = lastObjectAdded;

            if (snake.Count > 1)
            {
                switch (lastObjectAdded.GetComponent<NpcController>().lastSuccessfulDirection)
                {
                    case NpcController.Direction.Left:
                        transform.position = lastObjectAdded.transform.position + Vector3.right * offSet;
                        break;

                    case NpcController.Direction.Right:
                        transform.position = lastObjectAdded.transform.position + Vector3.left * offSet;
                        break;

                    case NpcController.Direction.Up:
                        transform.position = lastObjectAdded.transform.position + Vector3.down * offSet;
                        break;

                    case NpcController.Direction.Down:
                        transform.position = lastObjectAdded.transform.position + Vector3.up * offSet;
                        break;

                }
            }
            else
            {
                switch (lastObjectAdded.GetComponent<PlayerController>().lastSuccessfulDirection)
                {
                    case PlayerController.Direction.Left:
                        transform.position = lastObjectAdded.transform.position + Vector3.right * offSet;
                        break;

                    case PlayerController.Direction.Right:
                        transform.position = lastObjectAdded.transform.position + Vector3.left * offSet;
                        break;

                    case PlayerController.Direction.Up:
                        transform.position = lastObjectAdded.transform.position + Vector3.down * offSet;
                        break;

                    case PlayerController.Direction.Down:
                        transform.position = lastObjectAdded.transform.position + Vector3.up * offSet;
                        break;

                }
            }
              

        lastObjectAdded = npc;
    }
}
