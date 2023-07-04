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

    private void Start() {
        btnImage = GetComponent<Image>();
        if(GameData.Instance == null) {
            Debug.Log("Game Data Not Initialized");
        } else if(GameData.Instance.worlds[worldIndex - 1] == null) {
            Debug.Log("World Not Initialized");
        }
        else if (GameData.Instance.worlds[worldIndex - 1].levels[levelIndex-1] == null) {
            Debug.Log("Level Not Initialized");
        }
        level = GameData.Instance.worlds[worldIndex-1].levels[levelIndex-1];
        btnImage.sprite = level.unlocked ? unlockedImg : lockedImg;
        btnImage.color = level.unlocked ? unlockedColor : Color.white;
        btnText.SetText(level.unlocked ? levelIndex.ToString() : "");
    }

    public void SelectLevel() {
        if(level.unlocked) {
            GameData.Instance.SelectLevel(worldIndex - 1, levelIndex - 1);
            levelLoader.LoadGameLevel(level.levelName);
        }
        
    }

}
