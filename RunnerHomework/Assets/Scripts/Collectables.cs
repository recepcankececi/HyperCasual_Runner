using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void Update() 
    {
        // A local rotation for coins in the game.
        transform.rotation *= Quaternion.Euler(1, 0, 0);
    }
    private void OnTriggerEnter(Collider other) 
    {
        PlayerController controller = other.GetComponentInParent<PlayerController>();
        if(controller)
        {
            // Everytime time player hit trigger gold increasing method call activates.
            controller.GoldIncrease();
            Destroy(gameObject);
        }
    }
}
