using DineSmartWebAPI.Models;
using DineSmartWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace DineSmartWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantService _restaurantService;
    

    public RestaurantController(RestaurantService restaurantService) =>
        _restaurantService = restaurantService;

    [HttpGet("allrestaurants")]
    public async Task<List<Restaurant>> GetRestaurants() =>
        await _restaurantService.GetRestaurantAsync();

    [HttpPost("addrestaurant")]
    public async Task<IActionResult> Post(Restaurant restaurant)
    { 
        await _restaurantService.CreateRestaurantAsync(restaurant);
        return Ok();
    }

    [HttpGet("allusers")]
    public async Task<List<User>> GetUsers() =>
        await _restaurantService.GetUserAsync();

    [HttpGet("{userName}")]
    public async Task<ActionResult<User>> Get(string userName)
    {
        var user = await _restaurantService.GetAsync(userName);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("findtime/{restaurantId}")]
    public async Task<ActionResult<int>> GetTime(string restaurantId)
    {
        var _users = await _restaurantService.GetUserAsync();

        int peopleInQueue = 0;

        foreach (User user in _users)
        {
            if (user.WaitListId == restaurantId)
                peopleInQueue++;
        }

        return Ok(peopleInQueue * 3);
    }

    [HttpGet("findreview/{restaurantId}")]
    public async Task<ActionResult<Dictionary<string, KeyValuePair<string, string>>>> RetrieveReview(string restaurantId)
    {
        var _users = await _restaurantService.GetUserAsync();

        if (_users is null)
            return NotFound();

        Dictionary<string, KeyValuePair<string, string>> ReviewList = new();

        foreach (User user in _users)
        {
            foreach (KeyValuePair<string, KeyValuePair<string, string>> list in user.ListOfReviews)
            {
                if (list.Key == restaurantId)
                    ReviewList.Add(user.UserName, list.Value);
            }
        }

        return ReviewList;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _restaurantService.CreateUserAsync(newUser);

        return Ok();
        //return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }


    [HttpPost("queue/{userName}/{restaurantId}")]
    public async Task<IActionResult> Update(string userName, string restaurantId)
    {
        var user = await _restaurantService.GetAsync(userName);
        var restaurant = await _restaurantService.GetSpecificRestaurantAsync(restaurantId);

        if (user is null)
        {
            return NotFound();
        }

        if(user.WaitListId == null)
            {
                user.WaitListId = restaurantId;
                restaurant.PeopleInQueue++;
            }
        else
            {
                user.WaitListId = null;
                restaurant.PeopleInQueue--;
            }
        


        await _restaurantService.UpdateUserAsync(userName, user);
        await _restaurantService.UpdateRestaurantAsync(restaurantId, restaurant);



        string lineData = user.WaitListId == null ? "not in line." : $"in line for {user.WaitListId}";
        return Ok($"{user.UserName} is currently {lineData}.");
    }


    [HttpPost("newReview/{userName}/{restaurantId}/{review}/{rating}")]
    public async Task<IActionResult> NewReview(string restaurantId, string userName, string review, string rating)
    {
        var user = await _restaurantService.GetAsync(userName);

        if (user is null | restaurantId is null | review is null)
        {
            return NotFound();
        }

        if (user.ListOfReviews.ContainsKey(restaurantId))
        {
            return BadRequest();
        }

        KeyValuePair<string, string> _value = new(review, rating);
        user.ListOfReviews.Add(key: restaurantId, value: _value);


        await _restaurantService.UpdateUserAsync(userName, user);


        return Ok();
    }

    [HttpGet("isQueue/{userName}")]

    public async Task<IActionResult> IsQueued(string userName)
    {
        var user = await _restaurantService.GetAsync(userName);

        if (user is null)
            return NotFound();
        
        return Ok(user.WaitListId != null ? "Not in Queue": "In queue");
    }
    
    [HttpDelete("{userName}")]
    public async Task<IActionResult> Delete(string userName)
    {
        var user = await _restaurantService.GetAsync(userName);

        if (user is null)
        {
            return NotFound();
        }

        await _restaurantService.RemoveAsync(userName);

        return NoContent();
    }


}