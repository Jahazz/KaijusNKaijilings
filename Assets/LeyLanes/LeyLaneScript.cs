using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeyLaneScript : MonoBehaviour
{
    [field: SerializeField]
    private MeshRenderer LeyCrystalRenderer { get; set; }
    [field: SerializeField]
    private List<RotatingRune> RunesCollection { get; set; } = new List<RotatingRune>();
    private Color DefaultColor { get; set; }
    public Vector3 InitialPosition { get; private set; }
    public Vector3 InitialScale { get; private set; }
    public Quaternion InitialRotation { get; private set; }
    protected int InitialLayer { get; private set; }
    protected int SceneLayer { get; private set; }
    private float CurrentGlow { get; set; }
    private float CurrentAlpha { get; set; }
    private float CurrentRotationSpeed { get; set; }


    public void OpenLeyLane ()
    {
        InitialLayer = gameObject.layer;
        SceneLayer = LayerMask.NameToLayer("LeyLane");
        SetLayerRecursively(transform, SceneLayer);
        SingletonContainer.Instance.LeyLaneManager.OpenLeyLaneMenu(this);
    }

    public void SetGlow (float value)
    {
        foreach (var item in RunesCollection)
        {
            item.SetGlowIntensity(value);
        }

        CurrentGlow = value;
        LeyCrystalRenderer.material.SetColor("_EmissionColor", DefaultColor * value);
    }

    public void SetRunesVisibility (float targetAlpha, float time)
    {
        DOTween.To(SetRunesVisibility, CurrentAlpha, targetAlpha, time);
    }
    public void SetRunesRotationSpeed (float targetSpeed, float time)
    {
        DOTween.To(SetRunesRotationSpeed, CurrentRotationSpeed, targetSpeed, time);
    }

    public void SetRunesRotationSpeed (float speed)
    {
        foreach (var item in RunesCollection)
        {
            item.SetRotationSpeed(speed);
        }

        CurrentRotationSpeed = speed;
    }

    private void SetRunesVisibility (float targetAlpha)
    {
        foreach (var item in RunesCollection)
        {
            item.SetBaseMapAlpha(targetAlpha);
        }

        CurrentAlpha = targetAlpha;
    }

    public void ResetLayer ()
    {
        SetLayerRecursively(transform, InitialLayer);
    }

    // Start is called before the first frame update
    void Start ()
    {
        Setup();
        DOTween.To(SetGlow, CurrentGlow, 10, 3).SetLoops(-1, LoopType.Yoyo);
    }

    private void Setup ()
    {
        InitialScale = transform.localScale;
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
        DefaultColor = LeyCrystalRenderer.material.GetColor("_EmissionColor");

    }

    private void SetLayerRecursively (Transform target, int layerID)//TODO: to static method
    {
        target.gameObject.layer = layerID;

        for (int i = 0; i < target.transform.childCount; i++)
        {
            SetLayerRecursively(target.transform.GetChild(i), layerID);
        }
    }
}
