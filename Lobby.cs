using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour {
    public Animation anim;
    public GameObject[] Host;
    public GameObject Load;
    public float timer;
    public bool time;
    public bool proverkaname;
	public void Exit()
    {        
        Application.Quit();
    }
    public void CreateServer()
    {
        if(proverkaname == false)
        {
            ProverkaHost();
            Host[0].SetActive(true);
        }
     
    }
    public void ConectHost()
    {
        if(proverkaname == false)
        {
            ProverkaHost();
            Host[1].SetActive(true);
        }
        
    }
    public void Updat()
    {
        Load.SetActive(true);
        anim.CrossFade("load");
        proverkaname = true;
        time = true;
    }
    public void Update()
    {
        if(time)
        {
            timer += Time.deltaTime;
            if( timer >= 4f)
            {
                anim.Stop("load");
                Load.SetActive(false);
                timer = 0f;
                time = false;

            }
        }
    }
    public void ProverkaHost()
    {
        foreach (GameObject item in Host)
        {
            item.SetActive(false);
        }
    }
}
