using MangaModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public interface IScoreDistributionEntry
    {
        Task<IEnumerable<ScoreDistributionEntry>> GetAllModelAsync();

        Task<ScoreDistributionEntry> GetModelAsync(int idAsync);

        Task<ScoreDistributionEntry> UpdateAsync(ScoreDistributionEntry UpdateModelAsync);

        Task<ScoreDistributionEntry> AddAsync(ScoreDistributionEntry AddAsync);

        Task<ScoreDistributionEntry> DeleteAsync(int idAsync);
    }
}