using Azure;
using Azure.Data.Tables;

namespace EntityModels.Models;

public class ToDo : ITableEntity
{
    public string? PartitionKey { get; set; }
    public string? RowKey { get; set; }
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
    public string? Theme  { get; set; }
    public string? Description  { get; set; }
}