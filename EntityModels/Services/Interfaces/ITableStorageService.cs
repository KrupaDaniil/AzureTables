using Azure.Data.Tables;

namespace EntityModels.Services.Interfaces;

public interface ITableStorageService
{
    public Task<IList<T>> GetAllEntitiesAsync<T>(string? partitionKey = null) where T : class, ITableEntity, new();
    public Task<T> GetEntityAsync<T>(string? partitionKey, string? rowKey) where T : class, ITableEntity;
    public Task UpsertEntityAsync<T>(T entity) where T : class, ITableEntity;
    public Task DeleteEntityAsync(string? partitionKey, string? rowKey);
}