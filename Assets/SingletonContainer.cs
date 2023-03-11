using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonContainer : SingletonMonobehaviour<SingletonContainer>
{
    [field: SerializeField]
    public EntityManager EntityManager { get; private set; }
    [field: SerializeField]
    public PlayerManager PlayerManager { get; private set; }
    [field: SerializeField]
    public BreedingManager BreedingManager { get; private set; }
    [field: SerializeField]
    public DialogueManager DialogueManager { get; private set; }
    [field: SerializeField]
    public OverworldPlayerCharacterManager OverworldPlayerCharacterManager { get; private set;}
}
