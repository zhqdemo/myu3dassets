using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAction : MonoBehaviour {
	public float speed = 1f;
	private bool moving = false;
	private Vector3 mousePosition;
	float jiaodu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		right2move ();
	}

	void right2move(){
		if(Input.GetMouseButton(1)){
			mousePosition = MoveScript.getClickPosition (transform);
			//Vector3 p = MoveScript.getClickPosition (transform);
			//Debug.Log ((mousePosition-transform.position).magnitude);
			//transform.position=Vector3.MoveTowards(transform.position,p,speed * Time.deltaTime);
			//transform.position = p;
			moving = true;
			route (mousePosition);

		}
		if(moving){
			if((mousePosition-transform.position).magnitude<=0){
				moving = false;
			}
			transform.position=Vector3.MoveTowards(transform.position,mousePosition,speed);
		}
	}

	void route(Vector3 mousePosition){
		Vector3 a = mousePosition - transform.position;
		float fl = Vector3.Angle (transform.right, a);
		Debug.Log (fl);
		Vector3 r = Vector3.up*fl;

		transform.Rotate (r*Time.deltaTime,Space.Self);

	}
}
