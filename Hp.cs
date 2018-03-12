using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Hp : NetworkBehaviour {
    [SyncVar]
    public int Helth;
    public bool dead;
    public Text Hp_currrent;
    public Slider Hp_currrent1;


    public void Damage(int damage)// принимвает урон и отнимает от хп
    {
        Helth -= damage;
    }
	void Die ()
    {

        GetComponent<playerSetup2>().Die();

        dead = true;
    }
    void Update()
    {
        if (isLocalPlayer)//отображение хп
        {
            Hp_currrent.text = Helth + "/100";
            Hp_currrent1.maxValue = 100;
            Hp_currrent1.value = Helth;
        }
        if(Helth<=0 && dead == false)
        {
            Die();
        }
    }
}
