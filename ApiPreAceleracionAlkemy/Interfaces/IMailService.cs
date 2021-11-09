using ApiPreAceleracionAlkemy.Entities;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public interface IMailService
    {
        Task SendEmail(User user);
    }
}