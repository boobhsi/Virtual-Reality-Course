using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoundaryController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //norm = this.transform.right;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        other.SendMessage("Stop", 1.1f);

        float nowRotation = other.transform.eulerAngles.y;

        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.DORotate(new Vector3(0, nowRotation + 180, 0), 1f);
            other.transform.DOMove(other.transform.position + this.transform.right.normalized * 2, 1f);
        }
        else if (other.gameObject.CompareTag("Troll")) other.SendMessage("RunOut");
    }
}
