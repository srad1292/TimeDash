using UnityEngine;
using System.Linq;

public class LevelSelectionMenu : MonoBehaviour
{

    [SerializeField] LevelSelectBtn[] levelSelectBtns;


    void Start()
    {
        if(GameData.Instance.justFinishedLevel == true) {
            HandleJustFinishedLevel();
        }
    }

    private void HandleJustFinishedLevel() {
        if(GameData.Instance.unlockNextWorld) {
            // Handle unlocking of new world
            // GameData.Instance.worlds[GameData.Instance.activeWorldIndex + 1].Unlock();
        } else if(GameData.Instance.unlockNextLevel) {
            // Handle unlocking of new level
            GameData.Instance.activeWorld.levels[GameData.Instance.activeLevelIndex + 1].Unlock();
            LevelSelectBtn levelSelectBtn = levelSelectBtns.First(button => button.GetWorldIndex() == GameData.Instance.activeWorldIndex && button.GetLevelIndex() == GameData.Instance.activeLevelIndex+1);
            if(levelSelectBtn != null) {
                levelSelectBtn.UnlockButton();
            }
        }

        GameData.Instance.ClearLevel();
        GameData.Instance.ClearUnlocks();
        GameData.Instance.ClearJustFinishedLevel();
    }

}
