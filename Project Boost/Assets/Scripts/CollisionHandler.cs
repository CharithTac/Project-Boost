using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("Completed the level");
                break;
            default:
                Debug.Log("Game over");
                break;
        }
    }
}
