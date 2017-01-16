using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    GameObject mainCamera;
    Camera innerCamera;
    public int direction = 0;
    public const int FACE_UP = 0;
    public const int FACE_DOWN = 1;
    public const int FACE_LEFT = 2;
    public const int FACE_RIGHT = 3;
    public int rayLayerMask;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");
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
        if(Input.GetKey(KeyCode.UpArrow))
        {
            mainCamera.transform.Translate(new Vector3(0, .05f));
            direction = FACE_UP;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            mainCamera.transform.Translate(new Vector3(0, -.05f));
            direction = FACE_DOWN;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            mainCamera.transform.Translate(new Vector3(-.05f, 0));
            direction = FACE_LEFT;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            mainCamera.transform.Translate(new Vector3(.05f, 0));
            direction = FACE_RIGHT;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Pressed confirm: " + rayLayerMask);
            Vector3 rayOrigin;
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
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, innerCamera.transform.forward, out hit, 1000f, rayLayerMask)) 
            {
                Debug.Log("Raycast Hit Something (Confirm button pressed)");
                if (hit.collider.tag == "NPC")
                {
                    Debug.Log("Raycast Hit NPC");
                }
                else
                {
                    Debug.Log(hit.collider.tag);
                }
            }
        }
    }
}
