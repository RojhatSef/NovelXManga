﻿using MangaModelService;

namespace MangaAccessService
{
    public interface IAuthorRepsitory
    {
        IEnumerable<AuthorModel> Search(string searchTerm);

        IEnumerable<AuthorModel> GetAuthorModels();

        AuthorModel GetAuthor(int id);

        AuthorModel Update(AuthorModel updateAuthorModel);

        AuthorModel Add(AuthorModel newAuthorModel);

        AuthorModel Delete(int id);
    }
}