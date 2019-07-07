
using System.Collections.Generic;

namespace BattleShipGame.BoardObjects
{
    public class GameBoard
    {
        public List<GamePanel> Panels { get; set; }

        public GameBoard()
        {
            Panels = new List<GamePanel>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Panels.Add(new GamePanel(i, j));
                }
            }
        }
    }
}
