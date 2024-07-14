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
    private Transform rootTransform;
    private float timePerFrame;
    private float timer;

    private void Start()
    {
        sourceAnimator = calc_funcs.InitializeAnimator(sourceAnimatorObjectName, out timePerFrame, framesPerSecond);
        if (sourceAnimator != null)
        {
            rootTransform = sourceAnimator.transform;
        }
    }

    private void Update()
    {
        string rpcMethodName = "RPC_SetAnimatorStateFromByteArray";
        calc_funcs.UpdateAnimator(photonView, sourceAnimator, rootTransform, ref timer, timePerFrame, rpcMethodName);
    }

    [PunRPC]
    public void RPC_SetAnimatorStateFromByteArray(byte[] data)
    {
        if (!photonView.IsMine)
        {
            calc_funcs.SetAnimatorStateFromByteArray(sourceAnimator, rootTransform, data);
        }
    }
}
