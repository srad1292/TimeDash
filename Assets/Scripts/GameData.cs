using UnityEngine;

public class GameData : MonoBehaviour
{

    public static GameData Instance; 

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
            activeLevelIndex = -1;
            activeWorldIndex = -1;
            InitializeGameData();
        }
    }

    public World[] worlds { get; private set; } 
    public int activeLevelIndex {get; private set;} 
    public int activeWorldIndex {get; private set;} 
    public Level activeLevel {get; private set;} 
    public World activeWorld {get; private set;} 
    public bool unlockNextLevel {get; private set;} 
    public bool unlockNextWorld {get; private set;}

    public bool justFinishedLevel { get; private set; }

    private void InitializeGameData() {
        Level[] world1Levels = new Level[] {
            new Level("GameScene", true, false, false, false, -1, 40000),
            new Level("GameSceneTwo", false, false, false, false, -1, 4000),
            new Level("GameSceneThree", false, false, false, false, -1, 4000),
            new Level("GameSceneFour", false, false, false, false, -1, 4000),
        };

        worlds = new World[] {
            new World(world1Levels, true, false, false)
        };
    }

    public void SelectLevel(int worldIndex, int levelIndex) {
        activeLevelIndex = levelIndex;
        activeWorldIndex = worldIndex;

        activeWorld = worlds[worldIndex];
        activeLevel = worlds[worldIndex].levels[levelIndex];
    }

    public void ClearLevel() {
        activeLevelIndex = -1;
        activeWorldIndex = -1;

        activeLevel = null;
        activeWorld = null;
    }

    public void ClearJustFinishedLevel() {
        justFinishedLevel = false;
    }

    public void HandleLevelFinished(int timeToBeat, bool collectedToken) {
        bool prevBeaten = Instance.activeLevel.beat;
        Instance.activeLevel.Beat();
        Instance.SetUnlocksBasedOnLevel(prevBeaten);

        Instance.activeLevel.SetBestTime(timeToBeat);
        print("Speed Run Target: " + Instance.activeLevel.speedRunTarget);
        print("Actual Time: " + timeToBeat);
        if (timeToBeat <= Instance.activeLevel.speedRunTarget) {
            Instance.activeLevel.SpeedRunCompleted();
        }

        if (collectedToken) {
            Instance.activeLevel.ChallengeTokenCollected();
        }

        if(Instance.activeLevel.speedRun && Instance.activeLevel.challengeToken) {
            Instance.activeWorld.CheckAndSetCompleted();
        }

        Instance.justFinishedLevel = true;
    }

    private void SetUnlocksBasedOnLevel(bool previouslyBeaten) {
        if(!previouslyBeaten) {
            if (activeLevelIndex == worlds[activeWorldIndex].levels.Length - 1) {
                unlockNextWorld = true;
            } else if(!activeWorld.isBonusWorld) {
                unlockNextLevel = true;
            }
        }
        
    }

    public void ClearUnlocks() {
        unlockNextLevel = false;
        unlockNextWorld = false;
    }


}
