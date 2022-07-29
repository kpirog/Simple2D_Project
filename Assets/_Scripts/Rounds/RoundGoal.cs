public class RoundGoal 
{
    public int MushroomsToKill { get; }
    public int GoblinsToKill { get; }
    public int GoldToCollect { get; }

    public RoundGoal(int MushroomsToKill, int GoblinsToKill, int GoldToCollect)
    {
        this.MushroomsToKill = MushroomsToKill;
        this.GoblinsToKill = GoblinsToKill;
        this.GoldToCollect = GoldToCollect;
    }
}
