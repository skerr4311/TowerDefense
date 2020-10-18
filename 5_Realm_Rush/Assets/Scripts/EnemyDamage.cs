using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] Collider collisionMesh;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;
    // Start is called before the first frame update
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        myAudioSource.PlayOneShot(enemyHitSFX);
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
    }
    private void KillEnemy()
    {
        var sfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        sfx.Play();
        float deathDuration = sfx.main.duration;
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        
        Destroy(sfx.gameObject, deathDuration);
        Destroy(gameObject);
    }
}