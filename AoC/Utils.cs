using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace AoC
{
    public class Utils
    {
        public static string LoadInput(string filename = "input.txt")
        {
            var year = Program.Config.Year;
            var day = Program.Config.Day;
            
            var inputFilepath = Path.GetFullPath(Path.Combine(Program.Config.ProjectPath, $"./{year}/Day{day:D2}/{filename}"));
            var inputUrl = $"https://adventofcode.com/{year}/day/{day}/input";
            var input = "INPUT NOT AVAILABLE";

            if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
            {
                input = File.ReadAllText(inputFilepath);
            }
            else
            {
                var currentEasternStandardTime = DateTime.UtcNow - TimeSpan.FromHours(5);
                if (currentEasternStandardTime >= new DateTime(year, 12, day))
                {
                    try
                    {
                        using var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("Cookie", $"session={Program.Config.SessionCookie}");
                        input = client.GetStringAsync(inputUrl).GetAwaiter().GetResult();
                        File.WriteAllText(inputFilepath, input);
                    }
                    catch (WebException e)
                    {
                        var statusCode = ((HttpWebResponse) e.Response).StatusCode;
                        switch (statusCode)
                        {
                            case HttpStatusCode.BadRequest:
                                Console.WriteLine(
                                    $"Day {day}: Error code 400 when attempting to retrieve puzzle input through the web client. Your session cookie is probably not recognized.");
                                break;
                            case HttpStatusCode.NotFound:
                                Console.WriteLine(
                                    $"Day {day}: Error code 404 when attempting to retrieve puzzle input through the web client. The puzzle is probably not available yet.");
                                break;
                            default:
                                Console.WriteLine(e.ToString());
                                break;
                        }
                    }
                }
                else
                {
                    var eta = new DateTime(year, 12, day) - currentEasternStandardTime;
                    var message = $"Puzzle is not yet open! ETA: {(int)eta.TotalHours:D2}:{eta.Minutes:D2}:{eta.Seconds:D2}";
                    Console.Error.WriteLine(message);
                    throw new Exception(message);
                }
            }

            return input;
        }

        public static IEnumerable<string> LoadInputLines(string filename = "input.txt")
        {
            return LoadInput(filename).Split("\n");
        }
    }
}
