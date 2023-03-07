using MangaModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public interface ICompletedBookListPageRepsitory
    {
        IEnumerable<CompletedBookList> GetAllModels();

        CompletedBookList GetModel(int id);

        CompletedBookList Update(CompletedBookList UpdateModel);

        CompletedBookList Add(CompletedBookList addNewModel);

        CompletedBookList Delete(int id);
    }
}