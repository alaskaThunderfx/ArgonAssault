using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    float loadDelay = 1f;

    [SerializeField]
    GameObject deathParticles;

    [SerializeField]
    ParticleSystem petalSparkleParticles;

    [SerializeField]
    Collider coll;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Material deathMaterial;

    [SerializeField]
    Renderer rend;

    float cd = 1f;

    bool isDying = false;

    void Update()
    {
        MaterialLerp();
    }

    void MaterialLerp()
    {
        if (isDying)
        {
            float lerp = Mathf.PingPong(Time.time, 1);
            rend.material.Lerp(rend.material, deathMaterial, lerp);
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                rend.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerController>().enabled = false;
        deathParticles.SetActive(true);
        petalSparkleParticles.Stop();
        coll.enabled = false;
        isDying = true;
        rb.useGravity = true;

        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
