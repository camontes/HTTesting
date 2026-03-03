using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EvidenceCoexistenceCommitteeVoteRepository(ApplicationDbContext context) : IEvidenceCoexistenceCommitteeVoteRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EvidenceCoexistenceCommitteeVote EvidenceCoexistenceCommitteeVote) => _context.EvidenceCoexistenceCommitteeVotes.Add(EvidenceCoexistenceCommitteeVote);
        public void AddRange(List<EvidenceCoexistenceCommitteeVote> EvidenceCoexistenceCommitteeVotes) => _context.EvidenceCoexistenceCommitteeVotes.AddRange(EvidenceCoexistenceCommitteeVotes);

        public void Delete(EvidenceCoexistenceCommitteeVote EvidenceCoexistenceCommitteeVote) => _context.EvidenceCoexistenceCommitteeVotes.Remove(EvidenceCoexistenceCommitteeVote);
        public void Update(EvidenceCoexistenceCommitteeVote EvidenceCoexistenceCommitteeVote) => _context.EvidenceCoexistenceCommitteeVotes.Update(EvidenceCoexistenceCommitteeVote);

        public async Task<bool> ExistsAsync(EvidenceCoexistenceCommitteeVoteId id) => await _context.EvidenceCoexistenceCommitteeVotes
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<EvidenceCoexistenceCommitteeVote?> GetByIdAsync(EvidenceCoexistenceCommitteeVoteId Id) =>
            await _context.EvidenceCoexistenceCommitteeVotes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<EvidenceCoexistenceCommitteeVote?> GetNoneEvidenceCoexistenceCommitteeVoteByCompanyIdAsync(CompanyId companyId) =>
            await _context.EvidenceCoexistenceCommitteeVotes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<EvidenceCoexistenceCommitteeVote>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.EvidenceCoexistenceCommitteeVotes
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
    }
}
