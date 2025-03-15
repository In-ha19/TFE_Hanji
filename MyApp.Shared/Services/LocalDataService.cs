using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Models;
using Microsoft.EntityFrameworkCore;
using MyApp.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Shared.Services
{
    public class LocalDataService
    {
        private readonly AppDbContext _context;

        public LocalDataService(string dbPath)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            _context = new AppDbContext(options);
        }

        // Méthode pour récupérer tous les articles
        public async Task<List<Article>> GetArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        // Méthode pour ajouter un article
        public async Task AddArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        // Méthode pour supprimer un article
        public async Task RemoveArticleAsync(Article article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
