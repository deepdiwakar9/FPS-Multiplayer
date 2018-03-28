using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    [SerializeField]
    PlayerWeapon weapon;

    [SerializeField]
    Camera cam;

    [SerializeField]
    LayerMask mask;

    private void Start()
    {
        if(cam == null)
        {
            Debug.Log("Give Reference to the camera");
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    [Client]
    void Shoot()
    {
        RaycastHit _Hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _Hit, weapon.range, mask))
        {
            CmdPlayerShoot(_Hit.collider.name, weapon.damage);
        }
    }

    [Command]
    void CmdPlayerShoot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot.");
        Player player = GameManager.GetPlayer(_playerID);

        player.RpcTakeDamage(_damage);
    }
}
