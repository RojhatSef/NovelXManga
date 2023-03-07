using MangaModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public interface IReadingListPageRepsitory
    {
        IEnumerable<ReadingList> GetAllModels();

        ReadingList GetModel(int id);

        ReadingList Update(ReadingList UpdateModel);

        ReadingList Add(ReadingList addNewModel);

        ReadingList Delete(int id);
    }
}