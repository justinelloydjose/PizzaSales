using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Repository;
using PizzaSalesAPI.Services.Interfaces;
using PizzaSalesAPI.Services;
using System.Reflection;
using PizzaSalesAPI.Shared;
using PizzaSales.Models;

namespace PizzaSalesAPI.Extensions
{
    public static class ServiceRegistrationExtensions
    {
            public static void AddApplicationServices(this IServiceCollection services)
            {
                //register IBaseRepository
                services.AddScoped<IBaseRepository<Order>, OrderRepository>();
                services.AddScoped<IBaseRepository<OrderItem>, OrderItemRepository>();
                services.AddScoped<IBaseRepository<Pizza>, PizzaRepository>();
                services.AddScoped<IBaseRepository<PizzaType>, PizzaTypeRepository>();

                //register IBaseService
                services.AddScoped<IBaseService<Order>, OrderService>();
                services.AddScoped<IBaseService<OrderItem>, OrderItemService>();
                services.AddScoped<IBaseService<Pizza>, PizzaService>();
                services.AddScoped<IBaseService<PizzaType>, PizzaTypeService>();

                // Register other services and repositories
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<IOrderItemService, OrderItemService>();
                services.AddScoped<IOrderItemRepository, OrderItemRepository>();
                services.AddScoped<IPizzaService, PizzaService>();
                services.AddScoped<IPizzaRepository, PizzaRepository>();
                services.AddScoped<IPizzaTypeService, PizzaTypeService>();
                services.AddScoped<IPizzaTypeRepository, PizzaTypeRepository>();
                services.AddScoped<IStatisticsService, StatisticsService>();


        }
    }
}
