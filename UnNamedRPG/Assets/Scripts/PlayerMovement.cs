using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

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
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rayLayerMask = LayerMask.GetMask("UI");
        rayLayerMask = ~rayLayerMask;
        textBox = GameObject.FindGameObjectWithTag("Text").GetComponent<UnityEngine.UI.Text>();
    }
	
	// Update is called once per frame
	void Update () {
        readKeys();
	}

    void readKeys()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10;
        }
        else
        {
            moveSpeed = 5;
        }
        float horz = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(horz * moveSpeed, rb.velocity.y, 0);
        float vert = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, vert * moveSpeed, 0);
        //Debug.Log("Inputs: " + horz + ": " + vert);

        Vector3 rayOrigin;
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
            rayOrigin = transform.position;
            Vector3 rayDirection;
            if (direction == FACE_LEFT)
            {
                rayDirection = new Vector3(-2, 0, 0);
            }
            else if (direction == FACE_RIGHT)
            {
                rayDirection = new Vector3(2, 0, 0);
            }
            else if (direction == FACE_DOWN)
            {
                rayDirection = new Vector3(0, -2, 0);
            }
            else
            {
                rayDirection = new Vector3(0, 2, 0);   
            }
            Debug.Log(rayOrigin + ": " + rayDirection);
            Debug.DrawRay(rayOrigin, rayDirection, Color.black, 1.0f, true);
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 1.2f, rayLayerMask))
            {
                Debug.Log("Raycast Hit Something (Confirm button pressed)");
                if (hit.collider.tag == "NPC")
                {
                    Debug.Log("Raycast Hit NPC");
                    NPCHit(hit.collider);
                }
                else if(hit.collider.tag == "Door")
                {
                    Debug.Log("Raycast Hit Door");
                    DoorHit(hit.collider);
                }
                else
                {
                    Debug.Log(hit.collider.tag);
                }
            }
        }
    }

    

        

    public void NPCHit(Collider coll)
    {
        if (coll.gameObject.name == "Villager1")
        {
            Debug.Log("Example Villager 1");
            textBox.text = "Example Villager 1 Clicked";
        }
        else if (coll.gameObject.name == "Villager2")
        {
            Debug.Log("Example Villager 2");
            textBox.text = "Example Villager 2 Clicked";
            SceneManager.LoadScene("CombatScene");
        }
        
    }

    public void DoorHit(Collider coll)
    {
        if(coll.gameObject.name == "Door1a")
        {
            Debug.Log("Example Door 1");
            textBox.text = "Entered Door 1";
            MoveController.doorEntered = 1;
            SceneManager.LoadScene("ExploreScene");
        }
        if(coll.gameObject.name == "Door1b")
        {
            Debug.Log("Example Door 1");
            textBox.text = "Entered Door 1";
            MoveController.doorEntered = 2;
            SceneManager.LoadScene("ExploreScene");
        }
        if(coll.gameObject.name == "Door2a")
        {
            Debug.Log("Example Door 2");
            textBox.text = "Entered Door 2";
            MoveController.doorEntered = 1;
            SceneManager.LoadScene("DoorScene1");
        }
        if (coll.gameObject.name == "Door2b")
        {
            Debug.Log("Example Door 2");
            textBox.text = "Entered Door 2";
            MoveController.doorEntered = 2;
            SceneManager.LoadScene("DoorScene1");
        }
        if (coll.gameObject.name == "Door3")
        {
            Debug.Log("Example Door 3");
            textBox.text = "Entered Door 3";
            MoveController.doorEntered = 1;
            SceneManager.LoadScene("DoorScene2");
        }
        if (coll.gameObject.name == "Door4")
        {
            Debug.Log("Example Door 4");
            textBox.text = "Entered Door 4";
            MoveController.doorEntered = 1;
            SceneManager.LoadScene("TutorialScene");
        }
    }
}
