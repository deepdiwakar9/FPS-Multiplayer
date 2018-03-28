using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
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

        GetComponent<Player>().Setup();

        //string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        //gameObject.name = _ID;
    }

    //Register Player
    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
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
        GameManager.UnRegisterPlayer(transform.name);
    }

}
