using QuakeReport.Domain.Repositories;
using QuakeReport.Domain.Models;
using System.Text.RegularExpressions;
using QuakeReport.Domain.Models.Exceptions;

namespace QuakeReport.Infra.FileRepository
{
    public class TournamentFileRepository : ITournamentRepository
    {
        private const string _patternEvent = @"(\d{1,2}:\d\d )(.*?)[:](.*)[^\n]*";
        
        private const string _path = @"qgames.log";

        public async Task<Tournament> GetAsync()
        {
            Regex expression = new Regex(_patternEvent);

            Tournament tournament = new Tournament();
            
            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    int numberLine = 0;
                    while (sr.Peek() >= 0)
                    {
                        numberLine++;
                        string? dataLine = sr.ReadLine();

                        if (String.IsNullOrWhiteSpace(dataLine)) 
                        {
                            Console.WriteLine(String.Format("Line {0} is empty"));
                            continue;
                        }

                        Match m = expression.Match(dataLine);

                        if(m.Success)
                        {
                            string type = m.Groups[2].ToString().Trim();
                            string time = m.Groups[1].ToString().Trim();
                            string content = m.Groups[3].ToString().Trim();

                            try
                            {
                                switch(type)
                                {
                                    default:
                                        continue;
                                    case "InitGame":
                                    this.BeginGame(tournament, time);
                                    break;
                                    case "ShutdownGame": 
                                    this.EndGame(tournament, time);
                                    break;
                                    case "ClientUserinfoChanged": 
                                        this.SaveInfoPlayer(tournament, content);
                                    break;
                                    case "Kill": 
                                        this.Kill(tournament, content);
                                    break;
                                }
                            }
                            catch(Exception ex)
                            {
                                throw new ValidationException(String.Format("Line {0} - Invalid format)", numberLine), ex);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
            }

            return await Task.FromResult(tournament);
        }

        private void BeginGame(Tournament tournament, string time)
        {
            tournament.BeginGame(TimeToInt(time));
        }

        private void EndGame(Tournament tournament, string time)
        {
            tournament.EndGame(TimeToInt(time));
        }

        private void SaveInfoPlayer(Tournament tournament, string content)
        {
            if(tournament.CurrentGame == null)
            {
                throw new InternalErrorException("Current game is null");
            }

            tournament.CurrentGame.SaveInfoPlayer(this.ToPlayer(content));
        }

        private Player ToPlayer(string content)
        {
            Player player = new Player();
            player.Id = Convert.ToInt32(content.Substring(0, 1));
            int nameBegin = content.IndexOf("n\\");
			int nameEnd = content.IndexOf("\\t");
            player.NickName = content.Substring(nameBegin + 2, nameEnd - 4);
            return player;
        }

        private void Kill(Tournament tournament, string content)
        {
            int end = content.IndexOf(":");
            string ids = content.Substring(0, end);
            string[] idsKill = ids.Trim().Split(" ");

            if(tournament.CurrentGame == null)
            {
                throw new InternalErrorException("Current game is null");
            }

            tournament.CurrentGame.AddKill(idsKill[0], idsKill[1], idsKill[2]);
        }

        public int TimeToInt(string time)
        {
            string[] minutesSeconds = time.Trim().Split(":");
            int seconds = Convert.ToInt32(minutesSeconds[0])*60;
            seconds =+ Convert.ToInt32(minutesSeconds[1]);
            return seconds;
        }
    }
}