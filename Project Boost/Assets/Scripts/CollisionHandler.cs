using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Fuel":
                Debug.Log("Hit the fuel");
                break;
            case "Friendly":
                Debug.Log("Hit a firendly object");
                break;
            case "Finish":
                LoadNextScene();
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Restart the level
                break;
        }
    }

    void LoadNextScene() { 
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;//Start over
        }
        SceneManager.LoadScene(nextSceneIndex);//Load next level
    }
}
