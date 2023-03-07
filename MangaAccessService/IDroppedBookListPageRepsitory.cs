using MangaModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public interface IDroppedBookListPageRepsitory
    {
        IEnumerable<DroppedBookList> GetAllModels();

        DroppedBookList GetModel(int id);

        DroppedBookList Update(DroppedBookList UpdateModel);

        DroppedBookList Add(DroppedBookList addNewModel);

        DroppedBookList Delete(int id);
    }
}