using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

[CreateAssetMenu(menuName ="Choice", fileName ="Create new story choice")]
public class StoryChoiceData : NScriptableObject {
    [Multiline]
    public string storyDescription;
    [Multiline]
    public string storyResolution;
    public List<StoryChoiceData> nextStoryChoices;

    public override string ToString() => "Description="+storyDescription+" NextChoices="+nextStoryChoices.Count;
}
