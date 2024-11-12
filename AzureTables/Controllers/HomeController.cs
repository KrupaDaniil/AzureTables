using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureTables.Models;
using EntityModels.Models;
using EntityModels.Services.Interfaces;

namespace AzureTables.Controllers;

public class HomeController : Controller
{
    private readonly ITableStorageService _tableStorageService;
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ITableStorageService tableStorageService, ILogger<HomeController> logger)
    {
        _tableStorageService = tableStorageService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        IList<ToDo> toDoList = await _tableStorageService.GetAllEntitiesAsync<ToDo>();

        if (!toDoList.Any())
        {
            return RedirectToAction("Adding", "ToDoList");
        }
        
        IList<ToDoListViewModel> models = new List<ToDoListViewModel>();

        foreach (ToDo item in toDoList)
        {
            models.Add(new ToDoListViewModel()
            {
                PartitionKey = item.PartitionKey,
                RowKey = item.RowKey,
                Theme = item.Theme,
                Description = item.Description
            });
        }
        
        return View(models);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}