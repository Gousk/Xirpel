using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int healthPoint = 5;

    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;

    void Start() 
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();  
        AddRigidbody();

        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (healthPoint == 0)
        {
            KillEnemy();
        }
    }  

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        healthPoint--;
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
