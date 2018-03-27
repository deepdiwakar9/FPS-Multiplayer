using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour{

    [SerializeField]
    Behaviour[] ComponentsToDisable;

    Camera sceneCamera;

    
    private void Start()
    {
        
        if(!isLocalPlayer)
        {
            ComponentDisable();
            AssignLayerMask();
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
                
            }
            
        }

        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        gameObject.name = _ID;
    }

    void AssignLayerMask()
    {
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");      //9 is assigned to RemotePlayer
    }

    void ComponentDisable()
    {
        for (int i = 0; i < ComponentsToDisable.Length; i++)
        {
            ComponentsToDisable[i].enabled = false;
        }
    }
   
   

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

}
