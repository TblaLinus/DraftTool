namespace DraftTool.UI.Service
{
    public interface IGameEngine
    {
        int NumberOfRounds { get; set; }
        int NumberOfPlayers { get; set; }
        int NumberOfCards { get; set; }
    }
}
