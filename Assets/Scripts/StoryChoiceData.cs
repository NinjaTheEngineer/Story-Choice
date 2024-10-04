using System;
using System.Collections.Generic;

[Serializable]
public class StoryChoiceData {
    public int Id;
    public string storyDescription;
    public string storyResolution;
    public int[] nextStoryChoicesIds;
    public int[] enemiesId;

    [NonSerialized]
    private List<StoryChoiceData> _nextStoryChoices;
    public List<StoryChoiceData> NextStoryChoices => _nextStoryChoices;
    public void SetNextStories(List<StoryChoiceData> nextStoryChoices) => _nextStoryChoices = nextStoryChoices;
}