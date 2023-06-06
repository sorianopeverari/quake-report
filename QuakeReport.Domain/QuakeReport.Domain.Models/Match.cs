namespace QuakeReport.Domain.Models
{
    public class Match
    {
        private readonly int _world = 1022;

        public int Number { get; set; } = 1;

        public int Level { get; set; } = 1;

        public SortedDictionary<int, Player> Players { get; } = new SortedDictionary<int, Player>();
    
        public SortedDictionary<int, int> PlayerKiller { get; } = new SortedDictionary<int, int>();
        
        public SortedDictionary<int, int> DeathCause { get; } = new SortedDictionary<int, int>();
    
        public IList<Kill> Kills { get; } = new List<Kill>();

        public int LastEvent { get; set; }

        public Match()
        {
        }

        public Match(int time)
        {
            this.LastEvent = time;
        }

        public void AddInfoPlayer(Player player)
        {
            if(this.Players.ContainsKey(player.Id))
            {
                this.Players[player.Id].NickName = player.NickName;
            }
            else
            {
                this.Players.Add(player.Id, player);
            }
        }

        public void AddKill(Kill kill)
        {
            kill.Level = this.Level;
            this.Kills.Add(kill);

            if(kill.Killer != this._world)
            {
                this.IncrementPlayerKiller(kill);
            }
            else
            {
                this.DecrementPlayerKilled(kill);
            }
            
            this.IncrementDeathCause(kill);
        }

        private void IncrementPlayerKiller(Kill kill)
        {            
            if(!this.PlayerKiller.ContainsKey(kill.Killer))
            {
                this.PlayerKiller.Add(kill.Killer, 0);
            }

            this.PlayerKiller[kill.Killer]++;
        }

        private void DecrementPlayerKilled(Kill kill)
        {
            if(!this.PlayerKiller.ContainsKey(kill.Killed))
            {
                this.PlayerKiller.Add(kill.Killed, 0);
            }
            this.PlayerKiller[kill.Killed]--;
        }

        private void IncrementDeathCause(Kill kill)
        {
            if(!this.DeathCause.ContainsKey(kill.DeathCause))
            {
                this.DeathCause.Add(kill.DeathCause, 0);
            }

            this.DeathCause[kill.DeathCause]++;
        }
    }
}