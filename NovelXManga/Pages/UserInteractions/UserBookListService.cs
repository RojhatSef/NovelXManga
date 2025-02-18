﻿using MangaAccessService;
using MangaAccessService.DTO.UserProfileMangaDTO;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.UserInteractions
{
    public class UserBookListService
    {
        private readonly MangaNNovelAuthDBContext _context;

        public UserBookListService(MangaNNovelAuthDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetReadingListMangaImages(string userId, int skip, int take)
        {
            return await _context.readingLists
                .Where(rl => rl.UserId == userId)
                .SelectMany(rl => rl.ReadingMangaList)
                .Skip(skip)
                .Take(take)
                .Select(m => new UserMangaImageDto
                {
                    MangaID = m.MangaID,
                    MangaName = m.MangaName,
                    PhotoPath = m.PhotoPath
                })
                .ToListAsync();
        }

        //wish
        public async Task<IEnumerable<UserMangaImageDto>> GetWishListMangaImages(string userId, int skip, int take)
        {
            return await _context.wishBookLists
                .Where(wl => wl.UserId == userId)
                .SelectMany(wl => wl.WishBooks)
                .Skip(skip)
                .Take(take)
                .Select(m => new UserMangaImageDto
                {
                    MangaID = m.MangaID,
                    MangaName = m.MangaName,
                    PhotoPath = m.PhotoPath
                })
                .ToListAsync();
        }

        //Dropped
        public async Task<IEnumerable<UserMangaImageDto>> GetDroppedListMangaImages(string userId, int skip, int take)
        {
            return await _context.droppedBookLists
                .Where(dl => dl.UserId == userId)
                .SelectMany(dl => dl._DroppedBooks)
                .Skip(skip)
                .Take(take)
                .Select(m => new UserMangaImageDto
                {
                    MangaID = m.MangaID,
                    MangaName = m.MangaName,
                    PhotoPath = m.PhotoPath
                })
                .ToListAsync();
        }

        //Favorite
        public async Task<IEnumerable<UserMangaImageDto>> GetFavoriteListMangaImages(string userId, int skip, int take)
        {
            return await _context.favoritBookLists
                .Where(fl => fl.UserId == userId)
                .SelectMany(fl => fl.FavoritBooks)
                .Skip(skip)
                .Take(take)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetCompletedListMangaImages(string userId, int skip, int take)
        {
            return await _context.completedBookLists
                .Where(cl => cl.UserId == userId)
                .SelectMany(cl => cl.CompleteBookList)
                .Skip(skip)
                .Take(take)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetReadingListMangaImages(string userId)
        {
            return await _context.readingLists
                .Where(rl => rl.UserId == userId && rl.ShowReadingList)
                .SelectMany(rl => rl.ReadingMangaList)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetCompletedListMangaImages(string userId)
        {
            return await _context.completedBookLists
                .Where(cl => cl.UserId == userId)
                .SelectMany(cl => cl.CompleteBookList)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetDroppedListMangaImages(string userId)
        {
            return await _context.droppedBookLists
                .Where(dl => dl.UserId == userId)
                .SelectMany(dl => dl._DroppedBooks)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetFavoriteListMangaImages(string userId)
        {
            return await _context.favoritBookLists
                .Where(fl => fl.UserId == userId)
                .SelectMany(fl => fl.FavoritBooks)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMangaImageDto>> GetWishListMangaImages(string userId)
        {
            return await _context.wishBookLists
                .Where(wl => wl.UserId == userId)
                .SelectMany(wl => wl.WishBooks)
                .Select(m => new UserMangaImageDto { MangaID = m.MangaID, MangaName = m.MangaName, PhotoPath = m.PhotoPath })
                .ToListAsync();
        }
    }
}