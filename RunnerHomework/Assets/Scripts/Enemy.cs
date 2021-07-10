using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        PlayerController controller = other.GetComponentInParent<PlayerController>();
        if(controller)
        {
            // If player hit enemy or obstacle we change state to finish and show retry UI panel.
            GameManager.manager.ToFinishGame();
            UIManager.manager.RetryMethod();
            controller.DefeatMove();
        }
    }
}
