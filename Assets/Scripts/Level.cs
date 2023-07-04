public class Level
{
    public string levelName { get; private set; }
    public bool unlocked { get; private set; }
    public bool beat { get; private set; }
    public bool speedRun { get; private set; }
    public bool challengeToken { get; private set; }
    public int bestTime { get; private set; }
    public int speedRunTarget { get; private set; }

    /// <summary>
    /// Constructor for Level object
    /// </summary>
    /// <param name="levelName">Name of the level - Should match the scene name that represents it</param>
    /// <param name="unlocked">Flag for if the level has been unlocked</param>
    /// <param name="beat">Flag for if the level has been beaten</param>
    /// <param name="speedRun">Flag for if the best completion time is under the speed run target time</param>
    /// <param name="challengeToken">Flag for if the challenge token for the level has been collected</param>
    /// <param name="bestTime">Shortest time(in MS) that the level has been beaten</param>
    /// <param name="speedRunTarget">Target time(in MS) to beat the level for the speed run completion</param>
    public Level(string levelName, bool unlocked, bool beat, bool speedRun, bool challengeToken, int bestTime, int speedRunTarget) {
        this.levelName = levelName;
        this.unlocked = unlocked;
        this.beat = beat;
        this.speedRun = speedRun;
        this.challengeToken = challengeToken;
        this.bestTime = bestTime;
        this.speedRunTarget = speedRunTarget;
    }

    public void Unlock() {
        unlocked = true;
    }

    public void Beat() {
        beat = true;
    }

    public void SpeedRunCompleted() {
        speedRun = true;
    }

    public void ChallengeTokenCollected() {
        challengeToken = true;
    }

    public void SetBestTime(int time) {
        bestTime = time <= bestTime ? time : bestTime;
    }
}
