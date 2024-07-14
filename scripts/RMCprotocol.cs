using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using RPC.MoCap;
using System;

public class RMCprotocol : MonoBehaviourPun
{
    public string sourceAnimatorObjectName = "target";
    public float framesPerSecond = 30f;
    private Animator sourceAnimator;
    private float timePerFrame;
    private float timer;

    private void Start()
    {
        sourceAnimator = calc_funcs.InitializeAnimator(sourceAnimatorObjectName, out timePerFrame, framesPerSecond);
    }

    private void Update()
    {
        string rpcMethodName = "RPC_SetBoneRotationsFromByteArray";
        calc_funcs.UpdateAnimator(photonView, sourceAnimator, ref timer, timePerFrame, rpcMethodName);
    }

    [PunRPC]
    public void RPC_SetBoneRotationsFromByteArray(byte[] rotationsByteArray)
    {
        if (!photonView.IsMine)
        {
            calc_funcs.SetBoneRotationsFromByteArray(sourceAnimator, rotationsByteArray);
        }
    }
}
