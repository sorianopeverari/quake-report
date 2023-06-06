using QuakeReport.Domain.Models;

namespace QuakeReport.Domain.Repositories
{
    public interface ITournamentRepository
    {
        Task<Tournament> GetTournament();
    }
}
