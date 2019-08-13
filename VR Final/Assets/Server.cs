using UnityEngine;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Server : MonoBehaviour
{
	private ServerThread st;
	private bool isSend;//儲存送訊息是否發完畢

    public GameObject AA;

	private List<string> inputBuffer;
	private string[] inputCandidates = {"w", "a", "s", "d"};

	private void Start()
	{
		//開始連線，設定使用網路、串流、TCP
		//Debug.Log("Hello world!");
		inputBuffer = new List<string> ();
		st = new ServerThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "0.0.0.0", 8000);
		st.Listen();//讓Server socket開始監聽連線
		st.StartConnect();//開啟Server socket
	}

	private void Update()
	{
        if (st.receiveMessage != null) {
            Debug.Log(st.receiveMessage);
            foreach (char c in st.receiveMessage){
				string s = System.Convert.ToString (c);
                
                if (inputCandidates.Contains(s)) {
					inputBuffer.Add(s);
					Debug.Log("Client:" + s);
				}
			}
			st.receiveMessage = null;
		}
		if (isSend == true)
			StartCoroutine(delaySend());//延遲發送訊息

		if (inputBuffer.Count () != 0) {
			string input = inputBuffer [0];
			inputBuffer.RemoveAt (0);
			Debug.Log("Input:" + input);
			if (input == "d") {
                AA.SendMessage("OnInput", 4.0f);
			}
			if (input == "a")
			{
                AA.SendMessage("OnInput", 3.0f);
			}
			if (input == "s")
			{
                AA.SendMessage("OnInput", 2.0f);
			}
			if (input == "w")
			{
                Debug.Log("w");
                AA.SendMessage("OnInput", 1.0f);
			}
		}

		st.Receive();
	}

	private IEnumerator delaySend()
	{
		isSend = false;
		yield return new WaitForSeconds(0.01f);//延遲1秒後才發送
		st.Send("Hello~ My name is Server");
		isSend = true;
	}

	private void OnApplicationQuit()//應用程式結束時自動關閉連線
	{
		st.StopConnect();
	}
}