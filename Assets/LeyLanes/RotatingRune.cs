using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingRune : MonoBehaviour
{
    [field: SerializeField]
    private MeshRenderer RuneRenderer { get; set; }

    private float Speed { get; set; }
    private float DefaultSpeed { get; set; }
    private Color DefaultColor { get; set; }

    private const string EMISSION_COLOR_NAME = "_EmissionColor";
    private const string BASE_COLOR_NAME = "_BaseColor";

    public void Start ()
    {
        DefaultSpeed = Speed = Random.Range(-1.0f, 1.0f);
        DefaultColor = RuneRenderer.material.GetColor(EMISSION_COLOR_NAME);
        SetGlowIntensity(10f);
    }

    public void SetRotationSpeed (float speed)
    {
        Speed = speed;
    }

    public void SetRotationSpeedToDefault ()
    {
        Speed = DefaultSpeed;
    }


    void Update()
    {
        RuneRenderer.transform.Rotate(new Vector3(0, Speed, 0), Space.Self);
    }

    public void SetGlowIntensity(float value)
    {
        RuneRenderer.material.SetColor(EMISSION_COLOR_NAME, DefaultColor * value);
    }

    public void SetBaseMapAlpha (float alphaValue)
    {
        Color defaultColor = RuneRenderer.material.GetColor(BASE_COLOR_NAME);
        defaultColor.a = alphaValue;
        RuneRenderer.material.SetColor(BASE_COLOR_NAME, defaultColor);
    }
}
