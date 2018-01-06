using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class LoomDemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void readServerMsg(){
		//启用线程
		Loom.RunAsync(() =>{
			new Thread (theadThread).Start ();
		});
		//new Thread (theadThread).Start ();
	}
	/// <summary>
	/// 消息读取线程
	/// </summary>
	private void theadThread(){
		
	}
}
