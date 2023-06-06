using QuakeReport.Domain.Models;
using QuakeReport.Domain.Repositories;

namespace QuakeReport.Domain.Business
{
    public class TournamentBusiness
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentBusiness(ITournamentRepository tournamentRepository)
        {
            this._tournamentRepository = tournamentRepository;
        }

        public async Task<Tournament> GetAssync()
        {
            Tournament tournament = await _tournamentRepository.GetAsync();
            return tournament;
        }
    }
}
