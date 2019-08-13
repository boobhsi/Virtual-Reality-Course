using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {


    private ParticleSystem PS;
    private Animator PA;

    public AudioSource BurstSFX;

    private Vector3 target;

    // Use this for initialization
    void Start() {
        PS = this.GetComponent<ParticleSystem>();
        PA = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SelfDestroy() {
        DestroyProcedures();
    }

    public void DestroyByPlayer() {
        //Debug.Log("success");
        StopCoroutine("FowardToPlayer");
        PA.SetTrigger("Die");
    }

    public void OnTriggerEnter(Collider other) {
        //Debug.Log(other);
        if (other.CompareTag("Player")) {
            StopCoroutine("FowardToPlayer");
            this.transform.parent.SendMessage("ForceDeQueue", this.gameObject.GetInstanceID());
            other.SendMessage("Hit");
            SelfDestroy();
        }
    }

    public void StartForward(Vector3 player) {
        target = player;
        StartCoroutine("FowardToPlayer");
    }

    IEnumerator FowardToPlayer() {
        while (true)
        {
            Vector3 direction = (target - this.transform.position).normalized;
            this.transform.position += direction * this.transform.parent.GetComponent<OrderedEmitter>().ParticleVelocity * Time.deltaTime;
            yield return null;
        }
    }

    void DestroyProcedures() {
        Debug.Log("DP");
        Destroy(this.gameObject);
    }

    public void ParticleBurst() {
        BurstSFX.Play();
        PS.Play();
        StartCoroutine("HellTime");
    }

    IEnumerator HellTime() {
        while (PS.isPlaying) {
            yield return null;
        }
        DestroyProcedures();
    }
}
