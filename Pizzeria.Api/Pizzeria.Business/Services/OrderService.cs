﻿using Pizzeria.Business.Exceptions;
using Pizzeria.Business.Models;
using Pizzeria.Repository.Interfaces;
using Pizzeria.Repository.Models;

namespace Pizzeria.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPizzaOrderRepository _pizzaOrderRepository;

        private readonly IRestaurantService _restaurantService;
        private readonly IPizzaService _pizzaService;

        public OrderService(IOrderRepository orderRepository, IPizzaOrderRepository pizzaOrderRepository, IRestaurantService restaurantService, IPizzaService pizzaService)
        {
            _orderRepository = orderRepository;
            _pizzaOrderRepository = pizzaOrderRepository;
            _restaurantService = restaurantService;
            _pizzaService = pizzaService;
        }

        // Order always starts with 1 pizza
        public Order Create(int restaurantId, int pizzaId)
        {
            var newOrderId = _orderRepository.Create(new OrderDto { RestaurantId = restaurantId });
            var restaurant = _restaurantService.Get(restaurantId);
            _pizzaOrderRepository.Create(newOrderId, pizzaId);

            return new Order
            {
                Id = newOrderId,
                PizzaOrders = new List<PizzaOrder> {
                    new PizzaOrder {
                        Pizza = _pizzaService.GetPizza(pizzaId),
                        ExtraToppings = new Topping[] { }
                    }
                },
                Restaurant = restaurant
            };
        }

        public Order Get(int id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                throw new UnknownOrderException(id);

            var restaurant = _restaurantService.Get(order.RestaurantId);
            var pizzaOrders = _pizzaOrderRepository.GetForOrder(order.Id);

            return new Order
            {
                Id = id,
                Restaurant = restaurant,
                PizzaOrders = pizzaOrders.Select(o => new PizzaOrder
                {
                    Id = o.Id,
                    Pizza = _pizzaService.GetPizza(o.PizzaId),
                    // ExtraToppings = ????
                })
            };
        }

        public void AddPizzaToOrder()
        {
            throw new Exception();
        }

        public void RemovePizzaFromOrder()
        {
            throw new Exception();
        }
    }
}
