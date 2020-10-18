using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem goalParticle;
    [SerializeField] float movementSpeed = 1;

    Vector3 movementTarget;

    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<WayPoint> path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path)
    {
        foreach (WayPoint waypoint in path)
        {
            movementTarget = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }

        //finished, goal reached
        SelfDestruct();
        //Play Goal Particles
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        //move towards next waypoint smoothly, at constant speed
        float step = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, movementTarget, step);
    }

    private void SelfDestruct()
    {
        var sfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        sfx.Play();
        float deathDuration = sfx.main.duration;

        Destroy(sfx.gameObject, deathDuration);
        Destroy(gameObject);
    }
}
