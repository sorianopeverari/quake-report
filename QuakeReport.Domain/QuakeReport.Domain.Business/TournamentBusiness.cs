﻿using QuakeReport.Domain.Models;
using QuakeReport.Domain.Repositories;
using QuakeReport.Infra.FileRepository;

namespace QuakeReport.Domain.Business
{
    public class TournamentBusiness
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentBusiness()
        {
            _tournamentRepository = new TournamentFileRepository();
        }

        public async Task<Tournament> GetTournament()
        {
            return await _tournamentRepository.GetTournament();
        }
    }
}