using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class shoot : NetworkBehaviour {

    public Transform cameraA;
    public Transform cameraB;
    public Transform shotpoint;
    public Transform MuzzelFlaslPref;
    public Transform FPSMuzzelFlaslPref;
    public Transform ClientMuzzelFlaslPref;
    public Animation anim;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip ReloadShoot;
    public GameObject CrossHair;
    public GameObject recoil;
    public Transform shootpoint;
    public GameObject HitParticals;
    public GameObject bulletImpact;
    public Text[] current_bullets;
    public Slider current_bullets1;
    public int bulletmagaz = 30;
    public int curentbellets;
    public int bullitleft = 200;
    float firetimer;
    public float range = 10000f;
    public float firerate = 0.1f;
    public Animator anim1;
    private bool IsReload;
    public bool aim = false;
    public bool timetproverka = false;
    public float timer;
    public float returnSpeed = 5f;
    void Start()
    {
        curentbellets = bulletmagaz;
    }
    private void FixedUpdate()
    {
        if (!Input.GetButton("Fire1"))//если отпустил кнопку мыши то стрельба прикрошается 
        {
            if (aim == false)
            {
                anim1.SetBool("fire", false);
            }
            
        }
        AnimatorStateInfo info = anim1.GetCurrentAnimatorStateInfo(0);
        IsReload = info.IsName("Reload");
    }
    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);//запуск музыки стрельбы
    }
    private void PlayReload()
    {
        audioSource.PlayOneShot(ReloadShoot);//запуск музыки перезарядки
    }
    private void DoReload()// метод перезарядки
    {
        aim = false;
        AnimatorStateInfo info = anim1.GetCurrentAnimatorStateInfo(0);

        if (IsReload) return;

        PlayReload();
        anim1.CrossFadeInFixedTime("Reload", 0.1f);
        Reload();   
    }
    private void Reload()// метод перезарядки
    {
        if (bullitleft <= 0) return;

        int bulletsToload = bulletmagaz - curentbellets;
        int bulletstodeduct = (bullitleft >= bulletsToload) ? bulletsToload : bullitleft;

        bullitleft -= bulletstodeduct;
        curentbellets += bulletstodeduct;
    }
    private void Fire()// метод стрельбы
    {
        timetproverka = true;
        if (firetimer < firerate || curentbellets <= 0 || IsReload) return;
        firetimer = 0f;
        PlayShootSound();
        if (aim == false)
        {
            anim1.SetBool("fire", true);
        }
        curentbellets--;
        
        if(aim == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraA.transform.position, cameraA.transform.forward, out hit, 1000))//стрельба
            {
                if (hit.transform.CompareTag("Player"))//попадание если пользователь
                {
                    GameObject hitParticlEffec = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    Destroy(hitParticlEffec, 1f);
                    CmdShoot(hit.transform.root.GetComponent<NetworkIdentity>().netId);

                }
                else if (!hit.transform.CompareTag("Player"))//попадание если не пользователь
                {
                    GameObject hitParticlEffect = Instantiate(HitParticals, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                    Destroy(hitParticlEffect, 3f);
                }
            }
        }
        if (aim == true)// тоже самое только если включен прицел
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraB.transform.position, cameraB.transform.forward, out hit, 1000))
            {

                if (hit.transform.CompareTag("Player"))
                {
                    GameObject hitParticlEffec = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    Destroy(hitParticlEffec, 1f);
                    CmdShoot(hit.transform.root.GetComponent<NetworkIdentity>().netId);

                }
                else if (!hit.transform.CompareTag("Player"))
                {
                    GameObject hitParticlEffect = Instantiate(HitParticals, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                    Destroy(hitParticlEffect, 3f);
                }
            }
        }

        Transform tr = Instantiate(MuzzelFlaslPref, FPSMuzzelFlaslPref.position, FPSMuzzelFlaslPref.rotation) as Transform;
        tr.parent = transform;
    }
    public void Update () {

		if(!isLocalPlayer)
        {
           
            return;
        }
        if(isLocalPlayer)// отображения кол-во потронов
        {
            current_bullets[0].text = curentbellets + "/" + bullitleft;
            current_bullets1.maxValue = 30;
            current_bullets1.value = curentbellets;
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (curentbellets > 0){
                Fire();
            }
            else if (bullitleft > 0 && curentbellets == 0 ) {
                DoReload();
            }
               
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))//прицел
        {
            if (aim == false)
            {
                anim1.SetBool("aim", true);
                aim = true;
            }
            else if (aim == true)
            {
                anim1.SetBool("aim", false);
                aim = false;
            }
        }
        if (timetproverka == true)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timetproverka = false;
                timer = 0;
            }
        }
        if (timetproverka == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (aim == false)
                {
                    anim1.SetBool("fire", false);
                    anim1.SetBool("aim", true);
                    aim = true;
                }
                else if (aim == true)
                {
                    anim1.SetBool("fire", true);
                    anim1.SetBool("aim", false);
                    aim = false;
                }
            }
        }    
        if (Input.GetKeyDown(KeyCode.R))// вызов перезарядки
        {
            aim = false;
            if (curentbellets < bulletmagaz && bulletmagaz > 0 && bullitleft > 0)
            {
                DoReload();
            } 
        }
        if(firetimer<firerate)
        {
            firetimer += Time.deltaTime;
        }  
    }
    [Command]
    void CmdShoot(NetworkInstanceId id)//отнятия хп у пользователя в которго папали
    {
        NetworkServer.FindLocalObject(id).GetComponent<Hp>().Damage(5);
    }
}
