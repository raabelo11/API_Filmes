using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
using FilmesAPI.Data;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost] //Criar recurso novo.
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges(); // salva o json no banco de dados
            return CreatedAtAction(nameof(RetornaFilmesPorId), new { Id = filme.Id }, filme); //informa aonde o json foi gravado local onde pode ser acessado
            
        }

        [HttpGet] //Retorna os dados do filme.
        public IEnumerable<Filme> RetornaFilmes()
        {
            return(_context.Filmes);
        }

        [HttpGet("{id}")] // identificador do ID do filme desejado a ser retornado: ex: localhost:500/filme/1
        public IActionResult RetornaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);  // retorna null quando não existe filme com ID indicado.
            if(filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

    }
}
