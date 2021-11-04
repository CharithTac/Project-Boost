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
    [SerializeField]
    ParticleSystem explosionParticles;
    [SerializeField]
    ParticleSystem winParticles;

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
                winParticles.Play();
                LoadLevel("LoadNextScene", winSound);
                break;
            default:
                explosionParticles.Play();
                LoadLevel("StartCrashSequence", explosionSound);
                break;
        }
    }

    void LoadLevel(string loadLevelMethod, AudioClip audio) {
        GetComponent<Movement>().enabled = false;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(audio);
        Invoke(loadLevelMethod, loadLevelDelay);
        isTransitioning = true;
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
