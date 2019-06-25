using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UIController UICont;

    [SerializeField] string objName;

    [SerializeField] PlayerController player;

    bool alreadyLookingAtObject = false;

    public void ShowObjectDetails()
    {
        if (PlayerLookingAtObject() == true)
        {
            if (alreadyLookingAtObject == false)
            {
                alreadyLookingAtObject = true;
                Debug.Log(objName);
            }
        }
        else
        {
            alreadyLookingAtObject = false;
        }
    }

    public bool PlayerLookingAtObject()
    {
        RaycastHit hit;
        //Debug.DrawRay(player.playerPos, player.forwardRay, Color.red);

        if (Physics.Raycast(player.playerPos, player.forwardRay, out hit, 2.0f))
        {
            if (hit.collider.isTrigger)
            {
                return true;
            }
        }
         return false;
    }


}
