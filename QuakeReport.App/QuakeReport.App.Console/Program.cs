using QuakeReport.App.Parser;
using QuakeReport.Domain.Business;
using QuakeReport.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using QuakeReport.Infra.FileRepository;
using QuakeReport.Domain.Repositories;

ServiceProvider serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<ITournamentRepository, TournamentFileRepository>()
    .AddSingleton<TournamentBusiness>()
    .AddSingleton<GameBusiness>()
    .BuildServiceProvider();

TournamentBusiness tournamentBusiness = serviceProvider.GetRequiredService<TournamentBusiness>(); 
Tournament tournament = await tournamentBusiness.GetAssync();

GameBusiness gameBusiness = serviceProvider.GetRequiredService<GameBusiness>();
gameBusiness.SumScore(tournament);

Console.WriteLine(GameKillByPlayerJson.Write(tournament));
Console.Write(GameDeathCauseJson.Write(tournament));