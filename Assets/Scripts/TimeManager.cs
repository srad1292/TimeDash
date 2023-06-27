using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField] float timeBetweenPauses;
    [SerializeField] float pauseDuration;

    public Action<bool> OnPauseSet;
    
    private float activeTimer;
    private float pausedTimer;
    private bool isPaused;
    private bool playerLaunched = false;

    private void Start() {
        pausedTimer = 0f;
        activeTimer = 0f;
        isPaused = true;
    }

    private void Update() {
        if(isPaused) {
            pausedTimer += Time.deltaTime;

            if (pausedTimer >= pauseDuration) {
                ResumeTime();
            }
        } else if(playerLaunched) {
            activeTimer += Time.deltaTime;

            if (activeTimer >= timeBetweenPauses) {
                PauseTime();
            }
        }
        
    }

    private void PauseTime() {
        activeTimer = 0f;
        isPaused = true;
        if(OnPauseSet != null) {
            OnPauseSet.Invoke(isPaused);
        }
    }

    private void ResumeTime() {
        pausedTimer = 0f;
        isPaused = false;
        if(OnPauseSet != null) {
            OnPauseSet.Invoke(isPaused);
        }
    }

    public void OnPlayerLaunched() {
        playerLaunched = true;
        if(isPaused) {
            ResumeTime();
        }
    }

}
