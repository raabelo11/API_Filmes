using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using AutoMapper;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _automapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _automapper = mapper;
        }

        [HttpPost("Adiciona_filme")] //Criar recurso novo.
        public IActionResult AdicionaFilme([FromBody] AdicionaFilmesDto adFilmesDto)
        {
            Filme filme = _automapper.Map<Filme>(adFilmesDto);
            //auto map pega as informações da classe AdicionaFilmesDto e salva na classe filme.
            _context.Filmes.Add(filme);
            _context.SaveChanges(); // salva o json no banco de dados
            return CreatedAtAction(nameof(RetornaFilmesPorId), new { Id = filme.Id }, filme); //informa aonde o json foi gravado local onde pode ser acessado
            
        }

        [HttpGet("Retorna_filme")] //Retorna os dados do filme.
        public IEnumerable<Filme> RetornaFilmes()
        {
            return(_context.Filmes);
        }

        [HttpGet("Retorna_filmes {id}")] // identificador do ID do filme desejado a ser retornado: ex: localhost:500/filme/1
        public IActionResult RetornaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);  //recupera o filme já cadastrado, se for null = NotFound
            if(filme != null)
            {
                LerFilmesDto lerfilmesDto = _automapper.Map<LerFilmesDto>(filme);
                //auto map converte filme para LerFilmesDto.
                return Ok(lerfilmesDto);
            }
            return NotFound();

        }


        [HttpPut("Modifica_filme {id}")]
        public IActionResult ModificarFilme(int id, [FromBody] ModificaFilmesDto modFilmesDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            else
            {
                _automapper.Map(modFilmesDto, filme); 
                //auto map pega as informações inseridas em modFilmesDto e salva na classe filme.
                _context.SaveChanges();
                return NoContent();
            }
                
        }

        [HttpDelete("Deleta_filme")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); //recupera o filme já cadastrado, se for null = NotFound
            if (filme == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(filme);
                _context.SaveChanges();
                return NoContent();
            }
        }

    }
}
