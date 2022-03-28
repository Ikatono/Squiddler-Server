namespace Squiddler.Server
{
    public class Player
    {
        public readonly Hand _Hand = new Hand();
        public readonly Guid GUID = Guid.NewGuid();
        public readonly Guid GameGuid;
        public string Name { get; private set; }
        public int Score { get; private set; }
        public Player(string name, Guid gameGuid)
        {
            Name = name;
            GameGuid = gameGuid;
        }
    }
}