using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

/*
  Author: Charlotte Saethre, and Jackson Linford
  Helper tool to read PGN files for ChessBrowser Project.   
 */
namespace ChessBrowserLib
{
    public static class PgnReader
    {

        /// <summary>
        /// 
        /// Reads PGN file and parses out the following information:
        /// - Event name
        /// - Site name
        /// - WhitePlayer
        /// - BlackPlayer
        /// - Result of game
        /// - Moves of game
        /// - Elos of both white and black players
        /// This information is used to construct ChessGame objects and inserted into a list.
        /// 
        /// </summary>
        /// <param name="path">Path to PGN file to read.</param>
        /// <returns>List of ChessGame objects that were parsed out of PGN file.</returns>
        public static List<ChessGame> ReadPgnFile(string path)
        {
            List<ChessGame> games = new List<ChessGame>();
            string[] lines = File.ReadAllLines(path);

            var linesEnumerator = ((IEnumerable<string>)lines).GetEnumerator();
            if (linesEnumerator == null ) { return games; }

            bool hasNext = true;

            while (linesEnumerator.MoveNext())
            {
                string Event = "";
                string Site = "";
                string Round = "";
                string WhitePlayer = "";
                string BlackPlayer = "";
                string Result = "";
                string Moves = "";
                int WhiteElo = 0;
                int BlackElo = 0;
                string EventDate = "";

                do
                {
                    string line = linesEnumerator.Current;

                    if (line == "") break;

                    string[] parts = line.Split(" ");
                    switch (parts[0])
                    {
                        case "[Event":
                            Event = line.Substring(8, line.Length - 2 - 8);
                            break;
                        case "[Site":
                            Site = line.Substring(7, line.Length - 2 - 7);
                            break;
                        case "[Round":
                            Round = line.Substring(8, line.Length - 2 - 8);
                            break;
                        case "[White":
                            WhitePlayer = line.Substring(8, line.Length - 2 - 8);
                            break;
                        case "[Black":
                            BlackPlayer = line.Substring(8, line.Length - 2 - 8);
                            break;
                        case "[Result":
                            Result = (line.Substring(9, line.Length - 2 - 9)) switch
                            {
                                "1-0" => "W",
                                "0-1" => "B",
                                _ => "D"
                            };
                            break;
                        case "[WhiteElo":
                            int.TryParse(line.Substring(11, line.Length - 2 - 11), out WhiteElo);
                            break;
                        case "[BlackElo":
                            int.TryParse(line.Substring(11, line.Length - 2 - 11), out BlackElo);
                            break;
                        case "[EventDate":
                            EventDate = line.Substring(12, line.Length - 2 - 12);
                            //DateTime.TryParse(line.Substring(12, line.Length - 2 - 12), out EventDate);
                            break;
                        default:
                            break;
                    }
                } while (linesEnumerator.MoveNext());

                StringBuilder sb = new StringBuilder();
                while (linesEnumerator.MoveNext())
                {
                    string line = linesEnumerator.Current;
                    if (line == "") break;
                    
                    sb.Append(line);
                }
                Moves = sb.ToString();

                ChessGame game = new ChessGame(Event, Site, Round, WhitePlayer, BlackPlayer, WhiteElo, BlackElo, Result, EventDate, Moves);
                games.Add(game);
            }

            return games;
        }
    }
}
