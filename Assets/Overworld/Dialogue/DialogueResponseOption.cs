using MVC.List;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueResponseOption : ListElement<DialogueResponseOptionData>
{
    [field: SerializeField]
    private TMP_Text ResponseText { get; set; }
    private DialogueResponseOptionData ResponseOptionData { get; set; }

    public override void Initialize (DialogueResponseOptionData elementData)
    {
        ResponseOptionData = elementData;
        ResponseText.text = ResponseOptionData.ResponseText;
    }

    public void OnResponseSelected ()
    {
        SingletonContainer.Instance.DialogueManager.DialogueController.SelectResponse(ResponseOptionData.ResponseID);
    }
}
