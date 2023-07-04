using System.Linq;

public class World
{
    public Level[] levels { get; private set; }
    public bool unlocked { get; private set; }
    public bool completed { get; private set; }
    public bool isBonusWorld { get; private set; }

    public World(Level[] levels, bool unlocked, bool completed, bool isBonusWorld) {
        this.levels = levels;
        this.unlocked = unlocked;
        this.completed = completed;
        this.isBonusWorld = isBonusWorld;
    }

    public void Unlock() {
        unlocked = true;
    }

    public void CheckAndSetCompleted() {
        if(completed) { return; }

        Level[] completedLevels = levels.Where(level => level.challengeToken && level.speedRun).ToArray();

        completed = completedLevels.Length == levels.Length;
    }
}
