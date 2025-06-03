using ApiCadastroReact.Models;

namespace ApiCadastroReact.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModels> CriarUsuario(UsuarioModels usuarioModels);
        Task<List<UsuarioModels>> BuscarTodos();
        Task<UsuarioModels> BuscarUsuario(int id);
        Task<bool> RemoverUsuario(int id);
        Task<UsuarioModels> AtualizarUsuario(UsuarioModels usuarioModels, int id);
    }
}
