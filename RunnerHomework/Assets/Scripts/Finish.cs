using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        PlayerController controller = other.GetComponentInParent<PlayerController>();
        if(controller)
        {
            // If player finishes level camera is changing to finish cam with this _isFinished bool variable.
            controller._isFinished = true; 
            controller.FinishMove();
            UIManager.manager.ShowNextLevelPanel();
            GameManager.manager.ToFinishGame();
        }    
    }
}
