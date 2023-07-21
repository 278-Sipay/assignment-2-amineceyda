namespace Assigment2Api.DBOperations.Repository.Base
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        void Save();
        Entity GetById(int id);
        void Insert(Entity entity);
        void Update(Entity entity);
        void Delete(Entity entity);
        void DeleteById(int id);
        List<Entity> GetAll();
        IQueryable<Entity> GetAllAsQueryable();
    }

}
