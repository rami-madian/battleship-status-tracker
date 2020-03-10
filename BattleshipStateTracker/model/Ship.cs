using System;
namespace BattleshipStateTracker.model
{
    public class Ship
    {
        public ShipLayoutEnum Layout { get; set; }
        public int ShipLength { get; set; }
        public string ShipSymbol { get; set; }

        public Ship()
        {
        }
    }
}
