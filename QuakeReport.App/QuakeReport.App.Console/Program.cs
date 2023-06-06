using QuakeReport.App.Parser;
using QuakeReport.Domain.Business;
using QuakeReport.Domain.Models;

TournamentBusiness tb = new TournamentBusiness();
Tournament t = await tb.GetTournament();

Console.WriteLine(MatchKillJson.Write(t));
Console.WriteLine("");
Console.Write(MatchDeathCauseJson.Write(t));
Console.WriteLine("");