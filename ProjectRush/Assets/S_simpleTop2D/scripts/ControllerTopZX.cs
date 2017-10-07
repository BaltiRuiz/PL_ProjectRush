
using UnityEngine;

public class ControllerTopZX : MonoBehaviour {

    public AudioSource levelMusic;

	public float moveSpeed;

    private float extraSpeed;

    new Rigidbody rigidbody;

	Action action;

	Camera viewCamera;

	Vector3 velocity;

	void Start ()
    {
		rigidbody = GetComponent<Rigidbody>();

		action = GetComponent<Action>();

		viewCamera = Camera.main;
	}

	void Update ()
    {        	
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));

		transform.LookAt (mousePos + Vector3.up * transform.position.y);

        extraSpeed = 1;

        if (Input.GetKey(KeyCode.C))
            extraSpeed = 0.5f;

        else if (Input.GetKey(KeyCode.X))
            extraSpeed = 2;

        levelMusic.pitch = 1 * extraSpeed;

		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed * extraSpeed;

        if (velocity != Vector3.zero){
            if (extraSpeed == 1 && action.CurrentAction != (int)ActionIs.Walk)
                action.CurrentAction = (int)ActionIs.Walk;

            else if (extraSpeed == 0.5f && action.CurrentAction != (int)ActionIs.Sneaky)
                action.CurrentAction = (int)ActionIs.Sneaky;

            else if (extraSpeed == 2 && action.CurrentAction != (int)ActionIs.Run)
                action.CurrentAction = (int)ActionIs.Run;
        }

        else if (velocity == Vector3.zero && action.CurrentAction != (int)ActionIs.Stop)
            action.CurrentAction = (int)ActionIs.Stop;
	}

	void FixedUpdate()
    {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}
}