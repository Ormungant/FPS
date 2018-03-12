using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerSinkPos : NetworkBehaviour
{
    [SyncVar]
    private Vector3 SinkPos;
    [SyncVar]
    private Quaternion SinkRot;
    public float lerpspeed = 60f;
    [SyncVar] float SinkAnimY;
    public Animator Anim;
    public CamControll Cm;

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, SinkPos, Time.deltaTime * lerpspeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, SinkRot, Time.deltaTime * lerpspeed);
        }

    }
    [Client]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdSendPosition(transform.position, transform.rotation,Cm.y);
        }
    }
    [Command]
    void CmdSendPosition(Vector3 pos, Quaternion rot,float ChartterY)
    {
        SinkPos = pos;
        SinkRot = rot;
        SinkAnimY = ChartterY;
    }

    private void FixedUpdate()
    {
        LerpPosition();
        TransmitPosition();
    }
}
