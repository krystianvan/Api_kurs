﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using RestaurantApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _restaurantService.Create(dto);

            return Created($"api/restaurant/{id}", null);
        }



        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RestaurantDto>> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<RestaurantDto>> Delete([FromRoute] int id)
        {
            var isDeleted = _restaurantService.Delete(id);
            if(isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] EditRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _restaurantService.Put(id, dto);
            if (isUpdated)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
