using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueResponseOptionData
{
    public string ResponseText { get; private set; }
    public int ResponseID { get; private set; }

    public DialogueResponseOptionData (string responseText, int responseID)
    {
        ResponseText = responseText;
        ResponseID = responseID;
    }
}
