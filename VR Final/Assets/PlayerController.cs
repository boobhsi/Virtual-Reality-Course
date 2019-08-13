using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float HP = 100.0f;

    public float HitPanelty = 5.0f;

    public GameObject UIController;

    public AudioSource YellSFX;

    public AudioSource Punch;

	// Use this for initialization
	void Start () {
        UIController.SendMessage("SetHP", HP);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator YellDelay() {
        yield return new WaitForSeconds(0.3f);
        YellSFX.Play();
        yield return false;
    }

    public void Hit() {
        if (!Punch.isPlaying)
        {
            Punch.Play();
            StartCoroutine("YellDelay");
        }
        HP -= HitPanelty;
        if (HP < 0.0f)
        {
            UIController.SendMessage("PlayerDiedAnimation");
        }
        else
        {
            UIController.SendMessage("PlayHitAnimation");
            UIController.SendMessage("SetHP", HP);
        }
    }
}
