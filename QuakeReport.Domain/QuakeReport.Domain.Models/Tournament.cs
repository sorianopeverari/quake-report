namespace QuakeReport.Domain.Models
{
    public class Tournament
    {
        public string Id { get; set; } = String.Empty;
        public SortedDictionary<int, Match> Matches { get; } = new SortedDictionary<int, Match>();
        private int _lastMatchNumber;

        public Tournament()
        {
        }

        public Tournament(string id)
        {
            this.Id = id;
        }

        public Match GetCurrentMatch(string time)
        {
            int seconds = this.TimeToInt(time);

            if(_lastMatchNumber == 0)
            {
                Match m = new Match(seconds);
                this.Matches.Add(m.Number, m);
                _lastMatchNumber = m.Number;
                return this.Matches[_lastMatchNumber];
            }
            
            return this.GetCurrentMatch(seconds);
        }

        private Match GetCurrentMatch(int time)
        {
            Match m;

            if(this.Matches[_lastMatchNumber].LastEvent > time)
            {
                m = new Match(time) { Number = this.Matches[_lastMatchNumber].Number + 1 };
                this.Matches.Add(m.Number, m);
            }
            else
            {
                this.Matches[_lastMatchNumber].Level++;
                m = this.Matches[_lastMatchNumber]; 
            }

            _lastMatchNumber = m.Number;
            this.Matches[_lastMatchNumber].LastEvent = time;
            return m;
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
