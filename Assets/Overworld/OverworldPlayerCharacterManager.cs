using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerCharacterManager : MonoBehaviour
{
    [field: SerializeField]
    public CharacterMovementInterpreter CharacterMovementInterpreter { get; private set; }
    [field: SerializeField]
    public OverworldPlayerMovement OverworldCharacterMovement { get; private set; }

    public void FreezePlayer ()
    {
        OverworldCharacterMovement.SetCharacterMovementActive(false);
        CharacterMovementInterpreter.FreezeCharacterMovement();
    }

    public void UnfreezePlayer ()
    {
        OverworldCharacterMovement.SetCharacterMovementActive(true);
        CharacterMovementInterpreter.UnfreezeCharacterMovement();
    }
}
