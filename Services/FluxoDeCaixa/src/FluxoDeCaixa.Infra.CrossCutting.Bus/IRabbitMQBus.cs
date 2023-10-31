using FluentValidation.Results;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Infra.CrossCutting.Bus
{
    public interface IRabbitMQBus
    {
        Task<ValidationResult> SendMessage(string message, string queueName);
    }

}