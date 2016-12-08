using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	//Fires every Physics updates
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		//Call all helper functions.
		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move(float h, float v) 
	{
		movement.Set (h, 0f, v);  //Set the Vector3 variable movement
		movement = movement.normalized * speed * Time.deltaTime; //Speed is scale, Time updated every FixedUpdate.

		playerRigidbody.MovePosition (transform.position + movement); //Current position + new addition above.
	}

	//Uses rays to initiate mouse following.
	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); //Find the point underneath the mouse if it hits the floor Quad.

		RaycastHit floorHit;
		//out means the floorHit will be assigned values.
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;  //h not equal to 0 or v not equal to zero.
		anim.SetBool("IsWalking", walking);
	}
}