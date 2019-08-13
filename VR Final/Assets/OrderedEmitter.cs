using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedEmitter : MonoBehaviour {

    public List<GameObject> PrefabsList;
    public float EmitterRadius;
    public float EmitterConeAngle;
    public float EmissionTime;

    public Transform NullTransform;


    //public GameObject ParticlesManager;
    public GameObject player;

    public float ParticleVelocity;

    //private 

    private Queue<GameObject> EmissionParticles;

    //private TrashHash

	// Use this for initialization
	void Start () {
        EmissionParticles = new Queue<GameObject>();
        StartCoroutine("RegularEmit");
        this.transform.localScale = new Vector3(1.0f / this.transform.parent.localScale.x, 1.0f / this.transform.parent.localScale.y, 1.0f / this.transform.parent.localScale.z);
        //StartCoroutine("ParticlesTransform");
        //EmissionParticles = new Queue<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (EmissionParticles.Count > 0)
        {
            if (Input.GetKeyDown("w"))
            {
                if (EmissionParticles.Peek().CompareTag("North"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    //temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            else if (Input.GetKeyDown("s"))
            {
                if (EmissionParticles.Peek().CompareTag("South"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    //temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            else if (Input.GetKeyDown("a"))
            {
                if (EmissionParticles.Peek().CompareTag("West"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    //temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            else if (Input.GetKeyDown("d"))
            {
                if (EmissionParticles.Peek().CompareTag("East"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    //temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            
            /*
            foreach (GameObject child in EmissionParticles)
            {
                Vector3 direction = (player.transform.position - child.transform.position).normalized;
                child.transform.position += direction * ParticleVelocity * Time.deltaTime;
            }
            */
        }
	}

    public void OnInput(float value)
    {
        if (EmissionParticles.Count > 0)
        {
            if (value == 1.0f)
            {
                if (EmissionParticles.Peek().CompareTag("North"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            else if (value == 2.0f)
            {
                if (EmissionParticles.Peek().CompareTag("South"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            else if (value == 3.0f)
            {
                if (EmissionParticles.Peek().CompareTag("West"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
            else if (value == 4.0f)
            {
                if (EmissionParticles.Peek().CompareTag("East"))
                {
                    GameObject temp = EmissionParticles.Dequeue();
                    temp.transform.parent = NullTransform;
                    temp.GetComponent<ParticleController>().DestroyByPlayer();
                }
            }
        }
    }
    public IEnumerator RegularEmit() {
        while (true)
        {
            yield return new WaitForSeconds(EmissionTime);
            //if()
            Random.InitState(System.Guid.NewGuid().GetHashCode());
            GameObject temp = Instantiate(PrefabsList[Random.Range(0, 3)], this.transform);
            float tempRadius = Mathf.Deg2Rad * Random.Range(-EmitterConeAngle, EmitterConeAngle);
            float tempAngle = Mathf.Deg2Rad * Random.Range(-EmitterConeAngle, EmitterConeAngle);
            temp.transform.position = new Vector3(EmitterRadius*Mathf.Sin(tempAngle)*Mathf.Cos(tempRadius), EmitterRadius*Mathf.Sin(tempAngle)*Mathf.Sin(tempRadius), EmitterRadius*Mathf.Cos(tempAngle));
            temp.transform.LookAt(player.transform.position);
            temp.GetComponent<ParticleController>().StartForward(player.transform.position);
            EmissionParticles.Enqueue(temp);
            yield return null;
        }
    }

    /*
    public IEnumerator ParticlesTransform() {
        while (true) {
            if(EmissionParticles.Count > 0) foreach (GameObject child in EmissionParticles) {
                Vector3 direction = (player.transform.position - child.transform.position).normalized;
                child.transform.position += direction * ParticleVelocity * Time.deltaTime;
            }
            yield return null;
        }
    }
    */

    public void ForceDeQueue(int value) {
        if(value == EmissionParticles.Peek().GetInstanceID()) EmissionParticles.Dequeue();
    }
}
