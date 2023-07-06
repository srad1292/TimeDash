using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectBtn : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] int worldIndex;
    [SerializeField] int levelIndex;
    [SerializeField] TextMeshProUGUI btnText;
    [SerializeField] Sprite lockedImg;
    [SerializeField] Sprite unlockedImg;
    [SerializeField] Color unlockedColor;

    Image btnImage;
    
    Level level;

    private void Awake() {
        btnImage = GetComponent<Image>();
    }

    private void Start() {
        print("Setting up level");
        level = GameData.Instance.worlds[worldIndex].levels[levelIndex];
        if(level == null) {
            print("Woops level is null for: " + worldIndex.ToString() + "-" + levelIndex.ToString());
        }
        SetupButtonDisplay();
    }

    private void SetupButtonDisplay() {
        if(GameData.Instance == null) {
            print("WTF game data is null");
        }
        if (level == null) {
            print("Getting level for: " + worldIndex.ToString() + "-" + levelIndex.ToString());
            level = GameData.Instance.worlds[worldIndex].levels[levelIndex];
        }
        if(level == null) {
            print("Level still null????");
        } else {
            btnImage.sprite = level.unlocked ? unlockedImg : lockedImg;
            btnImage.color = level.unlocked ? unlockedColor : Color.white;
            btnText.SetText(level.unlocked ? levelIndex.ToString() : "");
        }
    }

    public void UnlockButton() {
        SetupButtonDisplay();
    }

    public void SelectLevel() {
        if(level.unlocked) {
            GameData.Instance.SelectLevel(worldIndex, levelIndex);
            levelLoader.LoadGameLevel(level.levelName);
        }
    }

    public int GetWorldIndex() {
        return worldIndex;
    }

    public int GetLevelIndex() {
        return levelIndex;
    }

}
