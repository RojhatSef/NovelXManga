using MangaModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public interface IFavoritListPageRepsitory
    {
        IEnumerable<FavoritBookList> GetAllModels();

        FavoritBookList GetModel(int id);

        FavoritBookList Update(FavoritBookList UpdateModel);

        FavoritBookList Add(FavoritBookList addNewModel);

        FavoritBookList Delete(int id);
    }
}