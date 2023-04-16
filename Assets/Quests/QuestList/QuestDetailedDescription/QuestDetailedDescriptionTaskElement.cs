using MVC.List;
using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDetailedDescriptionTaskElement : ListElement<QQ_Task>
{
    [field: SerializeField]
    private TMP_Text TaskNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text TaskDescriptionLabel { get; set; }
    [field: SerializeField]
    private TMP_Text ProgressLabel { get; set; }
    [field: SerializeField]
    private TMP_Text IsOptionalLabel { get; set; }

    private QQ_Task CurrentElementData;
    private const string PROGRESS_LABEL_FORMAT = "{0}/{1}";

    public override void Initialize (QQ_Task elementData)
    {
        CurrentElementData = elementData;

        TaskNameLabel.text = CurrentElementData.Name;
        TaskDescriptionLabel.text = CurrentElementData.Description;
        ProgressLabel.text = string.Format(PROGRESS_LABEL_FORMAT, CurrentElementData.Progress, CurrentElementData.MaxProgress);
        IsOptionalLabel.gameObject.SetActive(CurrentElementData.Optional == true);
    }
}
