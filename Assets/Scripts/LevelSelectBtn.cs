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
    [SerializeField] Image beatStar;
    [SerializeField] Image speedRunStar;
    [SerializeField] Image challengeTokenStar;
    [SerializeField] Color unobtainedStarColor;
    [SerializeField] Color beatStarColor;
    [SerializeField] Color speedRunStarColor;
    [SerializeField] Color challengeTokenStarColor;

    Image btnImage;
    
    Level level;

    private void Awake() {
        btnImage = GetComponent<Image>();
    }

    private void Start() {
        level = GameData.Instance.worlds[worldIndex].levels[levelIndex];
        SetupButtonDisplay();
        SetupStarsDisplay();
    }

    private void SetupButtonDisplay() {
        if (level == null) {
            level = GameData.Instance.worlds[worldIndex].levels[levelIndex];
        }
        btnImage.sprite = level.unlocked ? unlockedImg : lockedImg;
        btnImage.color = level.unlocked ? unlockedColor : Color.white;
        btnText.SetText(level.unlocked ? levelIndex.ToString() : "");
    }

    public void UnlockButton() {
        SetupButtonDisplay();
        SetupStarsDisplay();
    }

    private void SetupStarsDisplay() {
        if(!level.unlocked) { return; }

        beatStar.gameObject.SetActive(true);
        speedRunStar.gameObject.SetActive(true);
        challengeTokenStar.gameObject.SetActive(true);

        
        beatStar.color = level.beat ? beatStarColor : unobtainedStarColor;
        speedRunStar.color = level.speedRun ? speedRunStarColor : unobtainedStarColor;
        challengeTokenStar.color = level.challengeToken ? challengeTokenStarColor : unobtainedStarColor;


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
