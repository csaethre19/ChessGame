using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessBrowserLib
{
    public static class PgnReader
    {
        public static List<ChessGame> readPgnFile(string path)
        {
            List<ChessGame> games = new List<ChessGame>();
            string[] lines = File.ReadAllLines(path);

            var linesEnumerator = ((IEnumerable<string>)lines).GetEnumerator();
            if (linesEnumerator == null ) { return games; }

            bool hasNext = true;

            while (hasNext)
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
                DateTime EventDate = default(DateTime);

                while (hasNext)
                {
                    hasNext = linesEnumerator.MoveNext();
                    string line = linesEnumerator.Current;
                    if (line == "") break;
                    string[] parts = line.Split(" ");
                    Console.WriteLine(parts[0]);
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
                            Result = line.Substring(9, line.Length - 2 - 9);
                            break;
                        case "[WhiteElo":
                            int.TryParse(line.Substring(11, line.Length - 2 - 11), out WhiteElo);
                            break;
                        case "[BlackElo":
                            int.TryParse(line.Substring(11, line.Length - 2 - 11), out BlackElo);
                            break;
                        case "[EventDate":
                            DateTime.TryParse(line.Substring(12, line.Length - 2 - 12), out EventDate);
                            break;
                        default:
                            break;
                    }
                }

                StringBuilder sb = new StringBuilder();
                while (hasNext)
                {
                    hasNext = linesEnumerator.MoveNext();
                    if (!hasNext) break;
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
