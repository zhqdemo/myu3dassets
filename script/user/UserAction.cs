using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAction : MonoBehaviour {
	public float speed = 1f;
	private bool moving = false;
	private Vector3 mousePosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		right2move ();
	}

	void right2move(){
		if(Input.GetMouseButton(1)){
			mousePosition = MoveScript.getClickPosition (transform);
			//Vector3 p = MoveScript.getClickPosition (transform);
			Debug.Log ((mousePosition-transform.position).magnitude);
			//transform.position=Vector3.MoveTowards(transform.position,p,speed * Time.deltaTime);
			//transform.position = p;
			moving = true;
		}
		if(moving){
			if((mousePosition-transform.position).magnitude<=0){
				moving = false;
			}
			transform.position=Vector3.MoveTowards(transform.position,mousePosition,speed);
		}
	}

}
