using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Author: Charlotte Saethre, and Jackson Linford
  Data class to hold Chess Game information.  
 */

namespace ChessBrowserLib
{
    /// <summary>
    /// A ChessGame object has the following information:
    /// Event, Site, Round, WhitePlayer, BlackPlayer, WhiteElo, BlackElo, Result, EventDate, and Moves.
    /// The fields of this class all contain getters and setters.
    /// </summary>
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
        public string EventDate { get; set; }
        public string Moves { get; set; }

        /// <summary>
        /// Constructs a ChessGame object.
        /// </summary>
        /// <param name="Event">Event name of game</param>
        /// <param name="Site">Site at which game took place</param>
        /// <param name="Round">Round of game</param>
        /// <param name="WhitePlayer">Name of White player in game</param>
        /// <param name="BlackPlayer">Name of Black player in game</param>
        /// <param name="WhiteElo">Elo score of the white player</param>
        /// <param name="BlackElo">Elo score of the black player</param>
        /// <param name="Result">Result of the game</param>
        /// <param name="EventDate">Date at which the event took place</param>
        /// <param name="Moves">List of moves for the game</param>
        public ChessGame(string Event, string Site, string Round, 
            string WhitePlayer, string BlackPlayer, int WhiteElo, 
            int BlackElo, string Result, string EventDate, string Moves)
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
