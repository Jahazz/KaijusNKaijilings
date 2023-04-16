using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkNodeTagsData
{
    public string SpeakerID { get; private set; }
    public Side SpeakerSide { get; private set; }

    private const string SPEAKER_TAG = "speaker";
    private const string SIDE_TAG = "side";

    public InkNodeTagsData (List<string> tagsCollection)
    {
        string outputValue;

        foreach (string tag in tagsCollection)
        {

            if (TryToGetTagValue(tag, SPEAKER_TAG, out outputValue))
            {
                SpeakerID = outputValue;
            }
            else if (TryToGetTagValue(tag, SIDE_TAG, out outputValue))
            {
                SpeakerSide = (Side)Enum.Parse(typeof(Side), outputValue);
            }
        }
    }

    private bool TryToGetTagValue (string source, string tagToLookUp, out string tagValue)
    {
        bool output = source.StartsWith(tagToLookUp);

        if (output == true)
        {
            tagValue = source.Substring(tagToLookUp.Length + 1).Trim();
        }
        else
        {
            tagValue = null;
        }

        return output;
    }
}

public enum Side
{
    NONE,
    LEFT,
    RIGHT
}
