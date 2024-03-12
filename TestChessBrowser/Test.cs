using ChessBrowserLib;

List<ChessGame> games = PgnReader.readPgnFile("C:/Users/charl/OneDrive/School/CS 5530/HW6/kbtest.pgn");

foreach (ChessGame game in games)
{
    Console.WriteLine(game.Event);
}







