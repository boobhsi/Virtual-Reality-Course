using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
	private ClientThread ct;
	private bool isSend;
	private bool isReceive;

	private void Start()
	{
		// 39.9.131.193
		// 10.40.149.114
		ct = new ClientThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "127.0.0.1", 8000);
		ct.StartConnect();
	}

	private void Update()
	{
		if (ct.receiveMessage != null)
		{
			Debug.Log("Server:" + ct.receiveMessage);
			ct.receiveMessage = null;
		}
			
		if (Input.GetKeyDown("w"))
		{
			StartCoroutine(keySend ("w"));
		}
		if (Input.GetKeyDown("a"))
		{
			StartCoroutine(keySend ("a"));
		}
		if (Input.GetKeyDown("s"))
		{
			StartCoroutine(keySend ("s"));
		}
		if (Input.GetKeyDown("d"))
		{
			StartCoroutine(keySend ("d"));
		}

		ct.Receive();
	}
		
	private IEnumerator keySend(string keyboardInput)
	{
		Debug.Log (keyboardInput);
		yield return new WaitForSeconds(0.1f);
		ct.Send(keyboardInput);
	}

	private void OnApplicationQuit()
	{
		ct.StopConnect();
	}
}