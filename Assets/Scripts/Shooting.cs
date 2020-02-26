using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour
{
    public GameObject bulletPrefabs;
    public GameObject bulletPoint;
    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown("space"))
        {
            CmdSpawnBullet();
        }
        
    }
    [Command]
    void CmdSpawnBullet()
    {
        CreateBullet();

        //Call to back client
        RpcCreateBullet();
    }
    //Server Call Client
    [ClientRpc]
    void RpcCreateBullet()
    {
        if (!isServer)
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefabs, bulletPoint.transform.position, bulletPoint.transform.rotation);
        // AddForce refer to add power in bullet transfrom forward 
        //bullet.GetComponent<Rigidbody>().AddForce(bulletPoint.transform.forward * 2000);
        bullet.GetComponent<Rigidbody>().velocity = bulletPoint.transform.forward * 50;
        Destroy(bullet, 2.0f);
    }
}
