// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BookStoreApi.Services;
// using DineSmartWEBAPI.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace DineSmartWEBAPI.Controller
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class RestaurantController : ControllerBase
//     {
//         private readonly RestaurantService _restaurantService;

//     public RestaurantController(RestaurantService RestaurantService) =>
//         _restaurantService = RestaurantService;

//     [HttpGet]
//     public async Task<List<Restaurant>> Get() =>
//         await _restaurantService.GetAsync();

//     [HttpGet("{id:length(24)}")]
//     public async Task<ActionResult<Restaurant>> Get(string id)
//     {
//         var Restaurant = await _restaurantService.GetAsync(id);

//         if (Restaurant is null)
//         {
//             return NotFound();
//         }

//         return Restaurant;
//     }

//     [HttpPost]
//     public async Task<IActionResult> Post(Restaurant newBook)
//     {
//         await _restaurantService.CreateAsync(newBook);

//         return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
//     }

//     [HttpPut("{id:length(24)}")]
//     public async Task<IActionResult> Update(string id, Restaurant updatedBook)
//     {
//         var Restaurant = await _restaurantService.GetAsync(id);

//         if (Restaurant is null)
//         {
//             return NotFound();
//         }

//         updatedBook.Id = Restaurant.Id;

//         await _restaurantService.UpdateAsync(id, updatedBook);

//         return NoContent();
//     }

//     [HttpDelete("{id:length(24)}")]
//     public async Task<IActionResult> Delete(string id)
//     {
//         var Restaurant = await _restaurantService.GetAsync(id);

//         if (Restaurant is null)
//         {
//             return NotFound();
//         }

//         await _restaurantService.RemoveAsync(id);

//         return NoContent();
//     }
//     }
// }