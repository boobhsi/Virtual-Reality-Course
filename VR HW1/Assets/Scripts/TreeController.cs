using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

    public int hp = 10;
    public TreeManager tm;
    public List<TrollController> TrollList;
    public List<ListController> DetectorList;

	// Use this for initialization
	void Start () {
        tm = this.GetComponentInParent<TreeManager>();
        tm.TreeList.Add(this);
	}

    void Hit(int value) {
        hp -= value;
        if (hp <= 0) {
            Die();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    void Die() {
        tm.TreeList.Remove(this);
        tm.setTC();
        this.GetComponents<Collider>()[0].enabled = false;
        TrollList.ForEach(delegate (TrollController a) {
            a.SendMessage("TreeMiss");
        });
        DetectorList.ForEach(delegate (ListController a) {
            a.CollisionObjects.Remove(this.GetComponent<Collider>());
        });
        GameObject.Destroy(this.gameObject);
    }
}
