namespace SignalR.DataAccessLayer.Abstract;

public interface IGenericDal<T> where T : class
{
    void Add(T Entity);
    void Update(T Entity);
    void Delete(T Entity);
    T GetById(int id);
    List<T> GetListAll();
}