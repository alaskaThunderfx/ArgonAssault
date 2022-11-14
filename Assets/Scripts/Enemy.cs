using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform parent;
    [SerializeField]
    GameObject enemyExplosion;

    void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
