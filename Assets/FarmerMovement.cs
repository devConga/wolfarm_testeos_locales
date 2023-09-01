using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FarmerMovement : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Vector3 lastKnowLocation;

    public int tiempoDePersecucion;
    [SerializeField] public Transform[] waypoints;
    private int waypointsIndex;
    private int random;

    public int chanceVolverUnoEntre;
    private bool persiguiendo;
    public float chaseTimer=0;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastKnowLocation = GetComponent<Transform>().position;
        waypointsIndex = 0;
    }

    void MoverAlSiguienteWP(){
        if(waypointsIndex < waypoints.Length){
            agent.SetDestination(waypoints[waypointsIndex].position);
            waypointsIndex++;
        }
        else{
            waypointsIndex = 0;
        }
    }

    void MoverAlAnteriorWP(){
        waypointsIndex-=2;
        if(waypointsIndex==-1){
            waypointsIndex = waypoints.Length;
            }
        if(waypointsIndex == -2){
            waypointsIndex = waypoints.Length -1;
            }

        agent.SetDestination(waypoints[waypointsIndex].position);
        waypointsIndex++;
    
    }


    void Perseguir(){
        if(GetComponent<FieldOfView>().canSeePlayer){
            lastKnowLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        agent.SetDestination(lastKnowLocation);
    }

    // Update is called once per frame
    void Update()
    {

        if(GetComponent<FieldOfView>().canSeePlayer){
            persiguiendo = true;
            chaseTimer = (float)tiempoDePersecucion;
        }
        else{
            if(chaseTimer > 0){
                chaseTimer-=Time.deltaTime;
            }

            if(chaseTimer <= 0){
                persiguiendo = false;
            }
        }

        if(persiguiendo){
            Perseguir();
        }

        if(!persiguiendo){
        if(!agent.pathPending && agent.remainingDistance < 0.1f){
            random = Random.Range(1, chanceVolverUnoEntre+1);
            Debug.Log(random);
            if(random!=1){
                MoverAlSiguienteWP();
            }
            else{
                MoverAlAnteriorWP();
                /* Debug.Log("Volvere"); */
            }
        }
        }
    }
}
