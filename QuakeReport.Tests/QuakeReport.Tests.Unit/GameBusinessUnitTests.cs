using Xunit;
using QuakeReport.Domain.Models;
using QuakeReport.Domain.Business;
using QuakeReport.Domain.Models.Enums;

namespace QuakeReport.Tests.Unit;

public class GameUnitTests
{
    [Theory]
    [InlineData(2, 3, 6, 12)]
    [InlineData(3, 2, 6, 12)]
    [InlineData(1022, 2, 22, 12)]
    public void SumScore_ReturnExpectedKillScore(int killerPlayerId, 
                                                         int killedPlayerId,
                                                         int deathCause,
                                                         int expected)
    {
        GameBusiness arrangeGameBusiness = this.SetupDefaultGameBusiness();
        Game fakeGame = this.SetupFakeGame();
        fakeGame.AddKill(killerPlayerId, killedPlayerId, deathCause);

        arrangeGameBusiness.SumScore(fakeGame);
        int actual = fakeGame.KillScore;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2, 3, 6, 2, -4)]
    [InlineData(3, 2, 6, 3, 1)]
    [InlineData(1022, 3, 22, 3, -1)]
    [InlineData(1022, 2, 22, 2, -6)]
    public void SumScore_ReturnExpectedKillScoreByPlayerId(int killerPlayerId, 
                                                         int killedPlayerId,
                                                         int deathCause,
                                                         int playerId,
                                                         int expected)
    {
        GameBusiness arrangeGameBusiness = this.SetupDefaultGameBusiness();
        Game fakeGame = this.SetupFakeGame();
        fakeGame.AddKill(killerPlayerId, killedPlayerId, deathCause);

        arrangeGameBusiness.SumScore(fakeGame);
        int actual = fakeGame.PlayersById[playerId].KillScore;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2, 3, 7, 4)]
    [InlineData(3, 2, 19, 2)]
    public void SumScore_ReturnExpectedDeathCauseScore(int killerPlayerId, 
                                                         int killedPlayerId,
                                                         int deathCause,
                                                         int expected)
    {
        GameBusiness arrangeGameBusiness = this.SetupDefaultGameBusiness();
        Game fakeGame = this.SetupFakeGame();
        fakeGame.AddKill(killerPlayerId, killedPlayerId, deathCause);

        arrangeGameBusiness.SumScore(fakeGame);
        int actual = fakeGame.DeathCauseScore[(DeathCause)deathCause];
        Assert.Equal(expected, actual);
    }

    private GameBusiness SetupDefaultGameBusiness()
    {
        return new GameBusiness();
    }

    private Game SetupFakeGame()
    {
        Game game = new Game();
        game.SaveInfoPlayer(new Player(2, "Isgalamido"));
        game.SaveInfoPlayer(new Player(2, "Isgalamido"));
        game.Level++;
        game.SaveInfoPlayer(new Player(2, "Isgalamido"));
        game.AddKill(1022, 2, 22);
        game.AddKill(1022, 2, 22);
        game.SaveInfoPlayer(new Player(2, "Isgalamido"));
        game.SaveInfoPlayer(new Player(2, "Isgalamido"));
        game.AddKill(1022, 2, 22);
        game.SaveInfoPlayer(new Player(3, "Dono da Bola"));
        game.SaveInfoPlayer(new Player(3, "Mocinha"));
        game.AddKill(2, 3, 7);
        game.AddKill(2, 2, 7);
        game.AddKill(2, 2, 7);
        game.AddKill(1022, 2, 22);
        game.AddKill(1022, 2, 22);
        game.AddKill(1022, 2, 22);
        game.AddKill(1022, 2, 19);
        game.AddKill(1022, 2, 22);
        return game;
    }
}
