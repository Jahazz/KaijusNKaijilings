using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    [field: SerializeField]
    private TMP_Text AuthorNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text TextContentLabel { get; set; }
    [field: SerializeField]
    private float SecondsPerLeter { get; set; }

    public bool IsAnimatingText { get; private set; }
    private string CurrentText { get; set; }
    private  IEnumerator TextAnimator { get; set; }
    private Player CurrentlyTalkingPlayer { get; set; }

    public void SetTextContents (Player author, string text)
    {
        CurrentlyTalkingPlayer = author;
        AuthorNameLabel.text = CurrentlyTalkingPlayer.Name;
        CurrentText = text;
        TextAnimator = AnimateText();
        StartCoroutine(TextAnimator);
    }

    public IEnumerator AnimateText ()
    {
        IsAnimatingText = true;
        CurrentlyTalkingPlayer.SetTalking(true);

        string[] words = CurrentText.Split(' ');
        TextContentLabel.text = string.Empty;

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];

            if (i != words.Length - 1)
            {
                word += " ";
            }

            string previousText = TextContentLabel.text;

            float lastHeight = TextContentLabel.preferredHeight;
            TextContentLabel.text += word;

            if (TextContentLabel.preferredHeight > lastHeight)
            {
                previousText += System.Environment.NewLine;
            }

            for (int j = 0; j < word.Length; j++)
            {
                TextContentLabel.text = previousText + word.Substring(0, j + 1);
                yield return new WaitForSeconds(SecondsPerLeter);
            }
        }

        TextContentLabel.text = CurrentText;
        IsAnimatingText = false;
        CurrentlyTalkingPlayer.SetTalking(false);
    }

    public void SkipAnimation ()
    {
        StopCoroutine(TextAnimator);
        TextContentLabel.text = CurrentText;
        IsAnimatingText = false;
        CurrentlyTalkingPlayer.SetTalking(false);
    }
}
