using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + " collided with "+ other.gameObject.name);
        RestartProcess();
    } 

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " triggered by "+ other.gameObject.name);
    } 

    void RestartProcess()
    {
        crashVFX.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerCtrl>().enabled = false;
        Invoke("RestartLevel", loadDelay);   
    } 

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }    
}
