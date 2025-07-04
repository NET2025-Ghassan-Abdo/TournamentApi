using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Tournaments.Core.Entities;
using Tournaments.Core.Repositories;
using Tournaments.Core.Dto;
using Microsoft.AspNetCore.Identity;


namespace Tournaments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public TournamentsController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments(bool IncludeGames = false)
        {
            var tournaments = await _uow.TournamentRepository.GetAllAsync(IncludeGames);
            var dtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);    
            return Ok(dtos);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            if (!await _uow.TournamentRepository.AnyAsync(id))
                return NotFound();
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            var dto = _mapper.Map<TournamentDto>(tournament);
            return Ok(dto);
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournament([FromBody]TournamentDto dto)
        {
            if (!ModelState.IsValid)
               return BadRequest(ModelState);
            var tournament = _mapper.Map<Tournament>(dto);
           
            _uow.TournamentRepository.Add(tournament);
          
            await _uow.CompletAsync();

            var resultDto = _mapper.Map<TournamentDto>(tournament);
            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, resultDto);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, [FromBody]TournamentDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!await _uow.TournamentRepository.AnyAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tournament = _mapper.Map<Tournament>(dto);
            _uow.TournamentRepository.Update(tournament);
            try
            {
                await _uow.CompletAsync();
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok(dto);
        }

        

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            if (!await _uow.TournamentRepository.AnyAsync(id))
                return NotFound();
            var tournment = await _uow.TournamentRepository.GetAsync(id);
            _uow.TournamentRepository.Remove(tournment);
            await _uow.CompletAsync();
            return NoContent();           
        }

        //Patch: api/Tournaments/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTournament(int id, [FromBody] JsonPatchDocument<TournamentDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            // Hämta befintlig entitet
            var existing = await _uow.TournamentRepository.GetAsync(id);
            if (existing == null)
                return NotFound();

            // Mappa till DTO för patchning
            var dto = _mapper.Map<TournamentDto>(existing);

            // Applicera patch mot DTO och validera
            patchDoc.ApplyTo(dto, ModelState);
            if (!TryValidateModel(dto))
                return BadRequest(ModelState);

            // Mappa tillbaka till entitet
            _mapper.Map(dto, existing);

            try
            {
                await _uow.CompletAsync();
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok(dto);
        }

    }
}
