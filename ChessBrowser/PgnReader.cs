using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessBrowser
{
    internal static class PgnReader
    {
        static List<ChessGame> readPgnFile(string path)
        {
            List<ChessGame> games = new List<ChessGame>();
            string[] lines = File.ReadAllLines(path);
            var linesEnumerator = lines.GetEnumerator() as IEnumerator<string>;

            while (linesEnumerator.MoveNext())
            {
                string Event, Site, Round, WhitePlayer, BlackPlayer, Result;
                int WhiteElo, BlackElo;
                DateTime EventDate;

                for (string line = linesEnumerator.Current; line != "\n"; linesEnumerator.MoveNext()) 
                {
                    string[] parts = line.Split(" ");
                    switch (parts[0])
                    {
                        case "[Event":
                            Event = line.Substring(8, line.Length - 2);
                            break;
                        case "[Site":
                            Site = line.Substring(7, line.Length - 2);
                            break;
                        case "[Round":
                            Round = line.Substring(8, line.Length - 2);
                            break;
                        case "[White":
                            WhitePlayer = line.Substring(8, line.Length - 2);
                            break;
                        case "[Black":
                            BlackPlayer = line.Substring(8, line.Length - 2);
                            break;
                        case "[Result":
                            Result = line.Substring(9, line.Length - 2);
                            break;
                        case "[WhiteElo":
                            int.TryParse(line.Substring(11, line.Length - 2), out WhiteElo);
                            break;
                        case "[BlackElo":
                            int.TryParse(line.Substring(11, line.Length - 2), out BlackElo);
                            break;
                        case "[EventDate":
                            DateTime.TryParse(line.Substring(12, line.Length - 2), out EventDate);
                            break;
                        default:
                            break;
                    }
                }
                
                
            }

            return games;
        }
    }
}
