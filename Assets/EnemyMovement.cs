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


    void Update()
    {
        // ... set the destination of the nav mesh agent to the player.
        nav.SetDestination(player.position);
        anim.SetTrigger("MoveToPlayer");
        
    }
}