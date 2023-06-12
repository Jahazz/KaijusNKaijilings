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

    public PlayerState CurrentPlayerState { get; set; } 

    public void FreezePlayer (PlayerState reason)
    {
        CurrentPlayerState = reason;
        CameraFollow.gameObject.SetActive(false);
        AssetInputsInstance.SetCharacterMovementActive(false);
        ThirdPersonControllerInstance.FreezeCharacterMovement();

        if (reason == PlayerState.IN_BATTLE)
        {
            SetActionMap(InputMapType.BATTLE);
        }
    }

    public void UnfreezePlayer ()
    {
        CurrentPlayerState = PlayerState.IN_OVERWORLD;
        CameraFollow.gameObject.SetActive(true);
        AssetInputsInstance.SetCharacterMovementActive(true);
        ThirdPersonControllerInstance.UnfreezeCharacterMovement();
        SetActionMap(InputMapType.PLAYER);
    }

    private void SetActionMap (InputMapType mapType)
    {
        SingletonContainer.Instance.InputSystem.SwitchCurrentActionMap(mapType.ToString());
    }
}
