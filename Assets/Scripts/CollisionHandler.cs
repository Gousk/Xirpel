using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

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
        GetComponent<PlayerCtrl>().enabled = false;
        Invoke("RestartLevel", loadDelay);   
    } 

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }    
}
