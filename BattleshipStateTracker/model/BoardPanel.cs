using System;
namespace BattleshipStateTracker.model
{
    public class BoardPanel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Occupation{ get; set; }
        public bool Hit { get; set; }

        public BoardPanel()
        {
        }

    }
}
