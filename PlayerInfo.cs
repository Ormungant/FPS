using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerInfo : NetworkBehaviour
{
    [SyncVar] public int Hp = 100;
    private void Start()
    {
        transform.name = "Player " + GetComponent<NetworkIdentity>().netId.ToString(); 
    }
    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.Label(new Rect(Screen.width - 200, 50, 300, 100),"Health " + Hp);
        }
    }
    void Update()
    {
        if(Hp <=0)
        {
            GetComponent<PlayerSetup>().DidavlePlayer();
        }
    }
}
