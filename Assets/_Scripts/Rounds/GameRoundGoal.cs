[System.Serializable]
public class GameRoundGoal 
{
    public int MushroomsToKill { get; }
    public int GoblinsToKill { get; }
    public int GoldToCollect { get; }

    public GameRoundGoal(int MushroomsToKill, int GoblinsToKill, int GoldToCollect)
    {
        this.MushroomsToKill = MushroomsToKill;
        this.GoblinsToKill = GoblinsToKill;
        this.GoldToCollect = GoldToCollect;
    }
}
