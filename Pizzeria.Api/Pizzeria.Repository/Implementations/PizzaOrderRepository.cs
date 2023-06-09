﻿using Pizzeria.Repository.Interfaces;
using Pizzeria.Repository.Models;

namespace Pizzeria.Repository.Implementations
{
	public class PizzaOrderRepository : IPizzaOrderRepository
	{
		private static List<PizzaOrderDto> _pizzaOrder = new List<PizzaOrderDto> { };

		public int Create(int orderId, int pizzaId)
		{
			var newId = _pizzaOrder.Any() ? _pizzaOrder.Max(x => x.Id) + 1 : 1;
			_pizzaOrder.Add(new PizzaOrderDto
			{
				Id = newId,
				OrderId = orderId,
				PizzaId = pizzaId
			});

			return newId;
		}

		public void Delete(int pizzaOrderId)
		{
			var countChange = _pizzaOrder.RemoveAll(p => p.Id == pizzaOrderId);
			if (countChange == 0)
				throw new Exception($"Could not find pizza order for {pizzaOrderId}");
		}

		public IEnumerable<PizzaOrderDto> GetForOrder(int orderId)
		{
			return _pizzaOrder.Where(p => p.OrderId == orderId);
		}
	}
}
