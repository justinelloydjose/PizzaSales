using CsvHelper.Configuration;
using CsvHelper;
using PizzaSales.Models;
using PizzaSalesAPI.Helper;
using PizzaSalesAPI.Repository;
using System.Globalization;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Services.Interfaces;
using PizzaSalesAPI.Shared;
using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task GetDataFromCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<OrderItemDtoMap>();

                var orderItems = new List<OrderItem>();
                while (await csv.ReadAsync())
                {
                    var row = csv.GetRecord<OrderItemDtoCsv>();

                    var orderItem = new OrderItem
                    {
                        OrderId = row.OrderId,
                        PizzaId = row.PizzaId,
                        Quantity = row.Quantity,
                    };

                    orderItems.Add(orderItem);
                }
                await _orderItemRepository.AddRangeAsync(orderItems);
                await _orderItemRepository.SaveChangesAsync();
            }
        }

        public async Task<List<OrderItem>> GetPaginated(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 10)
            {
                pageSize = 10;
            }

            return await _orderItemRepository.GetPagedAsync(pageNumber, pageSize);
        }

    }
}
