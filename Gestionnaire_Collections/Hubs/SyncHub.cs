using Gestionnaire_Collections.DTO.Shared.Synchronisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Gestionnaire_Collections.Hubs
{
    public class SyncHub : Hub
    {
        public async Task SendCategoriesWithArticles(List<CategoryWithArticlesDTO> categoriesWithArticles)
        {
            await Clients.All.SendAsync("ReceiveCategories", categoriesWithArticles);
        }
    }


}
