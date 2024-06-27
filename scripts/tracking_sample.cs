using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using RMC_protocol.parameters;
using RMC_protocol.RMC_function;

public class tracking_sample : MonoBehaviourPun
{
    public string sourceAnimatorObjectName = "target"; // Avatar name
    public float framesPerSecond = 30f; // Frame per second for transferring
    private Animator sourceAnimator; // Animator attached to the avatar
    private float timePerFrame; // frame time
    private float timer; // for timer interrupt

    private RMC_param rmcParam = new RMC_param();
    private functions rmcFunctions = new functions();

    private void Start()
    {
        GameObject sourceObject = GameObject.Find(sourceAnimatorObjectName); // Find the GameObject
        if (sourceObject != null)
        {
            sourceAnimator = sourceObject.GetComponent<Animator>(); // set the source animator
        }
        else
        {
            Debug.LogError("Source Animator Object not found."); // Debug code
        }

        timePerFrame = 1f / framesPerSecond;
    }

    private void Update()
    {
        if (photonView.IsMine) // when the avatar is controlled by the user
        {
            timer += Time.deltaTime;
            if (timer >= timePerFrame)
            {
                timer -= timePerFrame;
                if (sourceAnimator != null)
                {
                    // Call the remote procedure to synchronize remote avatar motion
                    photonView.RPC("SetBoneRotationsFromByteArray", RpcTarget.All, rmcFunctions.GetBoneRotationsAsByteArray(sourceAnimator));
                }
            }
        }
    }

    [PunRPC]
    public void SetBoneRotationsFromByteArray(byte[] rotationsByteArray)
    {
        rmcFunctions.SetBoneRotationsFromByteArray(rotationsByteArray, sourceAnimator, photonView.IsMine);
    }
}
