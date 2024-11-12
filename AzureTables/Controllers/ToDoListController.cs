using AzureTables.Models.DTOs;
using EntityModels.Models;
using EntityModels.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureTables.Controllers;

public class ToDoListController : Controller
{
    private readonly ITableStorageService _tableStorageService;

    public ToDoListController(ITableStorageService tableStorageService)
    {
        _tableStorageService = tableStorageService;
    }
    
    // GET
    public IActionResult Index()
    {
        return RedirectToAction("Adding");
    }

    [HttpGet]
    public IActionResult Adding()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adding(ToDoListDTOViewModel dtoViewModel)
    {
        ToDo toDo = new ToDo
        {
            PartitionKey = dtoViewModel.Category,
            RowKey = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Theme = dtoViewModel.Theme,
            Description = dtoViewModel.Description
        };

        await _tableStorageService.UpsertEntityAsync(toDo);
        
        return RedirectToAction("Index", "Home");
    }
}