using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// 获取鼠标世界坐标
	/// </summary>
	/// <returns>The click position.</returns>
	public static Vector3 getClickPosition(Transform transform){
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position); // 目的获取z，在Start方法  
		Vector3 mousePos = Input.mousePosition;  
		mousePos.z = screenPos.z; // 这个很关键  
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);  
		return worldPos;   
	}
}
