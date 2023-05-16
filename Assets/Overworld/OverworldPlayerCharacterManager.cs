using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerCharacterManager : MonoBehaviour
{
    [field: SerializeField]
    public AssetInputs AssetInputsInstance { get; private set; }
    [field: SerializeField]
    public ThirdPersonController ThirdPersonControllerInstance { get; private set; }
    [field: SerializeField]
    public CinemachineVirtualCamera CameraFollow { get; set; }

    public void FreezePlayer ()
    {
        CameraFollow.gameObject.SetActive(false);
        AssetInputsInstance.SetCharacterMovementActive(false);
        ThirdPersonControllerInstance.FreezeCharacterMovement();
    }

    public void UnfreezePlayer ()
    {
        CameraFollow.gameObject.SetActive(true);
        AssetInputsInstance.SetCharacterMovementActive(true);
        ThirdPersonControllerInstance.UnfreezeCharacterMovement();
    }
}
