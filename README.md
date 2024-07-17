# Remote Motion Capture Protocol (RMCP) for Unity
## Purpose
This repository includes the source codes of Remote Motion Capture protocol (RMCP) created by Division for Smart Healthcare Research, Dokkyo Medical University. This is limited private and unauthorized reproduction is prohibited and legal action will be taken upon discovery.
## Contact
Shun Irie, Ph.D. Senior Assistant Professor, Division for Smart Healthcare Research, Dokkyo Medical University
Address: 880, Kita-Kobayashi, Mibu, Tochigi, Japan Tel: +81-282-87-2492
e-mail: s-irie360@dokkyomed.ac.jp
## Acknowledge
MEXT KAKENHI (S.I., no. 22H05220 and 24H00909) and Research Support Award 2023 of the Dokkyo International Medical Education and Research Foundation (S.I., no. R5-12), and JKA and its promo-tional funds from KEIRIN RACE (no. 2023M-332).

<img src="https://github.com/user-attachments/assets/fdab8d35-89e1-40c1-8511-36f5eee5b702" width="160pt">

<img src="https://github.com/user-attachments/assets/555a9db3-1b9a-4f87-a191-6cb322b19dd2" width="160pt">

<img src="https://github.com/user-attachments/assets/82c7ad1f-8f74-4534-80d8-1e51b3fca6e6" width="160pt">

# How to install RMCP
In this repository, you can install RMCP using expanded Editor functions in Unity.
## Requirements
| OS | Unity version |
|---|---|
| Windows 10 and 11| later 2021.3.26.f1 |
| Mac OS later 13.4.1| later 2021.3.26.f1 |
| Android (>API level 22)| later 2021.3.26.f1 |

* Not using development build for Android.
* For iOS or WebGL, the system requirements for Photon Server includes these environments but I never tried. Please tell me while you succeeded to build in iOS and WebGL.

### 1. Install unity package file
The unity package file is available [here](https://github.com/shun-irie/RMCP_Dokkyo/raw/main/RemoteMotionCaptureProtocol.unitypackage).
### 2. Import PUN2 via package manager
This unity package requires the PUN2 from Photon Engine. After installation of PUN2, you must input the App ID for Photon Server.
PUN2 is available from [Unity Asset Store](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922?locale=ja-JP).
### 3. Import unity package on Unity
You can import the unitypackage file from Assets>Import package>Custom package.
### 4. Create Server Prefab
At first, you should create server prefab from the Menu (RMC>Create Server Prefab).
### 5. Import avatar object with motion capture scripts
You should import and add avatar object with motion capture scripts. The avatar rig must be set to humanoid.
### 6. Avatar prefab settings
First, you should select the avatar object and run the auto setting function (RMC>Auto Setting Avatar Prefab). After setting the prefab, you should delete the avatar object from hierarchy.The avatar prefab is stored in Assets/Photon/PhotonUnityNetworking/Resources. In the inspector, you should set sampling frequency, the source animator, and root position of an avatar in the component of RMCprotocol.
### 7. Deactivate setting for motion capture scripts in remote place
Sometimes, the motion capture scripts interfered the other motion capture system. Thus, the motion capture scripts must be deactivated in the remote places. You can deactivate the motion capture scripts in remote places using the component of Controller Script and input the names of MonoBehaviours related to motion capture functions.
### 8. ServerPrefab settings
Last, you should modify the ServerPrefab in the hierarchy, such as room name and avatar prefab name in the resource folder.
