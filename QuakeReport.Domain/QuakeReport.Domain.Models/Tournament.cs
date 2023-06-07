using QuakeReport.Domain.Models.Exceptions;

namespace QuakeReport.Domain.Models
{
    public class Tournament
    {
        public string Id { get; set; } = String.Empty;
        public IList<Game> Games { get; } = new List<Game>();
        public Game? CurrentGame { get; set; }

        public Game BeginGame(int beginSeconds)
        {
            if(this.CurrentGame == null)
            {
                Game game = new Game();
                this.Games.Add(game);
                this.CurrentGame = game;
                return this.CurrentGame;
            }

            if(beginSeconds > this.CurrentGame.LastEndGameSeconds)
            {
                this.CurrentGame.Level++;
                return this.CurrentGame;
            }
            else
            {
                Game game = new Game();
                game.Number = this.CurrentGame.Number + 1;
                this.Games.Add(game);
                return this.CurrentGame = game;
            }
        }

        public void EndGame(int seconds)
        {
            if(this.CurrentGame == null)
            {
                throw new InternalErrorException("Current game is null");
            }

            this.CurrentGame.LastEndGameSeconds = seconds;
        }
    }
}