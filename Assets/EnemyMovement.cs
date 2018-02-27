using UnityEngine;
using UnityEngine.AI;    //important!!!
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;                  // Reference to the animator component.


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

        // ... set the destination of the nav mesh agent to the player.
        nav.SetDestination( findAttackPoint() );
        anim.SetTrigger("MoveToPlayer");

    }

    void Update()
    {

        
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "NPC")
        {
            // ... too close to an ally.
            Debug.Log("NPC distance adjust ??");

        }
    }
    void OnTriggerExit(Collider other)
    {
        //Any trigger exit reset to follow player
        //if (other.gameObject.tag == "NPC")
        {
           //reset nav to the player
            nav.SetDestination(findAttackPoint());
            anim.SetTrigger("MoveToPlayer");
            Debug.Log("Nav reset to player");
        }
    }

    Vector3 findAttackPoint()
    {

        Vector3 offset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        return player.position + offset;
    }
}