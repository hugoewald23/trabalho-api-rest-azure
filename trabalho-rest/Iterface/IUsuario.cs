using trabalho_rest.Model;

namespace ServiceSoap.Interface
{
    public interface IUsuario : IICrud<Usuario>
    {
        Task<int> GetIdUsuario(string nome, string Email);

        Task<Usuario> Update(Usuario entity);
    }
}
