using MangaModelService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public class SQLScoreDistributionEntry : IScoreDistributionEntry
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLScoreDistributionEntry(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<ScoreDistributionEntry> AddAsync(ScoreDistributionEntry AddAsync)
        {
            await context.ScoreDistributionEntry.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<ScoreDistributionEntry> DeleteAsync(int idAsync)
        {
            ScoreDistributionEntry modelToDelete = await context.ScoreDistributionEntry.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.ScoreDistributionEntry.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<ScoreDistributionEntry>> GetAllModelAsync()
        {
            return await context.ScoreDistributionEntry.ToListAsync();
        }

        public async Task<ScoreDistributionEntry> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.ScoreDistributionEntry.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<ScoreDistributionEntry> UpdateAsync(ScoreDistributionEntry UpdateModelAsync)
        {
            var modelUpdate = context.ScoreDistributionEntry.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }
    }
}