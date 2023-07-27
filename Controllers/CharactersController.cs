using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalAssignmentWebAPI.Data;
using FinalAssignmentWebAPI.Models;
using AutoMapper;
using FinalAssignmentWebAPI.Data.DTOs.CharacterDTOs;

namespace FinalAssignmentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;

        public CharactersController(MoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        /// <summary>
        /// Returns all characters
        /// </summary>
        /// <param> Returns all characters </param>
        /// <returns></returns>
        [HttpGet("[action]/all")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
        {
          if (_context.Characters == null)
          {
              return NotFound();
          }

            var characters = await _context.Characters.Include(m => m.Movies).ToListAsync();

            var readCharacters = _mapper.Map<List<CharacterReadDTO>>(characters);

            return readCharacters;
        }




        /// <summary>
        /// Returns a single character by id
        /// </summary>
        /// <param name="id">The id of the character you want returned </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<CharacterReadDTO?>> GetCharacter(int id)
        {
          if (_context.Characters == null)
          {
              return NotFound();
          }

            var character = await _context.Characters.
                                        Where(c => c.Id == id)
                                       .Include(c => c.Movies)
                                       .FirstOrDefaultAsync();

            var readCharacters = _mapper.Map<CharacterReadDTO?>(character); 

            if (character == null)
            {
                return NotFound();
            }

            return readCharacters;
        }




        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="id">The id of the character which you want to update </param>
        /// /// <param name="characterDto">The information you want to update. Fill in all the fields, Id's need to match. </param>
        /// <returns></returns>
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterUpdateDTO characterDto)
        {
            if (id != characterDto.Id)
            {
                return BadRequest();
            }

            var domainCharacters = _mapper.Map<Character>(characterDto);
            _context.Entry(domainCharacters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="newCharacterDto">The new character information. Assigning the character to a movie can be done elsewhere </param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Character>> CreateCharacter(CharacterCreateDTO newCharacterDto)
        {
            var domainCharacter = _mapper.Map<Character>(newCharacterDto);

            _context.Characters.Add(domainCharacter);
            await _context.SaveChangesAsync();

            var characterReadDTO = _mapper.Map<CharacterReadDTO>(domainCharacter);

            return CreatedAtAction(nameof(GetCharacter), new { id = characterReadDTO.Id }, characterReadDTO);

        }




        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="id">The Id of the character you want to delete </param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return (_context.Characters?.Any(e => e.Id == id)).GetValueOrDefault();
        }




    }
}
