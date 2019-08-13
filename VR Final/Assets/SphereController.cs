using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {

    private Object[] objects;
    private Texture[] textures;
    private Material goMaterial;
    private int frameCounter = 0;

    private bool isRendered = false;

    public int frameRate = 30;

    void Awake() {
        this.goMaterial = this.GetComponent<MeshRenderer>().material;
    }

	// Use this for initialization
	void Start () {
        this.objects = Resources.LoadAll("BackGround", typeof(Texture));
        Debug.Log(objects.Length);
        this.textures = new Texture[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            this.textures[i] = (Texture)this.objects[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isRendered) StartCoroutine("PlayLoop", 1.0f / frameRate);
        goMaterial.mainTexture = textures[frameCounter];
        Debug.Log(frameCounter);
	}

    IEnumerator PlayLoop(float delay) {
        isRendered = true;
        yield return new WaitForSeconds(delay);

        frameCounter = (++frameCounter) % textures.Length;

        isRendered = false;
        StopCoroutine("PlayLoop");
    }
}
