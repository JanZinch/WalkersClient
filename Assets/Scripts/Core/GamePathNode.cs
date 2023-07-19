using System.Collections.Generic;

namespace Core
{
    public class GamePathNode
    {
        public int Index { get; private set; }
        public int BypassTargetIndex { get; private set; }
        public long Bonus { get; private set; }
        public LinkedList<Player> Players { get; private set; }

        public const int EmptyIndex = -1;
    
        public GamePathNode(int index, int bypassTargetIndex, long bonus)
        {
            Index = index;
            BypassTargetIndex = bypassTargetIndex;
            Bonus = bonus;
            Players = new LinkedList<Player>();
        }

        public void SetPlayer(Player player)
        {
            Players.AddLast(player);
        }
    
        public void UnsetPlayer(Player player)
        {
            Players.Remove(player);
        }

    }
}

