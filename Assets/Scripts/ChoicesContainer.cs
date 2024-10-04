using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;
using System;

public class ChoicesContainer : Container<StoryChoice> {
    public List<StoryChoice> storyChoices;
    public StringWritter stringWritter;
    private StoryChoiceData currentStoryData;
    //public string initialPresentMoment;
    private void Start() {
        //stringWritter.WriteSentence(initialPresentMoment);
        //stringWritter.SkipTyping();
        foreach (StoryChoice choice in storyChoices) {
            
        }
    }
    public void SetCurrentStory(StoryChoiceData storyData) {
        var logId = "SetCurrentStory";
        if(storyData == null) {
            logw(logId, "CurrentStory is null => no-op");
            return;
        }
        currentStoryData = storyData;
        List<StoryChoiceData> nextStories = GetNextStories(currentStoryData);
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

    public void Continue(StoryChoiceData choiceData) {
        SetCurrentStory(choiceData);
    }

    private List<StoryChoiceData> GetNextStories(StoryChoiceData storyData) {
        var logId = "GetNextStories";
        if(storyData == null) {
            logw(logId, "StoryData is null => no-op");
            return null;
        }
        return storyData.NextStoryChoices;
        
    }

    public void UpdateStoryResolution() {
        var logId = "UpdateStoryResolution";
        if(currentStoryData==null) {
            logw(logId, "CurrentStory is null => no-op");
            return;
        }
        var storyResolution = currentStoryData.storyResolution;
        logd(logId, "Updating Story Resolution to "+storyResolution.logf());
        stringWritter.WriteSentence(storyResolution);
    }
}
