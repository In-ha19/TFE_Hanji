using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;

namespace VotreProjet.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReminderService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SendRemindersAsync();
                //Changer le temps de vérification à 120 min par exemple à la place de 30
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); 
            }
        }
        private async Task SendRemindersAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var today = DateTime.Now.Date;

                var notesToRemind = dbContext.Notepads
                    .Where(note => note.Date_reminder.HasValue && note.Date_reminder.Value.Date == today && !note.IsEmailSent)
                    .ToList();

                foreach (var note in notesToRemind)
                {
                    var user = await userManager.FindByIdAsync(note.UserId);
                    if (user == null || string.IsNullOrEmpty(user.Email))
                        continue;

                    var subject = $"Rappel : {note.Titre}";
                    var body = $"<p>Voici votre rappel concernant : {note.Titre}</p><p>{note.Text}</p>";

                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                    await emailSender.SendEmailAsync(user.Email, subject, body);

                    note.IsEmailSent = true;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
#if DEBUG
                var bg = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("Erreur lors de l'envoi de mail :");
                Console.Write(ex.Message);
                Console.BackgroundColor = bg;
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("Hint : Est-ce que paper cut est allumé?");
                Console.BackgroundColor = bg;
                Console.WriteLine("");
#endif
            }
        }
    }
}
