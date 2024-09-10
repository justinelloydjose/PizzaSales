using CsvHelper;
using CsvHelper.Configuration;
using PizzaSales.Models;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Dto.CSV;
using PizzaSalesAPI.Helper;
using PizzaSalesAPI.Repository;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Services.Interfaces;
using PizzaSalesAPI.Shared;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq; // For .Select()

namespace PizzaSalesAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task GetDataFromCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<OrderDtoMap>();

                var orders = new List<Order>();
                while (await csv.ReadAsync())
                {
                    var row = csv.GetRecord<OrderDtoCsv>();

                    var order = new Order
                    {
                        CreatedAt = row.Date.Add(row.Time)
                    };

                    orders.Add(order);
                }
                await _orderRepository.AddRangeAsync(orders);
                await _orderRepository.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetPaginated(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 10)
            {
                pageSize = 10;
            }

            return await _orderRepository.GetPagedAsync(pageNumber, pageSize);
        }
    }
}
