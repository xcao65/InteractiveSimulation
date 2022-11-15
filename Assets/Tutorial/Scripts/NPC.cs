using UnityEngine;

// navmesh moved to UnityEngine.AI in 2017
#if !UNITY_5
using UnityEngine.AI;
#endif


public class NPC : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo( Vector3 pos )
    {
        agent.destination = pos;
    }
}
