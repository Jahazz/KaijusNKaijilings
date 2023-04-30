using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingRune : MonoBehaviour
{
    [field: SerializeField]
    private MeshRenderer RuneRenderer { get; set; }

    private float Speed { get; set; }
    private Color DefaultColor { get; set; }

    public void Start ()
    {
        Speed = Random.Range(-1.0f, 1.0f);
        DefaultColor = RuneRenderer.material.GetColor("_EmissionColor");
        SetGlowIntensity(10f);
    }


    void Update()
    {
        RuneRenderer.transform.Rotate(new Vector3(0, Speed, 0), Space.Self);
    }

    public void SetGlowIntensity(float value)
    {
        RuneRenderer.material.SetColor("_EmissionColor", DefaultColor * value);
    }
}
