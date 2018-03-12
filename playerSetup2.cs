using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class playerSetup2 : NetworkBehaviour
{
    public Transform client;
    public Transform me; 
    public GameObject[] Weapon;
    public GameObject[] Weapon_client;
    public Animator anim;
    public Camera DeadCamera;
    void Start()
    {
        DeadCamera.enabled = false;
        if (isLocalPlayer)
        {
            Screen.lockCursor = true;
            client.gameObject.SetActive(false);
            me.gameObject.SetActive(true);
            
        }
        else
        {       
            client.gameObject.SetActive(true);
            me.gameObject.SetActive(false);
            //GetComponent<CharacterController>().enabled = false;
        }
    }
    public void Die ()// метод смерти который выключает у пользователя все скрипты и т.д
    {
        if(isLocalPlayer)
        {
            DeadCamera.enabled = true;
        }
        anim.SetTrigger("die");
        client.gameObject.SetActive(true);
        me.gameObject.SetActive(false);
        GetComponent<CharacterController>().enabled = false;
        GetComponent<shoot>().enabled = false;
        GetComponent<PlayerSinkPos>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        
    }
    
}
