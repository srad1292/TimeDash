using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBtn : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] string sceneName;

    public void SelectLevel() {
        levelLoader.LoadGameLevel(sceneName);
    }
}
