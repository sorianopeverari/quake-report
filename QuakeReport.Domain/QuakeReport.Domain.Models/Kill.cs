namespace QuakeReport.Domain.Models
{
    public class Kill
    {
        public int Level { get; set; }
        public int Killer { get; set; }
        public int Killed { get; set; }
        public int DeathCause { get; set; }

        public Kill()
        {            
        }

        public Kill(string killer, string killed, string deathCause)
        {
            this.Killer = Convert.ToInt32(killer);
            this.Killed = Convert.ToInt32(killed);
            this.DeathCause = Convert.ToInt32(deathCause);
        }
    }
}

