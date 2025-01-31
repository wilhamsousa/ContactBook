using System.Runtime.ConstrainedExecution;
using Uex.ContactBook.Domain.Model.DTOs.External;

namespace Uex.ContactBook.Domain.Interfaces.External
{
    public interface IExternalServiceAsync
    {        
        Task<ViaCepResponse> GetViaCep(string cep);
        Task RabbitMqProduce(string queueName, string message);
    }
}
