using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    //-////////////////////////////////////////////////////
    ///
    /// Patrol takes a GameObject and makes such object to patrol specified locations at the given speed
    ///
    public float moveSpeed; //Patrol speed

    [Header("Agent's patrol areas")]
    public List<Transform> patrolLocations; //List of all the Transform locations the gameObject will patrol

    [Space, Header("Agent")]
    public GameObject patrollingGameObject; //Unity GameObject that patrols
    private int nextPatrolLocation; //Keeps track of the patrol location

    [Space, Header("AI")]
    private GameObject player;
    public float maxDistance = 5f;
    private float distance;
    public float chaseSpeed = 1f;
    Vector3 playerPos;
    Vector3 patrolPos;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector2.Distance((Vector2)player.transform.position, (Vector2)patrollingGameObject.transform.position);

    }

    //-////////////////////////////////////////////////////
    ///
    /// Update is called once per frame
    ///
    void Update () 
    {
        playerPos = player.transform.position;
        patrolPos = patrollingGameObject.transform.position;
        distance = Vector2.Distance((Vector2)player.transform.position, (Vector2)patrollingGameObject.transform.position);
        if (inRange())
        {
            patrolPos.x -= (Time.deltaTime * chaseSpeed * patrollingGameObject.transform.localScale.x);
            patrollingGameObject.transform.position = patrolPos;
        }
        else
        {
            PatrolArea();
        }
        
    }

    //-////////////////////////////////////////////////////
    ///
    /// Moves the patrollingGameObject towards patrol location,
    /// when reach destination switch to next patrol position in the list
    ///
    private void PatrolArea()
    {
        Flip();
        patrollingGameObject.transform.position = Vector2.MoveTowards(patrollingGameObject.transform.position,
            patrolLocations[nextPatrolLocation].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(patrollingGameObject.transform.position, patrolLocations[nextPatrolLocation].position) <= 2)
        {
            nextPatrolLocation = (nextPatrolLocation + 1) % patrolLocations.Count; //Prevents IndexOutofBound by looping back through list
        }
    }

    //-////////////////////////////////////////////////////
    /// 
    /// tests if the player is in range
    /// if the distance is less than the max distance, if there is a direct line of sight, if the direction is right, etc
    /// <returns></returns>
    private bool inRange()
    {
        float direction = patrollingGameObject.transform.localScale.x;
        distance = Vector2.Distance((Vector2)player.transform.position, (Vector2)patrollingGameObject.transform.position);
        if (distance > maxDistance)
        {
            return false;
        }
        if ((playerPos.x > patrolPos.x && direction == 1) || (playerPos.x < patrolPos.x && direction == -1))
        {
            
            return false;
        }
        Vector2 dir = (Vector2)playerPos - (Vector2)patrolPos;
        RaycastHit2D hit = Physics2D.Raycast(patrolPos, dir);
        if (hit.collider.gameObject != player)
        {
            return false;
        }
        return true;
    }

    //-////////////////////////////////////////////////////
    ///
    /// Makes the patrollingGameObject always be facing the next patrol location
    ///
    private void Flip()
    {
        Vector2 localScale = patrollingGameObject.transform.localScale;
        if (patrollingGameObject.transform.position.x - patrolLocations[nextPatrolLocation].position.x > 0)
            localScale.x = 1;
        else
            localScale.x = -1;
        patrollingGameObject.transform.localScale = localScale;
    }
}
