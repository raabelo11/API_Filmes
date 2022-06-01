using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<AdicionaFilmesDto, Filme>();
            CreateMap<Filme, LerFilmesDto>();
            CreateMap<ModificaFilmesDto, Filme>();
        }
    }
}
