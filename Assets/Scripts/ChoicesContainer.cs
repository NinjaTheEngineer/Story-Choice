using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;

public class ChoicesContainer : Container<StoryChoice> {
    public List<StoryChoice> storyChoices;
    public StringWritter stringWritter;
    private StoryChoiceData currentStoryChoice;
    public string initialPresentMoment;
    private void Start() {
        stringWritter.WriteSentence(initialPresentMoment);
        stringWritter.SkipTyping();
    }
    public void SetNextStories(StoryChoice currentStory) {
        var logId = "SetNextStories";
        if(currentStory==null) {
            logw(logId, "CurrentStory is null => no-op");
            return;
        }
        currentStoryChoice = currentStory.StoryChoiceData;
        var nextStories = currentStory.StoryChoiceData.nextStoryChoices;
        var nextStoriesCount = nextStories.Count;
        var choicesCount = storyChoices.Count;
        for (int i = 0; i < choicesCount; i++) {
            var currentStoryChoice = storyChoices[i];
            if(nextStoriesCount>i) {
                var nextCurrentStory = nextStories[i];
                logd(logId, "CurrentStoryChoice="+currentStoryChoice.logf()+" NextCurrentStory="+nextCurrentStory.logf()+" => setting new StoryChoiceData");
                currentStoryChoice.StoryChoiceData = nextCurrentStory;
            } else {
                logd(logId, "No more NextStories for CurrentStoryChoice="+currentStoryChoice.logf()+" => setting StoryChoiceData to null");
                currentStoryChoice.StoryChoiceData = null;
            }
        }
        //Update story resolution
        UpdateStoryResolution();
    }
    public void UpdateStoryResolution() {
        var logId = "UpdateStoryResolution";
        if(currentStoryChoice==null) {
            logw(logId, "CurrentStory is null => no-op");
            return;
        }
        var storyResolution = currentStoryChoice.storyResolution;
        logd(logId, "Updating Story Resolution to "+storyResolution.logf());
        stringWritter.WriteSentence(storyResolution);
    }
}
