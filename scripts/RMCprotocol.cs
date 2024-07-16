using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using RPC.MoCap;
using System;

public class RMCprotocol : MonoBehaviourPun
{
    public float framesPerSecond = 30f;
    public Animator sourceAnimator; 
    public Transform rootTransform;
    private float timePerFrame;
    private float timer;

    private void Start()
    {
        if (sourceAnimator == null)
        {
            Debug.LogError("Source Animator is not set in the Inspector.");
            return;
        }

        timePerFrame = 1f / framesPerSecond;
       
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
