using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;

public class GameManager : NinjaMonoBehaviour {
    [SerializeField] SaveLoadManager saveLoadManager;
    [SerializeField] ChoicesContainer choicesContainer;
    List<StoryChoiceData> storiesData = new List<StoryChoiceData>();
    private void Start() {
        if (saveLoadManager == null) {
            logw("Start", "SaveLoadManager => no-op");
            return;
        }

        storiesData = saveLoadManager.LoadStories();
        List<EnemyStats> enemies = saveLoadManager.LoadEnemies();
        
        foreach (StoryChoiceData storyChoice in storiesData) {
            storyChoice.SetNextStories(FindNextStories(storyChoice));
        }

        choicesContainer.SetCurrentStory(storiesData[0]);
    }

    private void OnEnable() {
        StoryChoice.CurrentStoryChanged += OnCurrentStoryChanged;
    }
    private void OnDisable() {
        StoryChoice.CurrentStoryChanged -= OnCurrentStoryChanged;
    }

    private void OnCurrentStoryChanged(StoryChoiceData data) {
        var logId = "OnCurrentStoryChanged";
        if(data==null) {
            logw(logId, "StoryData is null => no-op");
            return;
        }
        FindNextStories(data);
        choicesContainer.SetCurrentStory(data);
    }

    private List<StoryChoiceData> FindNextStories(StoryChoiceData storyData) {
        var logId = "FindNextStories";
        if(storyData == null) {
            logw(logId, "StoryData is null => returning null");
            return null;
        }
        logd(logId, "Finding next stories for StoryData=" + storyData.Id);
        var storiesCount = storyData.nextStoryChoicesIds.Length;
        List<StoryChoiceData> nextStories = new List<StoryChoiceData>();
        for (int i = 0; i < storiesCount; i++) {
            var nextId = storyData.nextStoryChoicesIds[i];
            var nextChoice = storiesData.Find(c => c.Id == nextId);
            if (nextChoice == null) {
                logw(logId, "NextChoice is null for storyData=" + storyData?.Id.logf() + " => no-op");
                break;
            }
            nextStories.Add(nextChoice);
        }
        return nextStories;
    }
}
