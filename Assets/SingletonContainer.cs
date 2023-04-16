using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public BattleScreenManager BattleScreenManager { get; private set; }
    [field: SerializeField]
    public OverworldPlayerCharacterManager OverworldPlayerCharacterManager { get; private set; }
    [field: SerializeField]
    public CharacterMenuController CharacterMenuController { get; private set; }
    [field: SerializeField]
    public QQ_QuestHandler QuestHandler { get; private set; }
    [field: SerializeField]
    public PlayerInput InputSystem { get; private set; }
}
