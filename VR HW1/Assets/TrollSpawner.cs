using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrollSpawner : MonoBehaviour {

    public GameObject TrollCandidate;
    public List<Transform> SpawnPoint;

    public float spawnTime = 10.0f;
    private float spawnCounter = 0.0f;
    public Text catched;
    private int TrollCount = 0;
    public int TrollLimit = 10;
    private int TrollDieCount = 0;

    // Use this for initialization
	void Start () {
        catched.text = "Catch:" + TrollController.TrollDie;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnCounter <= 0 && TrollCount < TrollLimit)
        {
            GameObject newTroll = GameObject.Instantiate(TrollCandidate);
            newTroll.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
            newTroll.GetComponent<TrollController>().Parent = this;
            spawnCounter = spawnTime;
            TrollCount += 1;
        }
        else spawnCounter -= Time.deltaTime;
	}

    public void setCatch() {
        catched.text = "Catch:" + TrollDieCount;
    }

    public void TrollDie() {
        TrollDieCount += 1;
        TrollCount -= 1;
        setCatch();
    }

    public void TrollFlee() {
        TrollCount -= 1;
    }

}
