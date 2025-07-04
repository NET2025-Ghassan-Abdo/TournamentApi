using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.Core.Dto;
using Tournaments.Core.Entities;
using Tournaments.Core.Repositories;



namespace Tournaments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public GamesController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        //{
        //    var games = await _uow.GameRepository.GetAllAsync();
        //    var dtos = _mapper.Map<IEnumerable<GameDto>>(games);
        //    return Ok(dtos);
        //}

        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames([FromQuery] string title = null)
        {
            IEnumerable<Core.Entities.Game> games;
            if (string.IsNullOrWhiteSpace(title))
                games = await _uow.GameRepository.GetAllAsync();
            else
                games = await _uow.GameRepository.GetByTitleAsync(title);


            var dtos = _mapper.Map<IEnumerable<GameDto>>(games);
            return Ok(dtos);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            if (!await _uow.GameRepository.AnyAsync(id))
                return NotFound();
            var game = await _uow.GameRepository.GetAsync(id);
            var dto = _mapper.Map<Game>(game);
            return Ok(dto);
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame([FromBody]GameDto dto)
        {
            var game = _mapper.Map<Game>(dto);
            _uow.GameRepository.Add(game);
            await _uow.CompletAsync();

            var resultDto = _mapper.Map<GameDto>(game);
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, resultDto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, [FromBody]GameDto dto)
        {
            if (id != dto.Id) 
                return BadRequest();
            if (!await _uow.GameRepository.AnyAsync(id))
                return NotFound();
            var game = _mapper.Map<Game>(dto);
            _uow.GameRepository.Update(game);
            await _uow.CompletAsync();
            return NoContent();          
        }

       

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            

            if (!await _uow.GameRepository.AnyAsync(id))
                return NotFound();

           var game = await _uow.GameRepository.GetAsync(id);
            _uow.GameRepository.Remove(game);
            await _uow.CompletAsync();
            return NoContent();
        }

        // PATCH: api/Games/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchGame(int id, [FromBody] JsonPatchDocument<GameDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var existing = await _uow.GameRepository.GetAsync(id);
            if (existing == null)
                return NotFound();

            var dto = _mapper.Map<GameDto>(existing);

            patchDoc.ApplyTo(dto, ModelState);
            if (!TryValidateModel(dto))
                return BadRequest(ModelState);

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
