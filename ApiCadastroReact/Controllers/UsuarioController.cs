using ApiCadastroReact.Models;
using ApiCadastroReact.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiCadastroReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModels>> Adicionar([FromBody] UsuarioModels usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioModels usuario = await _usuarioRepository.CriarUsuario(usuarioModel);

            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModels>>> Buscar()
        {
            List<UsuarioModels> listaUsuarios = await _usuarioRepository.BuscarTodos();
            return Ok(listaUsuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModels>> BuscarUnico(int id)
        {
            UsuarioModels usuario = await _usuarioRepository.BuscarUsuario(id);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModels>> Remover(int id)
        {
            bool apagado = (bool)await _usuarioRepository.RemoverUsuario(id);
            return Ok(apagado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModels>> Atualizar([FromBody] UsuarioModels usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModels usuario = await _usuarioRepository.AtualizarUsuario(usuarioModel, id);
            return Ok(usuario);
        }
    }
}
