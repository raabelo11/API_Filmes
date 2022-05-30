using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo título é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Campo Diretor é obrigatório")]
        public string Diretor { get; set; }
        public string Genero { get; set; }
        [Range(1, 250, ErrorMessage = "Duração deve conter no mínimo 1 e no máximo 300 minutos")]
        public int Duracao { get; set; }
    }
}
