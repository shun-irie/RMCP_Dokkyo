using System;
using UnityEngine;
using Photon.Pun;

namespace RPC
{
    namespace MoCap
    {
        public class calc_funcs
        {
            public static byte[] QuaternionToByteArray(Quaternion rotation)
            {
                byte[] byteArray = new byte[16];
                Buffer.BlockCopy(new float[] { rotation.x, rotation.y, rotation.z, rotation.w }, 0, byteArray, 0, 16);
                return byteArray;
            }

            public static Quaternion ByteArrayToQuaternion(byte[] byteArray, int offset)
            {
                float[] floatArray = new float[4];
                Buffer.BlockCopy(byteArray, offset, floatArray, 0, 16);
                return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
            }

            public static byte[] Vector3ToByteArray(Vector3 vector)
            {
                byte[] byteArray = new byte[12];
                Buffer.BlockCopy(new float[] { vector.x, vector.y, vector.z }, 0, byteArray, 0, 12);
                return byteArray;
            }

            public static Vector3 ByteArrayToVector3(byte[] byteArray, int offset)
            {
                float[] floatArray = new float[3];
                Buffer.BlockCopy(byteArray, offset, floatArray, 0, 12);
                return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
            }

            public static string[] bones = {
                "Hips",
                "Spine",
                "Chest",
                "Neck",
                "Head",
                "LeftShoulder",
                "LeftUpperArm",
                "LeftLowerArm",
                "LeftHand",
                "RightShoulder",
                "RightUpperArm",
                "RightLowerArm",
                "RightHand",
                "LeftUpperLeg",
                "LeftLowerLeg",
                "LeftFoot",
                "LeftToes",
                "RightUpperLeg",
                "RightLowerLeg",
                "RightFoot",
                "RightToes"
            };

            public static HumanBodyBones ConvertStringToHumanBodyBone(string boneName)
            {
                switch (boneName)
                {
                    case "Hips": return HumanBodyBones.Hips;
                    case "Spine": return HumanBodyBones.Spine;
                    case "Chest": return HumanBodyBones.Chest;
                    case "Neck": return HumanBodyBones.Neck;
                    case "Head": return HumanBodyBones.Head;
                    case "LeftShoulder": return HumanBodyBones.LeftShoulder;
                    case "LeftUpperArm": return HumanBodyBones.LeftUpperArm;
                    case "LeftLowerArm": return HumanBodyBones.LeftLowerArm;
                    case "LeftHand": return HumanBodyBones.LeftHand;
                    case "RightShoulder": return HumanBodyBones.RightShoulder;
                    case "RightUpperArm": return HumanBodyBones.RightUpperArm;
                    case "RightLowerArm": return HumanBodyBones.RightLowerArm;
                    case "RightHand": return HumanBodyBones.RightHand;
                    case "LeftUpperLeg": return HumanBodyBones.LeftUpperLeg;
                    case "LeftLowerLeg": return HumanBodyBones.LeftLowerLeg;
                    case "LeftFoot": return HumanBodyBones.LeftFoot;
                    case "LeftToes": return HumanBodyBones.LeftToes;
                    case "RightUpperLeg": return HumanBodyBones.RightUpperLeg;
                    case "RightLowerLeg": return HumanBodyBones.RightLowerLeg;
                    case "RightFoot": return HumanBodyBones.RightFoot;
                    case "RightToes": return HumanBodyBones.RightToes;
                    default: return HumanBodyBones.LastBone; // ñ≥å¯Ç»èÍçáÇÃëŒèà
                }
            }

            public static byte[] GetBoneRotationsAsByteArray(Animator animator)
            {
                if (!animator) return null;

                byte[] rotationsByteArray = new byte[(bones.Length * 16) + 12];
                Buffer.BlockCopy(Vector3ToByteArray(animator.transform.position), 0, rotationsByteArray, 0, 12);

                for (int i = 0; i < bones.Length; i++)
                {
                    HumanBodyBones bone = ConvertStringToHumanBodyBone(bones[i]);
                    if (bone != HumanBodyBones.LastBone)
                    {
                        Transform boneTransform = animator.GetBoneTransform(bone);
                        if (boneTransform)
                        {
                            Buffer.BlockCopy(QuaternionToByteArray(boneTransform.localRotation), 0, rotationsByteArray, i * 16 + 12, 16);
                        }
                        else
                        {
                            Buffer.BlockCopy(new byte[16], 0, rotationsByteArray, i * 16 + 12, 16);
                        }
                    }
                }

                return rotationsByteArray;
            }

            public static void SetBoneRotationsFromByteArray(Animator animator, byte[] rotationsByteArray)
            {
                if (!animator) return;

                animator.transform.position = ByteArrayToVector3(rotationsByteArray, 0);

                for (int i = 0; i < bones.Length; i++)
                {
                    HumanBodyBones bone = ConvertStringToHumanBodyBone(bones[i]);
                    if (bone != HumanBodyBones.LastBone)
                    {
                        Transform boneTransform = animator.GetBoneTransform(bone);
                        if (boneTransform)
                        {
                            boneTransform.localRotation = ByteArrayToQuaternion(rotationsByteArray, i * 16 + 12);
                        }
                    }
                }
            }

            public static Animator InitializeAnimator(string objectName, out float timePerFrame, float framesPerSecond)
            {
                GameObject sourceObject = GameObject.Find(objectName);
                if (sourceObject != null)
                {
                    timePerFrame = 1f / framesPerSecond;
                    return sourceObject.GetComponent<Animator>();
                }
                else
                {
                    Debug.LogError("Source Animator Object not found.");
                    timePerFrame = 0f;
                    return null;
                }
            }

            public static void UpdateAnimator(PhotonView photonView, Animator sourceAnimator, Transform root, ref float timer, float timePerFrame, string rpcMethodName)
            {
                if (photonView.IsMine)
                {
                    timer += Time.deltaTime;
                    if (timer >= timePerFrame)
                    {
                        timer -= timePerFrame;
                        if (sourceAnimator != null)
                        {
                            byte[] boneRotations = GetBoneRotationsAsByteArray(sourceAnimator);
                            byte[] rootPosition = Vector3ToByteArray(root.position);
                            byte[] rootRotation = QuaternionToByteArray(root.rotation);

                            byte[] combinedData = new byte[boneRotations.Length + rootPosition.Length + rootRotation.Length];
                            Buffer.BlockCopy(boneRotations, 0, combinedData, 0, boneRotations.Length);
                            Buffer.BlockCopy(rootPosition, 0, combinedData, boneRotations.Length, rootPosition.Length);
                            Buffer.BlockCopy(rootRotation, 0, combinedData, boneRotations.Length + rootPosition.Length, rootRotation.Length);

                            photonView.RPC(rpcMethodName, RpcTarget.All, combinedData);
                        }
                    }
                }
            }

            public static void SetAnimatorStateFromByteArray(Animator animator, Transform root, byte[] data)
            {
                if (!animator || !root) return;

                int boneDataLength = (bones.Length * 16) + 12;
                byte[] boneRotations = new byte[boneDataLength];
                byte[] rootPosition = new byte[12];
                byte[] rootRotation = new byte[16];

                Buffer.BlockCopy(data, 0, boneRotations, 0, boneDataLength);
                Buffer.BlockCopy(data, boneDataLength, rootPosition, 0, 12);
                Buffer.BlockCopy(data, boneDataLength + 12, rootRotation, 0, 16);

                SetBoneRotationsFromByteArray(animator, boneRotations);
                root.position = ByteArrayToVector3(rootPosition, 0);
                root.rotation = ByteArrayToQuaternion(rootRotation, 0);
            }
        }
    }
}