using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float runningSpeed;
	private Rigidbody rb;
	private Vector3 movement;

	//Initialize the rigidbody variable
	void Start() {
		//runningSpeed = 10.0f;
		rb = GetComponent<Rigidbody> ();
	}
	// Update is called once per frame
	void Update () {
		
	}

	//Run Before performing any physics calcs 
	void FixedUpdate() { 
		float moveHorizontal = Input.GetAxis ("Horizontal");
	    float moveVertical = Input.GetAxis ("Vertical");

		//Update the position of the player upon every input from the user.
		movement = new Vector3 (moveHorizontal*(runningSpeed/15.0f), 0.0f, moveVertical*(runningSpeed/15.0f));
		rb.MovePosition (transform.position += movement);

		if (Input.mousePresent) {
			print ("mouse here");
			print (Input.mousePosition);
		
		
		/*		public GameObject particle;
				void Update() {
					if (Input.GetButtonDown("Fire1")) {
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						if (Physics.Raycast(ray))
							Instantiate(particle, transform.position, transform.rotation);
					}
				}*/

		
		
		
		
		
		
		
		
		} else {
			print ("No Mouse Detected");
		}


	/*	if (Input.GetKey ("w")) {
			print (transform.forward);
			movement = new Vector3 (0.0f, 0.0f, runningSpeed);
			//print (transform.position);
			rb.MovePosition (transform.position + movement * Time.deltaTime);
		}
		else if (Input.GetKey ("a")) {
			print (-transform.right);
			movement = new Vector3 (-runningSpeed, 0.0f, 0.0f);
			//print (transform.position);
			rb.MovePosition (transform.position + movement * Time.deltaTime);
		}
		else if (Input.GetKey ("s")) {
			print (-transform.forward);
			movement = new Vector3 (0.0f, 0.0f, -runningSpeed);
			//print (transform.position);
			rb.MovePosition (transform.position + movement * Time.deltaTime);
		}
		else if (Input.GetKey ("d")) {
			print (transform.right);
			movement = new Vector3 (runningSpeed, 0.0f, 0.0f);
			//print (transform.position);
			rb.MovePosition (transform.position + movement * Time.deltaTime);
		}*/
		//rb.MovePosition (transform.position + transform.forward * Time.deltaTime);
		//rb.MovePosition(movement);

	}
}
