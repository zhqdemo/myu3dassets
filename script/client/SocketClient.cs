using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using SimpleJSON;

public class SocketClient : MonoBehaviour {
	private TcpClient client;//TCP连接
	private NetworkStream streamToServer;//发送到服务器的字节流
	private StreamReader read;//读取服务器发过来的字节流
	private static SocketClient myself;//自己的单例对象
	[SerializeField] private string serverIp = "127.0.0.1";//服务器IP
	[SerializeField] private int serverPort = 1013;//服务器端口号

	/// <summary>
	/// 获取自己的单例对象
	/// </summary>
	public static SocketClient instants(){		
		return SocketClient.myself;
	}
	// Use this for initialization
	void Start () {
		initClient ();
		myself = this;
	}	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake(){
		Loom.Initialize();
	}

	private SocketClient(){
		
	}
	private void initClient(){
		try{
			client = new TcpClient();  
			//client.Connect (IPAddress.Parse (serverIp), serverPort);
			client.Connect (serverIp, serverPort);
			streamToServer = client.GetStream();//获得客户端的流 
			read = new StreamReader(streamToServer);
			readServerMsg();
		}catch(Exception e){
			Debug.Log (e);
		}
	}
	/// <summary>
	/// 当程序退出或关闭时，服务器连接
	/// </summary>
	public void OnApplicationQuit(){
		try{
			read.Close ();
			streamToServer.Close();
			client.Close ();
		}catch(Exception e){
			Debug.Log (e);
		}
	}
	public void sendmsg(JSONNode msg){
		string a = msg.ToString()+"\n"; 
		byte[] byteArray = System.Text.Encoding.Default.GetBytes ( a );
		try{  
			streamToServer.Write(byteArray, 0, byteArray.Length);//将转换好的二进制数据写入流中并发送 
			Debug.Log("发出消息：" + a); 
		}catch(Exception ex){  
			Debug.Log("服务端产生异常："+ex.Message);  
		}  
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
		String msg = read.ReadLine();
		while(null!=msg){
			Debug.Log("服务器发过来的数据"+msg);
			//想指令队列中加入需要执行的指令
			JSONNode obj = JSON.Parse (msg);
			//MySocketClient.instants().addAction(obj);
			//发送消息给主线程，主线程update方法调用时会会执行这个函数
			Loom.QueueOnMainThread(()=>{ 
				readAction(obj);
			});
			msg = read.ReadLine ();
		}
	}
	/// <summary>
	/// 解析指令
	/// </summary>
	private void readAction(JSONNode actionTemp){

		try{
			if(actionTemp.IsArray){
				JSONArray array = (JSONArray)actionTemp;
				for(int i = 0;i<array.Count;i++){
					JSONObject obj = (JSONObject)array[i];
					runAction(obj);
				}
			}else{
				JSONObject obj = (JSONObject)actionTemp;
				runAction(obj);
			}

		}catch(Exception e){
			Debug.LogError (e);
		}
	}
	/// <summary>
	/// 运行指令
	/// </summary>
	/// <param name="obj">Object.</param>
	private void runAction(JSONObject obj){
		//ActionParser.runAction (obj);
	}

}
