using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RMC_protocol
{
    namespace parameters
    {
        public class RMC_param
        {
            public string[] bones = {
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
        }
    }

    namespace RMC_function
    {
        public class functions : MonoBehaviour
        {
            public byte[] QuaternionToByteArray(Quaternion rotation)
            {
                byte[] byteArray = new byte[16];
                Buffer.BlockCopy(new float[] { rotation.x, rotation.y, rotation.z, rotation.w }, 0, byteArray, 0, 16);
                return byteArray;
            }

            public Quaternion ByteArrayToQuaternion(byte[] byteArray, int offset)
            {
                float[] floatArray = new float[4];
                Buffer.BlockCopy(byteArray, offset, floatArray, 0, 16);
                return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
            }

            public byte[] Vector3ToByteArray(Vector3 vector)
            {
                byte[] byteArray = new byte[12];
                Buffer.BlockCopy(new float[] { vector.x, vector.y, vector.z }, 0, byteArray, 0, 12);
                return byteArray;
            }

            public Vector3 ByteArrayToVector3(byte[] byteArray, int offset)
            {
                float[] floatArray = new float[3];
                Buffer.BlockCopy(byteArray, offset, floatArray, 0, 12);
                return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
            }

            public byte[] GetBoneRotationsAsByteArray(Animator animator)
            {
                if (!animator) return null;

                RMC_param param = new RMC_param();
                string[] bones = param.bones;

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

            public HumanBodyBones ConvertStringToHumanBodyBone(string boneName)
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
                    default: return HumanBodyBones.LastBone;
                }
            }

            public void SetBoneRotationsFromByteArray(byte[] rotationsByteArray, Animator sourceAnimator, bool isMine)
            {
                if (!isMine)
                {
                    if (!sourceAnimator) return;

                    RMC_param param = new RMC_param();
                    string[] bones = param.bones;

                    sourceAnimator.transform.position = ByteArrayToVector3(rotationsByteArray, 0);

                    for (int i = 0; i < bones.Length; i++)
                    {
                        HumanBodyBones bone = ConvertStringToHumanBodyBone(bones[i]);
                        if (bone != HumanBodyBones.LastBone)
                        {
                            Transform boneTransform = sourceAnimator.GetBoneTransform(bone);
                            if (boneTransform)
                            {
                                boneTransform.localRotation = ByteArrayToQuaternion(rotationsByteArray, i * 16 + 12);
                            }
                        }
                    }
                }
            }
        }
    }
}
