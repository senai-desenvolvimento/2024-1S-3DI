using api_filmes_senai.Domains;
using api_filmes_senai.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeController(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        /// <summary>
        /// Endpoint para listar todos os filmes
        /// </summary>
        /// <returns>Lista dos filmes</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Filme> listaDeFilmes = _filmeRepository.Listar();

                return Ok(listaDeFilmes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Filme a ser cadastrado</param>
        /// <returns>Status code 201</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Filme novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);

                return Created();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Endpoint para buscar um filme pelo id
        /// </summary>
        /// <param name="id">Id do filme a ser buscado</param>
        /// <returns>Filme buscado</returns>
        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Filme filmeBuscado = _filmeRepository.BuscarPorId(id);

                return Ok(filmeBuscado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Endpoint para atualizar um filme
        /// </summary>
        /// <param name="id">Id do filme a ser atualizado</param>
        /// <param name="filme">Filme com dados atualizados</param>
        /// <returns>Status code 204</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Filme filme) 
        {
            try
            {
                _filmeRepository.Atualizar(id, filme);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para excluir um filme
        /// </summary>
        /// <param name="id">Id do filme a ser excluído</param>
        /// <returns>Status code 204</returns>
        [Authorize] 
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _filmeRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para filtrar os filmes pelo seu gênero
        /// </summary>
        /// <param name="id">Id do gênero</param>
        /// <returns>Lista de filmes filtrados pelo gênero</returns>
        [HttpGet("ListarPorGenero/{id}")]
        public IActionResult GetByGenero(Guid id)
        {
            try
            {
                List<Filme> listaDeFilmePorGenero = _filmeRepository.ListarPorGenero(id);

                return Ok(listaDeFilmePorGenero);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}