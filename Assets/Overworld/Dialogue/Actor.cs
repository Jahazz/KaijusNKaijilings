using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Actor
{
    public Vector3 InitialPosition { get; private set; }
    public Quaternion InitialRotation { get; private set; }
    public GameObject Model { get; private set; }
    protected Animator Animator { get; private set; }

    protected int InitialLayer { get; private set; }

    private const string TALK_ANIMATOR_PARAMETER = "IsTalking";
    private const string DIALOGUE_LAYER = "Dialogue";

    public Actor (Animator animator)
    {
        Animator = animator;
        Model = Animator.gameObject;
        InitialPosition = Model.transform.position;
        InitialRotation = Model.transform.rotation;
        InitialLayer = Model.layer;
        SetLayerRecursively(Model.transform, LayerMask.NameToLayer(DIALOGUE_LAYER));
    }

    public void ResetLayer ()
    {
        SetLayerRecursively(Model.transform, InitialLayer);
    }

    public void SetTalking (bool state)
    {
        Animator.SetBool(TALK_ANIMATOR_PARAMETER, state);
    }
    private void SetLayerRecursively (Transform target, int layerID)
    {
        target.gameObject.layer = layerID;

        for (int i = 0; i < target.transform.childCount; i++)
        {
            SetLayerRecursively(target.transform.GetChild(i), layerID);
        }
    }

}
