using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController : MonoBehaviour {
    public float speed_of_move;
    public float speed_of_rotation;
	public float jumpPower;

	private bool jumping = false;

	public UIController uiController;
    // Use this for initialization
    // Update is called once per frame
    void Update() {

    }
    
    void FixedUpdate()
    {
        
		if (Input.GetKeyDown(KeyCode.Space) && !jumping )//jump
        {
			GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
			jumping = true;
        }
        
        
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        

        Vector3 GlobalPos = GetComponent<Rigidbody>().transform.position;

        
        Vector3 GlobalDirectionForward = GetComponent<Rigidbody>().transform.TransformDirection(Vector3.forward);
        Vector3 ForwardDirection = moveVertical * GlobalDirectionForward;
                                                              
        Vector3 GlobalDirectionRight = GetComponent<Rigidbody>().transform.TransformDirection(Vector3.right);
        Vector3 RightDirection = moveHorizontal * GlobalDirectionRight;

        
        Vector3 MainDirection = ForwardDirection + RightDirection;

        
        GetComponent<Rigidbody>().velocity = new Vector3(speed_of_move * MainDirection.x, GetComponent<Rigidbody>().velocity.y, speed_of_move * MainDirection.z);

        
        float x = speed_of_rotation * Input.GetAxis("Mouse X");
        GetComponent<Rigidbody>().transform.rotation = Quaternion.Euler(GetComponent<Rigidbody>().transform.rotation.eulerAngles + Quaternion.AngleAxis(x, Vector3.up).eulerAngles);
    }
    /*
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 currentPos = GetComponent<Rigidbody>().transform.position;
        Vector3 currentAng = GetComponent<Rigidbody>().transform.eulerAngles;
        Vector3 movement = new Vector3(moveVertical * Mathf.Tan(currentAng.y * Mathf.PI / 180), 0.0f, moveVertical) * Mathf.Cos(currentAng.y * Mathf.PI / 180);
        Vector3 rotation = new Vector3(0.0f, moveHorizontal, 0.0f);
        GetComponent<Rigidbody>().velocity = movement * speed_of_move;
        GetComponent<Rigidbody>().angularVelocity = rotation * speed_of_rotation;
    }
    */
    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "gem") {
			Destroy (other.gameObject);
			uiController.increaseGemCount ();
		} else if (other.gameObject.tag == "terrain") {
			jumping = false;
		} else if (other.gameObject.tag == "beast") {
			uiController.decreaseLifeCount ();
		}
    }
}
