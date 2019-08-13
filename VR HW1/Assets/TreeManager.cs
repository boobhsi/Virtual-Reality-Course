using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour {

    public List<TreeController> TreeList;
    public PlayerController player;
    public Text TC;
    public Text SoG;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (TreeList.Count == 0)
        {
            player.halt = true;
            SoG.text = "Game Over!!";
            SoG.enabled = true;
        }
    }

    public void setTC() {
        TC.text = "Tree:" + TreeList.Count;
    }
}
