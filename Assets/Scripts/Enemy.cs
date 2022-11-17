using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform instantiateContainer;

    [SerializeField]
    int health = 2;

    [SerializeField]
    GameObject enemyExplosionVFX;

    [SerializeField]
    GameObject enemyHitVFX;

    [SerializeField]
    int pointsRewarded = 10;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        instantiateContainer = GameObject.FindWithTag("SpawnAtRuntime").transform;
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(enemyHitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = instantiateContainer;
        health--;

        if (health <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(pointsRewarded);
        GameObject vfx = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = instantiateContainer;        
        Destroy(gameObject);
    }
}
