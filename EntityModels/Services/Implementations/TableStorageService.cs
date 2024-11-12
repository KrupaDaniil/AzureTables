using Azure;
using Azure.Data.Tables;
using EntityModels.Services.Interfaces;

namespace EntityModels.Services.Implementations;

public class TableStorageService : ITableStorageService
{
    private const string TableName = "";
    private readonly string _path;

    public TableStorageService(string path)
    {
        _path = path;
    }

    private async Task<TableClient> GetTableClientAsync()
    {
        TableServiceClient tableServiceClient = new TableServiceClient(_path);
        TableClient tableClient = tableServiceClient.GetTableClient("entitiesStorage");
        await tableClient.CreateIfNotExistsAsync();
        
        return tableClient;
    }
    public async Task<IList<T>> GetAllEntitiesAsync<T>(string? partitionKey = null) where T : class, ITableEntity, new()
    {
        TableClient tableClient = await GetTableClientAsync();
        
        IList<T> entities = new List<T>();
        
        string? filter = partitionKey == null ? null : $"PartitionKey eq '{partitionKey}'";

        AsyncPageable<T> result = tableClient.QueryAsync<T>(filter);

        await foreach (T item in result)
        {
            entities.Add(item);
        }
        
        return entities;
    }

    public async Task<T> GetEntityAsync<T>(string? partitionKey, string? rowKey) where T : class, ITableEntity
    {
        TableClient tableClient = await GetTableClientAsync();

        Response<T> r = await tableClient.GetEntityAsync<T>(partitionKey, rowKey);
        
        return r.Value;
    }

    public async Task UpsertEntityAsync<T>(T entity) where T : class, ITableEntity
    {
        TableClient tableClient = await GetTableClientAsync();

        Response rsp = await tableClient.UpsertEntityAsync(entity);
    }

    public async Task DeleteEntityAsync(string? partitionKey, string? rowKey)
    {
        TableClient tableClient = await GetTableClientAsync();
        
        await tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}