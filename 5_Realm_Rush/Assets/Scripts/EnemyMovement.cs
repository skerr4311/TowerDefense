using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem goalParticle;
    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<WayPoint> path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path)
    {
        foreach (WayPoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
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
