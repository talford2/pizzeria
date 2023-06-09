﻿using Pizzeria.Repositories.Models;
using Pizzeria.Repository.Interfaces;

namespace Pizzeria.Repository.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private static List<RestaurantDto> _restaurants = new List<RestaurantDto>
        {
            new RestaurantDto
            {
                Id = 111,
                Location = "Preston"
            },
            new RestaurantDto
            {
                Id = 222,
                Location = "Southbank"
            }
        };

        public RestaurantDto? Get(int id)
        {
            return _restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            return _restaurants;
        }
    }
}
