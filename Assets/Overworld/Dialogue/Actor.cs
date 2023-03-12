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
    public Player Player { get; private set; }
    protected Animator Animator { get; private set; }

    protected int InitialLayer { get; private set; }
    protected int SceneLayer { get; private set; }

    private const string TALK_ANIMATOR_PARAMETER = "IsTalking";

    public Actor (Player player, string targetLayer)
    {
        Player = player;
        Animator = Player.OverworldAnimator;
        Model = Animator.gameObject;
        InitialPosition = Model.transform.position;
        InitialRotation = Model.transform.rotation;
        InitialLayer = Model.layer;
        SceneLayer = LayerMask.NameToLayer(targetLayer);
        SetLayerRecursively(Model.transform, SceneLayer);
    }

    public void SetLayerOfTransform (Transform transform) // move to utils pls
    {
        SetLayerRecursively(transform, SceneLayer);
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
