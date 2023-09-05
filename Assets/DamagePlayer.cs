using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class DamagePlayer : MonoBehaviour
{

    private NavMeshAgent agent;

    public float timeInRangeToAttack = 2.5f;

    public float attackCountdown;

    public GameObject player;

    public float attackRange = 7.5f;

    public bool onRange = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackCountdown = timeInRangeToAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<FieldOfView>().canSeePlayer){

            if(!agent.pathPending && agent.remainingDistance <= attackRange){
                onRange = true;
            }
        }

            if(onRange){
                attackCountdown -= Time.deltaTime;
                agent.speed = 0;
                }

            if(attackCountdown <= 0 && !agent.pathPending && agent.remainingDistance <= attackRange){
                    Destroy(player);
                }

            if(attackCountdown <= 0 && !agent.pathPending && agent.remainingDistance > attackRange){
                attackCountdown = timeInRangeToAttack;
                onRange = false;
                agent.speed = 6;
            }
        
    }
}
