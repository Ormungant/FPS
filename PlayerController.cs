using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public float MoveSpeed = 12f;
    [SyncVar]
    public float horizontal;
    [SyncVar]
    public float vertical;
    public float gravity = -9.8f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            controller.enabled = true;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else//отображает анимация по координатам пользователя
        {
            float lerpanimV = Mathf.Lerp(anim.GetFloat("ForwardSpeed"), vertical, Time.deltaTime * 10);
            anim.SetFloat("ForwardSpeed", vertical);
            float lerpanimР = Mathf.Lerp(anim.GetFloat("StarteSpeed"), horizontal, Time.deltaTime * 10);
            anim.SetFloat("StarteSpeed", vertical);
        }
        TransmitAnimationData();
    }
    [Client]
    void TransmitAnimationData()//отпровляет координаты пользователя на сервер
    {
        if (isLocalPlayer)
        {
            CmdSybsSpeed(horizontal, vertical);
        }
    }
    [Command]
    void CmdSybsSpeed (float x,float y)// принимает координаты пользователя
    {
        horizontal = x;
        vertical = y;
    }
}
