using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour {

    GameObject mainCamera;
    Camera innerCamera;
    public int direction = 0;
    public const int FACE_UP = 0;
    public const int FACE_DOWN = 1;
    public const int FACE_LEFT = 2;
    public const int FACE_RIGHT = 3;
    public float moveSpeed = .05f;
    public int rayLayerMask;
    public UnityEngine.UI.Text textBox;
    public static int doorEntered;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");
        textBox = GameObject.Find("TextBox").GetComponent<UnityEngine.UI.Text>();
        Debug.Log(textBox);
        innerCamera = (Camera)mainCamera.GetComponent("Camera");
        rayLayerMask = LayerMask.GetMask("UI");
        rayLayerMask = ~rayLayerMask;
    }
	
	// Update is called once per frame
	void Update () {
        readKeys();
	}

    void readKeys()
    {

        /*Vector3 rayOrigin;
        RaycastHit hit;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            direction = FACE_UP;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            
            direction = FACE_DOWN;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            direction = FACE_LEFT;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            direction = FACE_RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Pressed confirm: " + rayLayerMask);
            if(direction == FACE_LEFT)
            {
                rayOrigin = innerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                rayOrigin = new Vector3(rayOrigin.x - 1.0f, rayOrigin.y, rayOrigin.z);
            }
            else if (direction == FACE_RIGHT)
            {
                rayOrigin = innerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                rayOrigin = new Vector3(rayOrigin.x + 1.0f, rayOrigin.y, rayOrigin.z);
            }
            else if (direction == FACE_DOWN)
            {
                rayOrigin = innerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                rayOrigin = new Vector3(rayOrigin.x, rayOrigin.y - 1.0f, rayOrigin.z);
            }
            else
            {
                rayOrigin = innerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                rayOrigin = new Vector3(rayOrigin.x, rayOrigin.y + 1.0f, rayOrigin.z);
            }
            Debug.Log(rayOrigin);
            Debug.Log(innerCamera.transform.forward);
            if (Physics.Raycast(rayOrigin, innerCamera.transform.forward, out hit, 1000f, rayLayerMask)) 
            {
                Debug.Log("Raycast Hit Something (Confirm button pressed)");
                if (hit.collider.tag == "NPC")
                {
                    Debug.Log("Raycast Hit NPC");
                    NPCHit(hit.collider);
                }
                else
                {
                    Debug.Log(hit.collider.tag);
                }
            }
        }*/
    }

    public void NPCHit(Collider coll)
    {
        if(coll.gameObject.name == "Villager1")
        {
            Debug.Log("Example Villager 1");
            textBox.text = "Example Villager 1 Clicked";
        }
        else if(coll.gameObject.name == "Villager2")
        {
            Debug.Log("Example Villager 2");
            textBox.text = "Example Villager 2 Clicked";
            SceneManager.LoadScene("CombatScene");
        }
    }
}
