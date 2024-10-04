using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NinjaTools;
using System;

public class StoryChoice : NinjaMonoBehaviour {
    public StringWritter stringWritter;
    [SerializeField]
    private StoryChoiceData _storyChoiceData;
    public StoryChoiceData StoryChoiceData {
        get => _storyChoiceData;
        set {
            var logId = "StoryChoiceData_set";
            if(value==null) {
                logd(logId, "Tried to set StoryChoiceData from "+_storyChoiceData.logf()+" to "+value.logf()+" => Deactivating");
                Deactivate();
                return;
            }
            logd(logId, "Setting StoryChoiceData from "+_storyChoiceData.logf()+" to "+value.logf()+" => Updating and Activating");
            _storyChoiceData = value;
            stringWritter.WriteSentence(_storyChoiceData.storyDescription);
            Activate();
        }
    }

    public static Action<StoryChoiceData> CurrentStoryChanged;

    public TextMeshProUGUI storyDescriptionText;

    public void Deactivate() {
        var logId = "Deactivate";
        logd(logId,"Deactivating while "+StoryChoiceData.logf());
        gameObject.SetActive(false);
    }
    public void Activate() {
        var logId = "Activate";
        logd(logId,"Activating while "+StoryChoiceData.logf());
        gameObject.SetActive(true);
    }

    private void Start() {
        var logId = "Start";
        if(StoryChoiceData==null) {
            logw(logId, "StoryChoiceData is null => Deactivating");
            Deactivate();
            return;
        }
        stringWritter.WriteSentence(StoryChoiceData.storyDescription);
    }
    public void OnButtonClick() {
        SetCurrentStory(StoryChoiceData);
    }
    private void SetCurrentStory(StoryChoiceData storyChoiceData) {
        var logId = "SetCurrentStory";
        if(storyChoiceData==null) {
            logw(logId, "StoryData is null => no-op");
            return;
        }
        logd(logId, "Setting CurrentStory to " + storyChoiceData.Id);
        CurrentStoryChanged?.Invoke(storyChoiceData);
    }
    public override string ToString() => name+" StoryData="+StoryChoiceData;
}
