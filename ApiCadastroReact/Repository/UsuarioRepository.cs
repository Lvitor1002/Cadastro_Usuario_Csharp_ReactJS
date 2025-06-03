using ApiCadastroReact.Data;
using ApiCadastroReact.Models;
using ApiCadastroReact.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroReact.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosDB _db;
        public UsuarioRepository(UsuariosDB db)
        {
            _db = db;
        }
        public async Task<UsuarioModels> CriarUsuario(UsuarioModels usuarioModels)
        {
            try
            {
                await _db.AddAsync(usuarioModels);
                await _db.SaveChangesAsync();
                return usuarioModels;
            }
            catch (Exception ex)
            {
                throw new Exception($">Erro ao criar usuário: {ex.Message}");
            }
        }

        public async Task<UsuarioModels> AtualizarUsuario(UsuarioModels usuarioModels, int id)
        {
            try
            {
                UsuarioModels usuario = await BuscarUsuario(id);
                if (usuario == null)
                {
                    throw new Exception($"Usuario de Id[{id}] não encontrado.");
                }
                usuario.Id = usuarioModels.Id;
                usuario.Nome = usuarioModels.Nome;
                usuario.Idade = usuarioModels.Idade;
                usuario.Email = usuarioModels.Email;
                    
                _db.Usuarios.Update(usuario);
                await _db.SaveChangesAsync();
                return usuario;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível atualizar usuário! - {ex.Message}");
            }
        }

        public async Task<List<UsuarioModels>> BuscarTodos()
        {
            try
            {
                return await _db.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível buscar todos os usuários! - {ex.Message}");
            }
        }

        public async Task<UsuarioModels> BuscarUsuario(int id)
        {
            try
            {
                return await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível buscar usuário! - {ex.Message}");
            }
        }
        public async Task<bool> RemoverUsuario(int id)
        {
            try
            {
                UsuarioModels usuario = await BuscarUsuario(id);
                if(usuario == null)
                {
                    throw new Exception($"Usuario de Id[{id}] não encontrado.");
                }

                _db.Usuarios.Remove(usuario);
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível remover usuário! - {ex.Message}");
            }
        }
    }
}
