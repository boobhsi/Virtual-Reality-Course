using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //private GameObject view;
    private Rigidbody rigidBody;
    public float walkingVelocity = 50f;
    public Text StopT;
    public bool halt = false;

    private float stopCounter = 0.0f;
    private GameObject view;

	// Use this for initialization
	void Start () {
        view = this.transform.GetChild(0).gameObject;
        rigidBody = this.GetComponent<Rigidbody>();
        StopT.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (halt)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }
        if (stopCounter >= 0)
        {
            stopCounter -= Time.deltaTime;
            if (stopCounter < 0) StopT.enabled = false;
            rigidBody.velocity = Vector3.zero;
        }
        else
        {
            rigidBody.velocity = new Vector3(this.view.transform.forward.x, 0, this.view.transform.forward.z).normalized * walkingVelocity + new Vector3(0, -5f, 0);
        }
	}
    void Stop(float value) {
        stopCounter = value;
        StopT.enabled = true;
    }
}
