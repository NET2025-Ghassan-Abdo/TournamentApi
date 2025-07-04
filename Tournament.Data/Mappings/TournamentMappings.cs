using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Dto;
using Tournaments.Core.Entities;

namespace Tournaments.Data.Mappings
{
    public class TournamentMappings:Profile
    {
        public TournamentMappings()
        {
            //Entities till Dto
            CreateMap<Tournament, TournamentDto>();
            CreateMap<Game, GameDto>();
            //Dto to Entities (Put, Post indata)
            CreateMap<TournamentDto, Tournament>()
                .ForMember(dest => dest.Games, opt => opt.Ignore());
            CreateMap<GameDto, Game>();
        }
    }
}
