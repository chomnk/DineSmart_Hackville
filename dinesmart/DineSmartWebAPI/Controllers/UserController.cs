using DineSmartWebAPI.Models;
using DineSmartWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService booksService) =>
        _userService = booksService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _userService.GetAsync();

    [HttpGet("{userName}")]
    public async Task<ActionResult<User>> Get(string userName)
    {
        var user = await _userService.GetAsync(userName);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("findtime/{restaurantId}")]
    public async Task<ActionResult<int>> GetTime(string restaurantId)
    {
        var _users = await _userService.GetAsync();

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
        var _users = await _userService.GetAsync();

        if (_users is null)
            return NotFound();

        Dictionary<string, KeyValuePair<string, string>> ReviewList = new();

        foreach(User user in _users)
        {
            foreach(KeyValuePair<string, KeyValuePair<string,string>>list in user.ListOfReviews)
            {
                if(list.Key == restaurantId)
                    ReviewList.Add(user.UserName, list.Value);
            }
        }

        return ReviewList;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _userService.CreateAsync(newUser);

        return Ok();
        //return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }


    [HttpPut("queue/{userName}/{restaurantId}")]
    public async Task<IActionResult> Update(string userName, string restaurantId)
    {
        var user = await _userService.GetAsync(userName);

        if (user is null)
        {
            return NotFound();
        }

        user.WaitListId = user.WaitListId == null ? restaurantId : null;
        await _userService.UpdateAsync(userName, user);

        string lineData = user.WaitListId == null ? "not in line." : $"in line for {user.WaitListId}";
        return Ok($"{user.UserName} is currently {lineData}.");
    }
    

    [HttpPut("newReview/{userName}/{restaurantId}/{review}/{rating}")]
    public async Task<IActionResult> NewReview(string restaurantId, string userName, string review, string rating)
    {
        var user = await _userService.GetAsync(userName);

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


        await _userService.UpdateAsync(userName, user);


        return Ok();
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> Delete(string userName)
    {
        var user = await _userService.GetAsync(userName);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(userName);

        return NoContent();
    }
}