using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerSetup : NetworkBehaviour {

    public Camera CharterCamera;
    public AudioListener CharterAudio;
    private bool Can =false;
    private float timer;
    public float respawntimer = 3f;
    void Start() {

        EnablePlayer();

    }
    void EnablePlayer()
    {
        if (isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            CharterCamera.enabled = true;
            CharterAudio.enabled = true;
        }

        Can = false;
        //GetComponent<PlayerInfo>().Hp = 100;
        transform.position = new Vector3(0,0, 0);
    }
    public void DidavlePlayer()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        CharterCamera.enabled = false;
        CharterAudio.enabled = false;
        Can = true;
    }
    void Update()
    {
        if (Can == true)
        {
            timer += Time.deltaTime;
            if (timer >respawntimer)
            {
                EnablePlayer();
                timer = 0;
            }
        }

    }

}
