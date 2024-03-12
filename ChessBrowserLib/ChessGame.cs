using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBrowserLib
{
    public class ChessGame
    {
        public string Event {  get; set; }
        public string Site {  get; set; }
        public string Round {  get; set; }
        public string WhitePlayer { get; set; }
        public string BlackPlayer { get; set; }
        public int WhiteElo { get; set; }
        public int BlackElo { get; set; }
        public string Result { get; set; }
        public DateTime EventDate { get; set; }
        public string Moves { get; set; }

        public ChessGame(string Event, string Site, string Round, 
            string WhitePlayer, string BlackPlayer, int WhiteElo, 
            int BlackElo, string Result, DateTime EventDate, string Moves)
        {
            this.Event = Event;
            this.Site = Site;
            this.Round = Round;
            this.WhitePlayer = WhitePlayer;
            this.BlackPlayer = BlackPlayer;
            this.WhiteElo = WhiteElo;
            this.BlackElo = BlackElo;
            this.Result = Result;
            this.EventDate = EventDate;
            this.Moves = Moves;
        }

    }
}
