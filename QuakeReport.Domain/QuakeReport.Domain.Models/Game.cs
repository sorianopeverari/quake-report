using QuakeReport.Domain.Models.Enums;

namespace QuakeReport.Domain.Models
{
    public class Game
    {
        private readonly int _worldPlayerId = 1022;

        public int Number { get; set; } = 1;

        public int Level { get; set; } = 1;

        public int LastEndGameSeconds { get; set; } = 0;

        public SortedDictionary<int, Player> PlayersById { get; } = new SortedDictionary<int, Player>();
    
        public IList<Kill> Kills { get; } = new List<Kill>();

        private SortedDictionary<DeathCause, int> _deathCauseScore = new SortedDictionary<DeathCause, int>();
        
        public SortedDictionary<DeathCause, int> DeathCauseScore
        {
            get { return _deathCauseScore; }
        }

        public int KillScore { get; set; }

        public Game()
        {
            this.PlayersById.Add(_worldPlayerId, new Player(_worldPlayerId, true));
        }

        public void AddDeathCauseScore(DeathCause deathCause, int point)
        {
            if(!_deathCauseScore.ContainsKey(deathCause))
            {
                _deathCauseScore.Add(deathCause, 0);
            }
            _deathCauseScore[deathCause] += point;
        }

        public void SaveInfoPlayer(Player player)
        {
            if(this.PlayersById.ContainsKey(player.Id))
            {
                this.PlayersById[player.Id].NickName = player.NickName;
            }
            else
            {
                this.PlayersById.Add(player.Id, player);
            }
        }

        private void AddKill(Kill kill)
        {
            this.Kills.Add(kill);
        }

        public void AddKill(string killer, string killed, string deathCause)
        {
            this.AddKill(Convert.ToInt32(killer), 
                         Convert.ToInt32(killed),
                         Convert.ToInt32(deathCause));
        }

        public void AddKill(int killerPlayerId, int killedPlayerId, int deathCause)
        {            
            Kill kill = new Kill();
            kill.Killer = PlayersById[killerPlayerId];
            kill.Killed = PlayersById[killedPlayerId];
            kill.DeathCause = (DeathCause)deathCause;
            this.AddKill(kill);
        }
    }
}