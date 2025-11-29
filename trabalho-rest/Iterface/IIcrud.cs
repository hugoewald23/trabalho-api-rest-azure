using System.ServiceModel;

namespace ServiceSoap.Interface
{
    public interface IICrud<T1>
    {
        Task<T1> Create(T1 entity);

        Task<T1> Read(int id);

        Task<IEnumerable<T1>> ReadAll();

        Task<bool> Delete(int id);
    }
}
