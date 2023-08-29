using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
   private Queue<GameObject> snake = new Queue<GameObject>();
   public int index;
   private GameObject lastObjectAdded;
   private float offSet = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        snake.Enqueue(GameObject.Find("Player"));
        lastObjectAdded = GameObject.Find("Player");
        index++;
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

            if (lastObjectAdded.TryGetComponent<NpcController>(out NpcController contrller))
            {
                switch (lastObjectAdded.GetComponent<NpcController>().lastSuccessfulDirection)
                {
                    case NpcController.Direction.Left:
                        npc.transform.position = lastObjectAdded.transform.position + lastObjectAdded.transform.right * offSet;
                        break;

                    case NpcController.Direction.Right:
                        npc.transform.position = lastObjectAdded.transform.position + (lastObjectAdded.transform.right * -1) * offSet;
                        break;

                    case NpcController.Direction.Up:
                        npc.transform.position = lastObjectAdded.transform.position + (lastObjectAdded.transform.up * -1) * offSet;
                        break;

                    case NpcController.Direction.Down:
                        npc.transform.position = lastObjectAdded.transform.position + lastObjectAdded.transform.up * offSet;
                        break;

            }
            }
            else
            {
                switch (lastObjectAdded.GetComponent<PlayerController>().lastSuccessfulDirection)
                {
                    case PlayerController.Direction.Left:
                        npc. transform.position = lastObjectAdded.transform.position + lastObjectAdded.transform.right * offSet;
                        break;

                    case PlayerController.Direction.Right:
                        npc.transform.position = lastObjectAdded.transform.position + (lastObjectAdded.transform.right * -1) * offSet;
                        break;

                    case PlayerController.Direction.Up:
                        npc.transform.position = lastObjectAdded.transform.position + (lastObjectAdded.transform.up * -1) * offSet;
                        break;

                    case PlayerController.Direction.Down:
                        npc.transform.position = lastObjectAdded.transform.position + lastObjectAdded.transform.up * offSet;
                        break;

            }
            }
              

        lastObjectAdded = npc;
        index++;
    }
}
