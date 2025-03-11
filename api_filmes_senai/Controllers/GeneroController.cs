using api_filmes_senai.Domains;
using api_filmes_senai.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;
        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        /// <summary>
        /// Endpoint para listar todos os gêneros
        /// </summary>
        /// <returns>Lista de Gêneros</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_generoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero">Gênero a ser cadastrado</param>
        /// <returns>Status code 201</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Genero novoGenero)
        {
            try
            {
                _generoRepository.Cadastrar(novoGenero);

                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para buscar um gênero pelo seu id
        /// </summary>
        /// <param name="id">Id do Gênero buscado</param>
        /// <returns>Gênero Buscado</returns>
        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Genero generoBuscado = _generoRepository.BuscarPorId(id);

                return Ok(generoBuscado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para excluir um gênero
        /// </summary>
        /// <param name="id">Id do Gênero a ser excluído</param>
        /// <returns>Status Code 204</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Endpoint para atualizar um gênero
        /// </summary>
        /// <param name="id">Id do gênero a ser atualizado</param>
        /// <param name="genero">Objeto com os dados atualizados</param>
        /// <returns>Status code 201</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Genero genero)
        {
            try
            {
                _generoRepository.Atualizar(id, genero);

                return NoContent() ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}