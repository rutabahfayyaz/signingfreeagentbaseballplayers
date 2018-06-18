using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigningFreeAgentPlayers
{
    class CreatePlayer
    {
        
        public int VORP { get; set; }
        public int cost { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public CreatePlayer()
        {
        }
        public CreatePlayer(String name,int VORP,int cost) {
            this.name = name;
            this.VORP = VORP;
            this.cost = cost;
        }
        override public String ToString() {
            return "Player Name: " + name + " VORP:" + Convert.ToString(VORP) + " Cost:" + Convert.ToString(cost);
        }
        public void print() {
            Console.WriteLine("Player Name: " + name + " VORP:" + Convert.ToString(VORP) + " Cost:" + Convert.ToString(cost));
        }
    }
}
