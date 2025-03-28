// 📄 Controllers/UsersController.cs (Servis bazlı Edit + Delete endpoint'leri dahil)

using GoRestUserAdmin.UI.Models;
using GoRestUserAdmin.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoRestUserAdmin.UI.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return PartialView("_UserFormPartial", new UserDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        var success = await _userService.CreateAsync(userDto);
        return success ? Ok() : BadRequest("Kullanıcı oluşturulamadı.");
    }

    public async Task<IActionResult> PartialList()
    {
        var users = await _userService.GetAllAsync();
        return PartialView("_UserTablePartial", users);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return PartialView("_UserFormPartial", user);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] UserDto userDto)
    {
        var success = await _userService.UpdateAsync(userDto);
        return success ? Ok() : BadRequest("Kullanıcı güncellenemedi.");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _userService.DeleteAsync(id);
        return success ? Ok() : BadRequest("Silinemedi");
    }
}
