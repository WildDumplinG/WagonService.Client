using Grpc.Net.Client;
using static WagonService.Server.WagonService;
using WagonService.Server;

namespace WagonService.Client.Services
{
    public class WagonService
    {
        public WagonService(string connection) 
        {
            _grpcChannel = GrpcChannel.ForAddress(connection, new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 100 * 1024 * 1024,
                MaxSendMessageSize = 100 * 1024 * 1024
            });
        }

        private GrpcChannel _grpcChannel { get; set; }

        public async Task GetWagons()
        {
            var client = new WagonServiceClient(_grpcChannel);

            System.Console.WriteLine("Введите начальное время (в формате yyyy-MM-ddTHH:mm:ss):");
            var startTime = System.Console.ReadLine();
            System.Console.WriteLine("Введите конечное время (в формате yyyy-MM-ddTHH:mm:ss):");
            var endTime = System.Console.ReadLine();

            var request = new WagonRequest { StartTime = startTime, EndTime = endTime };
            var response = await client.GetWagonsAsync(request);

            System.Console.WriteLine("Полученные вагоны:");
            foreach (var wagon in response.Wagons)
            {
                System.Console.WriteLine($"Инвентарный номер: {wagon.InventoryNumber}, Время прибытия: {wagon.ArrivalTime}, Время отправления: {wagon.DepartureTime}");
            }
        }
    }
}
