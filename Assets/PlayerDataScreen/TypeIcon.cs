using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeIcon : MonoBehaviour
{
    [field: SerializeField]
    private Image TypeImage { get; set; }

    public void SetIcon (Sprite sourceSprite)
    {
        TypeImage.sprite = sourceSprite;
    }
}
