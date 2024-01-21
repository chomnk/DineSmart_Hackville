using DineSmartWebAPI.Models;
using DineSmartWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DineSmartWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private RestaurantService _restaurantService;


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

    [HttpGet("finduser/{userName}")]
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
            if (user.waitListId == restaurantId)
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
            foreach (KeyValuePair<string, KeyValuePair<string, string>> list in user.listOfReviews)
            {
                if (list.Key == restaurantId)
                    ReviewList.Add(user.userName, list.Value);
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
    public async Task<IActionResult> UpdateQueue(string userName, string restaurantId)
    {
        var user = await _restaurantService.GetAsync(userName);
        var restaurant = await _restaurantService.GetSpecificRestaurantAsync(restaurantId);

        if (user is null)
        {
            return NotFound();
        }

        if(user.waitListId is null)
            {
                user.waitListId = restaurantId;
                restaurant.peopleInQueue++;
            }
        else
            {
                user.waitListId = null;
                restaurant.peopleInQueue--;
            }
        


        await _restaurantService.UpdateUserAsync(userName, user);
        await _restaurantService.UpdateRestaurantAsync(restaurantId, restaurant);



        string lineData = (user.waitListId is null) ? "not in line" : $"in line for {user.waitListId}";
        return Ok($"{user.userName} is currently {lineData}.");
    }


    [HttpPost("newReview/{userName}/{restaurantId}/{review}/{rating}")]
    public async Task<IActionResult> NewReview(string restaurantId, string userName, string review, string rating)
    {
        var user = await _restaurantService.GetAsync(userName);

        if (user is null | restaurantId is null | review is null)
        {
            return NotFound();
        }


        KeyValuePair<string, string> _value = new(review, rating);
        user.listOfReviews.Add(key: restaurantId, value: _value);


        await _restaurantService.UpdateUserAsync(userName, user);


        return Ok();
    }

    [HttpGet("isQueue/{userName}")]

    public async Task<IActionResult> IsQueued(string userName)
    {
        var user = await _restaurantService.GetAsync(userName);

        if (user is null)
            return NotFound();
        
        return Ok(user.waitListId is null ? "Not in Queue": "In queue");
    }
    
    [HttpDelete("delete/{userName}")]
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

    [HttpGet("checklogin/{username}/{password}")]
    public async Task<IActionResult> CheckLogin(string username, string password)
    {
        var user = await _restaurantService.GetAsync(username);

        if (user.password == password)
            return Ok("Granted");
        else
            return BadRequest();
    }
    [HttpGet("signup/{username}/{password}")]
    
    public async Task<IActionResult> SignUp(string username, string password)
    {
        var user = await _restaurantService.GetAsync(username);

        if (user is null)
        {

            User newUser = new()
            {
                userName = username,
                password = password
            };

            await _restaurantService.CreateUserAsync(newUser);
            Console.WriteLine("mongoimport --uri mongodb+srv://davidcaluag:vgDHJCRMYYN2ed9s@cluster0.hjzavom.mongodb.net/RestaurantDatabase --collection _user --type JSON --file \"C:\\Users\\myelf\\OneDrive\\Desktop\\DineSmart_Hackville\\database\\RestaurantDatabase._user.json\"");
            return Ok("Granted");
        }

        else
            return BadRequest("user already exists.");
    }

}