using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class StoryAndEnemyEditorWindow : EditorWindow {
    private List<StoryChoiceData> storyChoices = new List<StoryChoiceData>();
    private List<EnemyStats> enemyStats = new List<EnemyStats>();

    private SaveLoadManager saveLoadManager;

    // Set default file paths
    private string storyFilePath;
    private string enemyFilePath;

    private Vector2 scrollPos;

    private Dictionary<int, bool> storyFoldoutStates = new Dictionary<int, bool>();
    private Dictionary<int, bool> enemyFoldoutStates = new Dictionary<int, bool>();

    [MenuItem("Tools/Story and Enemy Editor")]
    public static void ShowWindow() {
        GetWindow<StoryAndEnemyEditorWindow>("Story and Enemy Editor");
    }

    private void OnEnable() {
        // Ensure the default paths are within the Assets folder
        storyFilePath = System.IO.Path.Combine(Application.dataPath, "stories.txt");
        enemyFilePath = System.IO.Path.Combine(Application.dataPath, "enemies.txt");

        // Create or find SaveLoadManager
        saveLoadManager = new GameObject("SaveLoadManager").AddComponent<SaveLoadManager>();

        // Load data if the files exist
        if (System.IO.File.Exists(storyFilePath)) {
            storyChoices = saveLoadManager.LoadStories(storyFilePath);
        }

        if (System.IO.File.Exists(enemyFilePath)) {
            enemyStats = saveLoadManager.LoadEnemies(enemyFilePath);
        }

        // Set all story and enemy foldouts to collapsed initially
        foreach (var story in storyChoices) {
            storyFoldoutStates[story.Id] = false;
        }
        foreach (var enemy in enemyStats) {
            enemyFoldoutStates[enemy.Id] = false;
        }
    }

    private void OnDisable() {
        DestroyImmediate(saveLoadManager.gameObject);
    }
    private void OnGUI() {
        GUILayout.Label("Story and Enemy Editor", EditorStyles.boldLabel);

        // File selection for stories, supporting both .json and .txt
        if (GUILayout.Button("Select Story File")) {
            storyFilePath = EditorUtility.OpenFilePanel("Select Story File", Application.dataPath, "json,txt");
            if (!string.IsNullOrEmpty(storyFilePath)) {
                storyChoices = saveLoadManager.LoadStories(storyFilePath);
            }
        }
        GUILayout.Label("Selected Story File: " + storyFilePath);

        // File selection for enemies, supporting both .json and .txt
        if (GUILayout.Button("Select Enemy File")) {
            enemyFilePath = EditorUtility.OpenFilePanel("Select Enemy File", Application.dataPath, "json,txt");
            if (!string.IsNullOrEmpty(enemyFilePath)) {
                enemyStats = saveLoadManager.LoadEnemies(enemyFilePath);
            }
        }
        GUILayout.Label("Selected Enemy File: " + enemyFilePath);

        GUILayout.Space(20);

        // Begin scroll view for the lists
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

        // Story Section
        GUILayout.Label("Stories", EditorStyles.label);
        if (GUILayout.Button("Add New Story")) {
            int newId = (storyChoices.Count > 0) ? storyChoices[storyChoices.Count - 1].Id + 1 : 1;
            var newStory = new StoryChoiceData { Id = newId };
            storyChoices.Add(newStory);
            storyFoldoutStates[newStory.Id] = false; // Set new story as collapsed by default
        }

        for (int i = 0; i < storyChoices.Count; i++) {
            var story = storyChoices[i];
            if (!storyFoldoutStates.ContainsKey(story.Id)) {
                storyFoldoutStates[story.Id] = true;
            }

            storyFoldoutStates[story.Id] = EditorGUILayout.Foldout(storyFoldoutStates[story.Id], $"Story ID: {story.Id}");

            if (storyFoldoutStates[story.Id]) {
                GUILayout.BeginVertical("box");
                story.Id = EditorGUILayout.IntField("Story ID", story.Id);
                story.storyDescription = EditorGUILayout.TextField("Description", story.storyDescription);
                story.storyResolution = EditorGUILayout.TextField("Resolution", story.storyResolution);

                // Next Story Choices IDs
                GUILayout.Label("Next Story Choices IDs");
                if (story.nextStoryChoicesIds == null)
                    story.nextStoryChoicesIds = new int[0];

                for (int j = 0; j < story.nextStoryChoicesIds.Length; j++) {
                    GUILayout.BeginHorizontal();
                    story.nextStoryChoicesIds[j] = EditorGUILayout.IntField("Next Story ID", story.nextStoryChoicesIds[j]);
                    if (GUILayout.Button("Remove", GUILayout.Width(60))) {
                        List<int> nextChoicesList = new List<int>(story.nextStoryChoicesIds);
                        nextChoicesList.RemoveAt(j);
                        story.nextStoryChoicesIds = nextChoicesList.ToArray();
                    }
                    GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Next Story ID")) {
                    List<int> nextChoicesList = new List<int>(story.nextStoryChoicesIds);
                    nextChoicesList.Add(0);
                    story.nextStoryChoicesIds = nextChoicesList.ToArray();
                }

                // Enemy IDs
                GUILayout.Label("Enemy IDs");
                if (story.enemiesId == null)
                    story.enemiesId = new int[0];

                for (int j = 0; j < story.enemiesId.Length; j++) {
                    GUILayout.BeginHorizontal();
                    story.enemiesId[j] = EditorGUILayout.IntField("Enemy ID", story.enemiesId[j]);
                    if (GUILayout.Button("Remove", GUILayout.Width(60))) {
                        List<int> enemiesList = new List<int>(story.enemiesId);
                        enemiesList.RemoveAt(j);
                        story.enemiesId = enemiesList.ToArray();
                    }
                    GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Enemy ID")) {
                    List<int> enemiesList = new List<int>(story.enemiesId);
                    enemiesList.Add(0);
                    story.enemiesId = enemiesList.ToArray();
                }

                if (GUILayout.Button("Remove Story")) {
                    storyChoices.RemoveAt(i);
                }

                GUILayout.EndVertical();
            }
        }

        GUILayout.Space(20);

        // Enemy Section
        GUILayout.Label("Enemies", EditorStyles.label);

        if (GUILayout.Button("Add New Enemy")) {
            int newId = (enemyStats.Count > 0) ? enemyStats[enemyStats.Count - 1].Id + 1 : 1;
            var newEnemy = new EnemyStats { Id = newId };
            enemyStats.Add(newEnemy);
            enemyFoldoutStates[newEnemy.Id] = false; // Set new enemy as collapsed by default
        }

        for (int i = 0; i < enemyStats.Count; i++) {
            var enemy = enemyStats[i];
            if (!enemyFoldoutStates.ContainsKey(enemy.Id)) {
                enemyFoldoutStates[enemy.Id] = true;
            }

            enemyFoldoutStates[enemy.Id] = EditorGUILayout.Foldout(enemyFoldoutStates[enemy.Id], $"Enemy ID: {enemy.Id}");

            if (enemyFoldoutStates[enemy.Id]) {
                GUILayout.BeginVertical("box");
                enemy.Id = EditorGUILayout.IntField("Enemy ID", enemy.Id);
                enemy.Name = EditorGUILayout.TextField("Enemy Name", enemy.Name);
                enemy.Strength = EditorGUILayout.IntField("Strength", enemy.Strength);
                enemy.Health = EditorGUILayout.IntField("Health", enemy.Health);

                if (GUILayout.Button("Remove Enemy")) {
                    enemyStats.RemoveAt(i);
                }

                GUILayout.EndVertical();
            }
        }

        GUILayout.Space(20);  // Extra space to prevent clipping at the end of the scroll view

        EditorGUILayout.EndScrollView();  // End the scroll view

        GUILayout.Space(10);

        // Separate buttons for saving Story and Enemy files
        if (GUILayout.Button("Save Story Data", GUILayout.Height(30))) {  // Adjusted button height for better visibility
            if (string.IsNullOrEmpty(storyFilePath)) {
                storyFilePath = System.IO.Path.Combine(Application.dataPath, "stories.txt").Replace("\\", "/");
            }
            storyFilePath = EditorUtility.SaveFilePanel("Save Story File", Application.dataPath, "stories", "txt,json");
            if (!string.IsNullOrEmpty(storyFilePath)) {
                saveLoadManager.SaveStories(storyChoices, storyFilePath);
            }
        }

        if (GUILayout.Button("Save Enemy Data", GUILayout.Height(30))) {
            if (string.IsNullOrEmpty(enemyFilePath)) {
                enemyFilePath = System.IO.Path.Combine(Application.dataPath, "enemies.txt").Replace("\\", "/");
            }
            enemyFilePath = EditorUtility.SaveFilePanel("Save Enemy File", Application.dataPath, "enemies", "txt,json");
            if (!string.IsNullOrEmpty(enemyFilePath)) {
                saveLoadManager.SaveEnemies(enemyStats, enemyFilePath);
            }
        }
    }
}