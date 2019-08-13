using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class TrollController : MonoBehaviour
{

    public static int TrollDie = 0;

    public ListController TreeDetector;
    public ListController PlayerDetector;
    public ListController CloseTreeDetector;

    private Animator animator;
    //private Rigidbody rigidBody;
    public GameObject target;
    private bool beingChased;
    public float feelOK = 40f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private Vector3 direction;
    private float turnCounter = 0.0f;
    private bool rightTend = true;
    private float stopCounter = 0.0f;
    private bool die = false;
    private TreeController targetTree;
    public TrollSpawner Parent;
    private NavMeshAgent agent;
    //private float initCounter = 3f;
    public bool nav = false;
    public float navTimeGap = 5f;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //rigidBody = this.GetComponent<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
        direction = this.transform.forward;
    }

    public void Attack()
    {
        targetTree.SendMessage("Hit", 10);
    }

    public void TreeMiss() {
        targetTree = null;
        animator.SetBool("FrontTree", false);
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            return;
        }
        if (PlayerDetector.CollisionObjects.Count > 0)
        {
            target = PlayerDetector.CollisionObjects[0].gameObject;
            beingChased = true;
            agent.enabled = true;
            animator.SetBool("FrontTree", false);
            if (targetTree != null)
            {
                targetTree.TrollList.Remove(this);
                targetTree = null;
            }
        }
        if (!beingChased)
        {
            if (CloseTreeDetector.CollisionObjects.Count > 0 && targetTree == null)
            {
                target = CloseTreeDetector.CollisionObjects[0].gameObject;
                animator.SetBool("FrontTree", true);
                this.transform.LookAt(target.transform.position);
                agent.enabled = false;
                targetTree = target.GetComponent<TreeController>();
                targetTree.TrollList.Add(this);
            }
            else if (TreeDetector.CollisionObjects.Count > 0 && target == null)
            {
                target = TreeDetector.CollisionObjects[Random.Range(0, TreeDetector.CollisionObjects.Count)].gameObject;
                agent.enabled = true;
            }
            else if (target != null)
            {
                Run();
            }
            else
            {
                WalkAround();
            }
        }
        else
        {
            if (Vector3.Distance(target.transform.position, this.transform.position) > feelOK)
            {
                beingChased = false;
                target = null;
            }
            else Flee();
        }
    }

    void Flee()
    {

        /*
        if (Random.value < 0.2 && turnCounter <= 0.0f)
        {
            rightTend = !rightTend;
            turnCounter = 5.0f;
        }
        if (turnCounter > 0.0f) turnCounter -= Time.deltaTime;
        Vector3 directionTo = (rightTend ? this.transform.right : -this.transform.right) - (target.transform.position - this.transform.position);
        directionTo.y = 0;
        directionTo = directionTo.normalized;
        rigidBody.velocity = directionTo * runSpeed + new Vector3(0, rigidBody.velocity.y, 0);
        this.transform.LookAt(this.transform.position + directionTo);
        direction = this.transform.forward;
        */

    }

    void Run()
    {
        /*
        Vector3 directionTo = target.transform.position - this.transform.position;
        directionTo.y = 0;
        directionTo = directionTo.normalized;
        rigidBody.velocity = directionTo * runSpeed + new Vector3(0, rigidBody.velocity.y, 0);
        this.transform.LookAt(this.transform.position + directionTo);
        direction = this.transform.forward;
        */
    }

    void WalkAround()
    {
        /*
        if (Random.value >= 0.2 || turnCounter > 0.0f)
        {
            rigidBody.velocity = direction.normalized * walkSpeed;
            if (turnCounter > 0.0f) turnCounter -= Time.deltaTime;
        }
        else
        {
            rigidBody.MoveRotation(Quaternion.Euler(new Vector3(0, this.transform.eulerAngles.y + (Random.value * 15), 0)));
            direction = this.transform.forward;
            rigidBody.velocity = direction.normalized * walkSpeed;
            this.transform.LookAt(this.transform.position + direction);
            turnCounter = 5.0f;
        }
        */
    }

    void Die()
    {
        animator.SetTrigger("Die");
        die = true;
        agent.enabled = false;
        TrollDie += 1;
        this.transform.DOMoveY(-0.8f, 0.8f).SetRelative(true).SetDelay(0.3f).OnComplete(() =>
        {
            this.transform.DOMoveY(-4f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                this.GetComponents<Collider>()[0].enabled = false;
                GameObject.Destroy(this.gameObject);
            });
        });
    }

    void RunOut()
    {
        GameObject.Destroy(this.gameObject);
        Parent.SendMessage("TrollFlee");
    }

    /*
    void Stop(float value)
    {
        
    }
    */

    void Trigger()
    {
        Die();
    }
}
