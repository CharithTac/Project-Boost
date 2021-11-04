using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField]
    float loadLevelDelay = 2f;
    [SerializeField]
    AudioClip explosionSound;
    [SerializeField]
    AudioClip winSound;

    AudioSource myAudioSource;

    bool isTransitioning = false;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;
        switch (collision.gameObject.tag) {
            case "Fuel":
                Debug.Log("Hit the fuel");
                break;
            case "Friendly":
                Debug.Log("Hit a firendly object");
                break;
            case "Finish":
                GetComponent<Movement>().enabled = false;
                myAudioSource.Stop();
                myAudioSource.PlayOneShot(winSound);
                Invoke("LoadNextScene", loadLevelDelay);
                isTransitioning = true;
                break;
            default:
                GetComponent<Movement>().enabled = false;
                myAudioSource.Stop();
                myAudioSource.PlayOneShot(explosionSound);
                Invoke("StartCrashSequence", loadLevelDelay);
                isTransitioning = true;
                break;
        }
    }

    void StartCrashSequence() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Restart the level
    }

    void LoadNextScene() { 
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;//Start over
        }
        SceneManager.LoadScene(nextSceneIndex);//Load next level
    }
}
