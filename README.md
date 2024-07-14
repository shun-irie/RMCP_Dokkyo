# RMCP API
This is an API document for using the RMC protocol.
## namespace: RMC
### namespace: RMC.Server
#### Class: PhotonServer
```csharp
public void Connect(string roomName, Vector3 initialPosition, string avatarPrefabName)
```
**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
|string| roomName | room name that the user will log in.|
|Vector3|initialPosition|a global position where an avatar prefab is instantiated.|
|string|avatarPrefabName| a name of avatar prefab file that is instantiated. The file must be included in Assets/Photon/PhotonUnityNetworking/Resources

```csharp
public override void OnConnectedToMaster()
```
A callback while successfully connected with the master server.
```csharp
public override void OnJoinedRoom()
```
A callback while successfully connected with the game server.
### namespace: RMC.MoCap
#### Class: calc_funcs
```csharp
public static byte[] QuaternionToByteArray(Quaternion rotation)
```
**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| Quaternion | rotation | the quaternion of a bone|

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| byte[]| byte array converted from a quaternion of joint rotation|
```csharp
public static Quaternion ByteArrayToQuaternion(byte[] byteArray, int offset)
```
Converting the byte array of a bone rotation to a Quaternion array.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| byte[] | byteArray | joint rotation byte array calculated by QuaternionToByteArray|
| int | offset | offset for reading byte array |

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| Quaternion | a quaternion of bone rotation converted from a byte array |

```csharp
public static byte[] Vector3ToByteArray(Vector3 vector)
```
Converting the position data to a byte array.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| Vector3 | vector | a global position of root position of an avatar|

**Output**
| type | description                         |
| ---- | ----------------------------------- |
|byte[]|a byte array converted from a global position vector|

```csharp
public static Vector3 ByteArrayToVector3(byte[] byteArray, int offset)
```
Converting the byte array to a position vector data.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| byte[]|byteArray| a byte array of a global position vector|
|int|offset|offset of reading byte array|

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| Vector3 | a vector of avatar root position converted from byte array|

```csharp
public static string[] bones
public static HumanBodyBones ConvertStringToHumanBodyBone(string boneName)
```
Converting the bone names to the parameter of HumanBodyBones.
The list of HumanBodynames are written below. The definition of HumanBodyBones should be referred [here](https://docs.unity3d.com/ja/2020.3/ScriptReference/HumanBodyBones.html).

**Input & Output**
| String           | Class                       |
| ---------------- | --------------------------- |
| Hips             | HumanBodyBones.Hips         |
| Spine            | HumanBodyBones.Spine        |
| Chest            | HumanBodyBones.Chest        |
| Neck             | HumanBodyBones.Neck         |
| Head             | HumanBodyBones.Head         |
| LeftShoulder     | HumanBodyBones.LeftShoulder |
| LeftUpperArm     | HumanBodyBones.LeftUpperArm |
| LeftLowerArm     | HumanBodyBones.LeftLowerArm |
| LeftHand         | HumanBodyBones.LeftHand     |
| RightShoulder    | HumanBodyBones.RightShoulder|
| RightUpperArm    | HumanBodyBones.RightUpperArm|
| RightLowerArm    | HumanBodyBones.RightLowerArm|
| RightHand        | HumanBodyBones.RightHand    |
| LeftUpperLeg     | HumanBodyBones.LeftUpperLeg |
| LeftLowerLeg     | HumanBodyBones.LeftLowerLeg |
| LeftFoot         | HumanBodyBones.LeftFoot     |
| LeftToes         | HumanBodyBones.LeftToes     |
| RightUpperLeg    | HumanBodyBones.RightUpperLeg|
| RightLowerLeg    | HumanBodyBones.RightLowerLeg|
| RightFoot        | HumanBodyBones.RightFoot    |
| RightToes        | HumanBodyBones.RightToes    |
| (default)        | HumanBodyBones.LastBone     |

```csharp
public static byte[] GetBoneRotationsAsByteArray(Animator animator)
```
The function extracted the rotations of all bones from the animator. However, the avatar must be ***humanoid***.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
|Animator|animator|animator of an avatar |

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| byte[] | a byte array including all bone rotations|

```csharp
public static void SetBoneRotationsFromByteArray(Animator animator, byte[] rotationsByteArray)
```
The function updates avatar bones from a bone byte array.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| Animator | animator | an animator of an avatar |
| byte[] | rotationsByteArray | a byte array of all bones of an avatar |

```csharp
public static Animator InitializeAnimator(string objectName, out float timePerFrame, float framesPerSecond)
```
Initialize animator and update frequency

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| string | objectName | Name of avatar name |
| float | timePerFrame | time per frame |
| float | framesPerSecond | frames per second |

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| float | timePerFrame (arg out)|

```csharp
 public static void UpdateAnimator(PhotonView photonView, Animator sourceAnimator, Transform root, ref float timer, float timePerFrame, string rpcMethodName)
```
The function processes the synchronization of avatar movement across users.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| PhotonView | photonView | an instance of photon server |
| Animator | sourceAnimator | an animator of an avatar |
| Transform | root | a transform of avatar root |
| float | timer | timer for update (ref) |
| float | timePerFrame | frame time |
| string | rpcMethodName | the name of remote procedure calling |

```csharp
public static void SetAnimatorStateFromByteArray(Animator animator, Transform root, byte[] data)
```
The function for remote procedure calling. 

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| Animator | animator | a animator of an avatar |
| Transform | root | root transform of an avatar |
| byte[] | data | a byte array of global position and rotation in all bones |
