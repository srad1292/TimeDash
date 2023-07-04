using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    void Start()
    {
        if(GameData.Instance.justFinishedLevel == true) {
            HandleJustFinishedLevel();
        }
    }

    private void HandleJustFinishedLevel() {
        if(GameData.Instance.unlockNextWorld) {
            // Handle unlocking of new world
        } else if(GameData.Instance.unlockNextLevel) {
            // Handle unlocking of new level
        }

        GameData.Instance.ClearLevel();
        GameData.Instance.ClearUnlocks();
        GameData.Instance.ClearJustFinishedLevel();
    }

}
