using api_filmes_senai.Domains;
using api_filmes_senai.DTO;
using api_filmes_senai.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Endpoint para cadastrar usuário
        /// </summary>
        /// <param name="usuario">Usuário a ser cadastrado</param>
        /// <returns>Status code 201</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(usuario);

                return StatusCode(201, usuario);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint para buscar um usuário por id
        /// </summary>
        /// <param name="id">Id do usuário a ser buscado</param>
        /// <returns>Usuário buscado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(id);

                if (usuarioBuscado != null)
                {
                    return Ok(usuarioBuscado);
                }
                return null!;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}