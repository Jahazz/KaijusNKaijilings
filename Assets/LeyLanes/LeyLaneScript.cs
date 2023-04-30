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
    public float CurrentGlow { get; set; }
    private Color DefaultColor { get; set; }

    public void SetGlow (float value)
    {
        foreach (var item in RunesCollection)
        {
            item.SetGlowIntensity(value);
        }

        CurrentGlow = value;
        LeyCrystalRenderer.material.SetColor("_EmissionColor", DefaultColor * value);
    }

    // Start is called before the first frame update
    void Start ()
    {
        DefaultColor = LeyCrystalRenderer.material.GetColor("_EmissionColor");
        DOTween.To(SetGlow, CurrentGlow, 10, 3).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
