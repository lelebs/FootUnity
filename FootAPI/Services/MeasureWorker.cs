using FootAPI.Interfaces;
using FootAPI.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FootAPI.Services
{
    public class MeasureWorker : BackgroundService
    {
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer consumer;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IUsuarioMedidaRepository usuarioMedidaRepository;

        public MeasureWorker(RabbitMQConfigurations configurations,
            ILoggerFactory loggerFactory,
            IUsuarioRepository usuarioRepository)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configurations.HostName,
                Port = configurations.Port,
                UserName = configurations.UserName,
                Password = configurations.Password
            };

            this._logger = loggerFactory.CreateLogger<MeasureWorker>();
            this._connection = factory.CreateConnection();
            this._channel = _connection.CreateModel();
            this._channel.QueueDeclare("MeasureResult", false, false, false, null);
            this.consumer = new EventingBasicConsumer(_channel);
            this.usuarioRepository = usuarioRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                consumer.Received += async (a, evento) =>
                {
                    try
                    {
                        var content = System.Text.Encoding.UTF8.GetString(evento.Body.ToArray());
                        var retorno = JsonConvert.DeserializeObject<Measure>(content);
                        var pessoaDados = await usuarioRepository.ObterInfantilTamanho(retorno.UserId);

                        retorno.Infantil = pessoaDados.Item1;
                        retorno.IdSexo = pessoaDados.Item2;

                        retorno.DataHora = DateTime.UtcNow;

                        this._logger.LogInformation("Medida {0} inserida com sucesso", await usuarioMedidaRepository.InserirMedida(retorno));
                        _channel.BasicAck(evento.DeliveryTag, false);
                    }
                    catch(Exception ex)
                    {
                        this._logger.LogError(ex, System.Text.Encoding.UTF8.GetString(evento.Body.ToArray()));
                        _channel.BasicReject(evento.DeliveryTag, false);
                    }
                };
            }
        }
    }
}
