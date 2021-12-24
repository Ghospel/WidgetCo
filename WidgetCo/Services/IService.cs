namespace WidgetAndCo.Services
{
    public interface IService<TEntity>
    {
        Task<TEntity> GetEntity(string id);
        Task<IEnumerable<TEntity>> GetEntities();
        Task<TEntity> CreateEntity(TEntity entity);
        Task<TEntity> UpdateEntity(TEntity entity);
        Task<TEntity> DeleteEntity(string id);
    }
}
