namespace QuakeReport.Domain.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string NickName { get; set; } = String.Empty;

        public bool Hidden { get; set; }

        private int _killScore = 0;

        public Player()
        {

        }

        public Player(int id)
        {
            this.Id = id;
        }

        public Player(int id, bool hidden)
        {
            this.Id = id;
            this.Hidden = hidden;
        }

        public Player(int id, string nickName)
        {
            this.Id = id;
            this.NickName = nickName;
        }

        public int KillScore
        { 
            get { return this._killScore; }
        }
        
        public void AddKillScore(int point)
        {
            this._killScore += point;
        }
    }
}

