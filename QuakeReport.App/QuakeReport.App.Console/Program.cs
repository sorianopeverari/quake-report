using QuakeReport.App.Parser;
using QuakeReport.Domain.Business;
using QuakeReport.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using QuakeReport.Infra.FileRepository;

// TODO DI
TournamentBusiness tb = new TournamentBusiness(new TournamentFileRepository()); 
Tournament t = await tb.GetTournament();

Console.WriteLine(MatchKillJson.Write(t));
Console.WriteLine("");
Console.Write(MatchDeathCauseJson.Write(t));
Console.WriteLine("");
