using System;
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

            string Event, Site, Round, WhitePlayer, BlackPlayer, Result;
            int WhiteElo, BlackElo;
            DateTime EventDate;

            foreach (string line in lines)
            {
                string[] parts = line.Split(" ");
                switch (parts[0])
                {
                    case "[Event":
                        break;
                    case "[Site":
                        break;
                    case "[Date":
                        break;
                    case "[Round":
                        break;
                    case "[White":
                        break;
                    case "[Black":
                        break;
                    case "[Result":
                        break;
                    case "[WhiteElo":
                        break;
                    case "BlackElo":
                        break;
                    case "[EventDate":
                        break;
                    default:
                        break;
                }
            }

            return games;
        }
    }
}
