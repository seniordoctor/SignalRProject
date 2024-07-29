namespace SignalR.BusinessLayer.Abstract;

public interface IGenericService<T> where T : class
{
    void TAdd(T Entity);
    void TUpdate(T Entity);
    void TDelete(T Entity);
    T TGetById(int id);
    List<T> TGetListAll();
}