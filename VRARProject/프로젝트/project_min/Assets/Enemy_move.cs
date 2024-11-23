using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_move : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(target.position);
    }
}
