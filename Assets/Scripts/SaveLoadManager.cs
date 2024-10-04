using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour {
    [SerializeField] string storiesPath = "C:/Users/alexr/UnityGames/Projects/Story Choice/Assets/stories.txt";
    [SerializeField] string enemiesPath = "C:/Users/alexr/UnityGames/Projects/Story Choice/Assets/enemies.txt";
    // Serialize and save StoryChoice list to a file
    public void SaveStories(List<StoryChoiceData> storyChoices, string filePath) {
        if (string.IsNullOrEmpty(filePath)) {
            Debug.LogError("Invalid story file path.");
            return;
        }

        StoryChoiceList storyListWrapper = new StoryChoiceList { stories = storyChoices };
        string json = JsonUtility.ToJson(storyListWrapper, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Stories saved to " + filePath);
    }

    // Serialize and save EnemyStats list to a file
    public void SaveEnemies(List<EnemyStats> enemyStats, string filePath) {
        if (string.IsNullOrEmpty(filePath)) {
            Debug.LogError("Invalid enemy file path.");
            return;
        }

        EnemyStatsList enemyListWrapper = new EnemyStatsList { enemies = enemyStats };
        string json = JsonUtility.ToJson(enemyListWrapper, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Enemies saved to " + filePath);
    }

    // Deserialize and load StoryChoice list from a file
    public List<StoryChoiceData> LoadStories(string filePath = null) {
        if (filePath == null || !File.Exists(filePath)) {
            filePath = storiesPath;
            if (!File.Exists(filePath)) {
                Debug.LogError("Story file not found at: " + filePath);
                return new List<StoryChoiceData>();
            }
        }
        string json = File.ReadAllText(filePath);
        StoryChoiceList storyListWrapper = JsonUtility.FromJson<StoryChoiceList>(json);
        Debug.Log("Stories loaded from " + filePath);
        return storyListWrapper.stories;
    }

    // Deserialize and load EnemyStats list from a file
    public List<EnemyStats> LoadEnemies(string filePath = null) {
        if (filePath == null || !File.Exists(filePath)) {
            filePath = enemiesPath;
            if (!File.Exists(filePath)) {
                Debug.LogError("Enemy file not found at: " + filePath);
                return new List<EnemyStats>();
            }
        }

        string json = File.ReadAllText(filePath);
        EnemyStatsList enemyListWrapper = JsonUtility.FromJson<EnemyStatsList>(json);
        Debug.Log("Enemies loaded from " + filePath);
        return enemyListWrapper.enemies;
    }
}
[Serializable]
public class StoryChoiceList {
    public List<StoryChoiceData> stories;
}

[Serializable]
public class EnemyStatsList {
    public List<EnemyStats> enemies;
}