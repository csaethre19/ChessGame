
using Microsoft.Maui.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Author: Daniel Kopta and ...
  Chess browser backend 
*/

namespace ChessBrowser
{
  internal class Queries
  {

    /// <summary>
    /// This function runs when the upload button is pressed.
    /// Given a filename, parses the PGN file, and uploads
    /// each chess game to the user's database.
    /// </summary>
    /// <param name="PGNfilename">The path to the PGN file</param>
    internal static async Task InsertGameData(string PGNfilename, MainPage mainPage)
    {
      // This will build a connection string to your user's database on atr,
      // assuimg you've typed a user and password in the GUI
      string connection = mainPage.GetConnectionString();

      // TODO:
      //       Load and parse the PGN file
      //       We recommend creating separate libraries to represent chess data and load the file

      // TODO:
      //       Use this to tell the GUI's progress bar how many total work steps there are
      //       For example, one iteration of your main upload loop could be one work step
      //mainPage.SetNumWorkItems( ... );


      using (MySqlConnection conn = new MySqlConnection(connection))
      {
        try
        {
          // Open a connection
          conn.Open();

          // TODO:
          //       iterate through your data and generate appropriate insert commands

          // TODO:
          //       Use this inside a loop to tell the GUI that one work step has completed:
          await mainPage.NotifyWorkItemCompleted();

        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine(e.Message);
        }
      }

    }


    /// <summary>
    /// Queries the database for games that match all the given filters.
    /// The filters are taken from the various controls in the GUI.
    /// </summary>
    /// <param name="white">The white player, or null if none</param>
    /// <param name="black">The black player, or null if none</param>
    /// <param name="opening">The first move, e.g. "1.e4", or null if none</param>
    /// <param name="winner">The winner as "W", "B", "D", or null if none</param>
    /// <param name="useDate">True if the filter includes a date range, False otherwise</param>
    /// <param name="start">The start of the date range</param>
    /// <param name="end">The end of the date range</param>
    /// <param name="showMoves">True if the returned data should include the PGN moves</param>
    /// <returns>A string separated by newlines containing the filtered games</returns>
    internal static string PerformQuery(string white, string black, string opening,
      string winner, bool useDate, DateTime start, DateTime end, bool showMoves,
      MainPage mainPage)
    {
      // This will build a connection string to your user's database on atr,
      // assuimg you've typed a user and password in the GUI
      string connection = mainPage.GetConnectionString();

      // Build up this string containing the results from your query
      string parsedResult = "";

      // Use this to count the number of rows returned by your query
      // (see below return statement)
      int numRows = 0;

      using (MySqlConnection conn = new MySqlConnection(connection))
      {
        try
        {
          conn.Open();

          MySqlCommand cmd = conn.CreateCommand();
          string dynamicSelect = "SELECT e.Name, e.Site, e.Date, " +
                                 "(SELECT Name FROM Players WHERE pID=g.WhitePlayer) as WhitePlayer, " +
                                 "(SELECT Name FROM Players WHERE pID=g.BlackPlayer) as BlackPlayer, " +
                                 "g.Result";
          if (showMoves)
          {
             dynamicSelect += ", g.Moves";
          }
          dynamicSelect += " FROM Games g NATURAL JOIN Events e";

          List<string> whereParts = new List<string>();
          if (white != null)
          {
            cmd.Parameters.AddWithValue("@white", white);
            whereParts.Add("g.WhitePlayer=(SELECT pID FROM Players WHERE Name=@white limit 1)");
          }
          if (black != null)
          {
            cmd.Parameters.AddWithValue("@black", black);
            whereParts.Add("g.BlackPlayer=(SELECT pID FROM Players WHERE Name=@black limit 1)");
          }
          if (opening != null)
          {
            cmd.Parameters.AddWithValue("@opening", opening);
            whereParts.Add("g.Moves like '@opening%'");
          }
          if (winner != null)
          {
            cmd.Parameters.AddWithValue("@winner", winner);
            whereParts.Add("g.Result=@winner");
          }
          if (useDate)
          {
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            whereParts.Add("e.Date >= @start AND e.Date <= @end");
          }

          if (whereParts.Count != 0)
          { 
            dynamicSelect += " WHERE " + whereParts[0];
          }

          for (int i = 1; i < whereParts.Count; i++) {
            dynamicSelect += " AND " + whereParts[i];
          }

          dynamicSelect += ";";


          System.Diagnostics.Debug.WriteLine("Dynamic Select: " + dynamicSelect);

          cmd.CommandText = dynamicSelect;

          using (MySqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              numRows += 1;
              parsedResult += "Event: " + reader["Name"] + "\n" +
                  "Site: " + reader["Site"] + "\n" +
                  "Date: " + reader["Date"] + "\n" +
                  "White: " + reader["WhitePlayer"] + "\n" +
                  "Black: " + reader["BlackPlayer"] + "\n" +
                  "Result: " + reader["Result"] + "\n";
              if (showMoves)
              {
                parsedResult += reader["Moves"] + "\n";
              }
              parsedResult += "\n";
            }
          }
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine(e.Message);
        }
      }

      return numRows + " results\n\n" + parsedResult;
    }

  }
}
