using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State  {Searching, Patrol};
    private NavMeshAgent agent;
    public List<GameObject>PatrolTargets;
    public State state = State.Patrol;
    public float endDistance = 0.01f;
    private int currentPostionInPath = 0;
    public float radius = 5f;
    public float angle = 45f;
    public float searchTime = 10;
    private float currentTime = 0;
    public GameObject player;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                if(agent.destination==null || agent.remainingDistance<=endDistance)
                {
                    Debug.Log($"Set dist {PatrolTargets[(currentPostionInPath + 1) % PatrolTargets.Count].name}:{agent.remainingDistance}");
                    SetDestination(PatrolTargets[(currentPostionInPath + 1) % PatrolTargets.Count].transform.position);
                    currentPostionInPath++;
                }
                if (isViewPort()) state = State.Searching; 
                break;
            case State.Searching:
                SetDestination(player.transform.position);
                currentTime += Time.deltaTime;
                if (currentTime >= searchTime)
                {
                    currentTime = 0;
                    state = State.Patrol;
                }
                break;
            default:
                break;
        }
        
    }
    private bool isViewPort()
    {
        float realAngle = Vector3.Angle(-transform.right, player.transform.position-transform.position);
        if(realAngle<angle && (player.transform.position-transform.position).magnitude<radius) return true;
        Debug.Log($"{realAngle},{(player.transform.position-transform.position).magnitude}");
        return false;
    }
    private void SetDestination(Vector3 destination)
    {
        if (destination.x > transform.position.x)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        agent.SetDestination(destination);
     }
}
