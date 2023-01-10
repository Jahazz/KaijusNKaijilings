using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonContainer : SingletonMonobehaviour<SingletonContainer>
{
    [field: SerializeField]
    public EntityManager EntityManager { get; private set; }
}
