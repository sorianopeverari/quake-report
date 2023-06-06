using QuakeReport.Domain.Models;
using QuakeReport.Domain.Models.Exceptions;

namespace QuakeReport.Domain.Business
{
    public class GameBusiness
    {
        private readonly int _pointsToKiller = 1;
        private readonly int _pointsToKilledFromWorldPlayer = -1;
        private readonly int _worldPlayerId = 1022;
        private readonly int _pointsToDeathCause = 1;

        public GameBusiness()
        {
        }

        public void SumScore(Tournament tournament)
        {
            foreach(Game game in tournament.Games)
            {
                this.SumScore(game);
            }
        }

        public void SumScore(Game game)
        {
            foreach(Kill kill in game.Kills)
            {
                this.SumPlayerKillScore(game, kill);
                this.SumDeathCauseScore(game, kill);
                this.SumTotalKill(game);
            }
        }

        private void SumDeathCauseScore(Game game, Kill kill)
        {
            game.AddDeathCauseScore(kill.DeathCause, _pointsToDeathCause);
        }

        private void SumPlayerKillScore(Game game, Kill kill)
        {
            if(kill.Killer == null || kill.Killed == null)
            {
                throw new InternalErrorException("Killer or Killed is null");
            }

            if(kill.Killer.Id == _worldPlayerId)
            {
                kill.Killed.AddKillScore(_pointsToKilledFromWorldPlayer);
            }
            else
            {
                kill.Killer.AddKillScore(_pointsToKiller);
            }
        }

        private void SumTotalKill(Game game)
        {
            game.KillScore++;
        }
    }
}
