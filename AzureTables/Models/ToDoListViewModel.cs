using Azure;
using Azure.Data.Tables;

namespace AzureTables.Models;

public class ToDoListViewModel
{
    public string? PartitionKey { get; set; }
    public string? RowKey { get; set; }
    public string? Theme  { get; set; }
    public string? Description  { get; set; }
}