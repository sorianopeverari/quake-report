using QuakeReport.Domain.Repositories;
using QuakeReport.Domain.Models;
using QuakeMatch = QuakeReport.Domain.Models.Match;
using System.Text.RegularExpressions;
using Match = System.Text.RegularExpressions.Match;

namespace QuakeReport.Infra.FileRepository
{
    public class TournamentFileRepository : ITournamentRepository
    {
        private const string _pattern = @"(\d{1,2}:\d\d )(.*?)[:](.*)[^\n]*";
        private const string _path = @"../../qgames.log";

        private QuakeMatch? _currentMatch; 

        public async Task<Tournament> GetTournament()
        {
            Tournament tournament = new Tournament();
            
            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    while (sr.Peek() >= 0)
                    {
                        string? line = sr.ReadLine();

                        if (line == null) 
                        {
                            continue;
                        }

                        foreach (Match m in Regex.Matches(line, _pattern))
                        {
                            string type = m.Groups[2].ToString().Trim();
                            string time = m.Groups[1].ToString().Trim();
                            string content = m.Groups[3].ToString().Trim();

                            switch(type)
                            {
                                default:
                                    continue;
                                case "InitGame": this.InitGame(tournament, time);                                    
                                    break;
                                case "ClientUserinfoChanged": this.ClientUserInfoChanged(tournament, time, content);
                                    break;
                                case "Kill": this.Kill(tournament, time, content);
                                  break;
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

        private QuakeMatch InitGame(Tournament tournament, string time)
        {
            _currentMatch = tournament.GetCurrentMatch(time);
            return _currentMatch;
        }

        private void ClientUserInfoChanged(Tournament tournament, string time, string content)
        {
            Player p = new Player();
            p.Id = Convert.ToInt32(content.Substring(0, 1));
            
            int nameBegin = content.IndexOf("n\\");
			int nameEnd = content.IndexOf("\\t");

            p.NickName = content.Substring(nameBegin + 2, nameEnd - 4);
            _currentMatch?.AddInfoPlayer(p);
        }

        private void Kill(Tournament tournament, string time, string content)
        {
            int end = content.IndexOf(":");
            string ids = content.Substring(0, end);
            string[] idsKill = ids.Trim().Split(" ");
            Kill kill = new Kill(idsKill[0], idsKill[1], idsKill[2]);
            _currentMatch?.AddKill(kill);
        }
    }
}
