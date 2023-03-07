using MangaModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService
{
    public interface IBuyPageRepsitory
    {
        IEnumerable<BuyPage> GetAllModels();

        BuyPage GetModel(int id);

        BuyPage Update(BuyPage UpdateModel);

        BuyPage Add(BuyPage addNewModel);

        BuyPage Delete(int id);
    }
}