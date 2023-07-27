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
using FinalAssignmentWebAPI.Data.DTOs.FranchiseDTOs;
using FinalAssignmentWebAPI.Data.DTOs.CharacterDTOs;
using FinalAssignmentWebAPI.Data.DTOs.MoviesDTOs;

namespace FinalAssignmentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;

        public FranchisesController(MoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        /// <summary>
        /// Returns all franchises
        /// </summary>
        /// <param> Returns all franchises </param>
        /// <returns></returns>
        [HttpGet("[action]/all")]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
        {
          if (_context.Franchises == null)
          {
              return NotFound();
          }

            var franchises = await _context.Franchises.Include(f => f.Movies).ToListAsync();

            var readFranchises = _mapper.Map<List<FranchiseReadDTO>>(franchises);

            return readFranchises;
        }




        /// <summary>
        /// Returns a single franchise by id
        /// </summary>
        /// <param name="id">The id of the franchise you want returned </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<FranchiseReadDTO?>> GetFranchise(int id)
        {
          if (_context.Franchises == null)
          {
              return NotFound();
          }
            var franchise = await _context.Franchises.
                                        Where(f => f.Id == id)
                                       .Include(f => f.Movies)
                                       .FirstOrDefaultAsync();
            var readFranchises = _mapper.Map<FranchiseReadDTO?>(franchise);

            if (franchise == null)
            {
                return NotFound();
            }

            return readFranchises;
        }




        // PUT: api/Franchises/5
        /// <summary>
        /// Updates a franchise
        /// </summary>
        /// <param name="id">The id of the franchise which you want to update </param>
        /// /// <param name="franchiseDto">The information you want to update. Fill in all the fields, Id's need to match. </param>
        /// <returns></returns>
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseUpdateDTO franchiseDto)
        {
            if (id != franchiseDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == true)
            {
                var domainFranchises = _mapper.Map<Franchise>(franchiseDto);
                _context.Entry(domainFranchises).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return BadRequest("Object is invalid");
            }

        }




        /// <summary>
        /// Creates a new franchise
        /// </summary>
        /// <param name="newFranchiseDto">The new franchise information. Assigning movies to this franchise can be done elsewhere </param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO newFranchiseDto)
        {
            var domainFranchise = _mapper.Map<Franchise>(newFranchiseDto);

            _context.Franchises.Add(domainFranchise);
            await _context.SaveChangesAsync();

            var franchiseReadDTO = _mapper.Map<FranchiseReadDTO>(domainFranchise);

            return CreatedAtAction(nameof(GetFranchise), new { id = franchiseReadDTO.Id }, franchiseReadDTO);
        }




        /// <summary>
        /// Deletes a franchise
        /// </summary>
        /// <param name="id">The Id of the franchise you want to delete </param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (_context.Franchises == null)
            {
                return NotFound();
            }
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FranchiseExists(int id)
        {
            return (_context.Franchises?.Any(f => f.Id == id)).GetValueOrDefault();
        }




        //CUSTOM ENDPOINTS
        /// <summary>
        /// Returns the movies in a franchise
        /// </summary>
        /// <param name="id">The franchise Id which you want to get the movies of </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]

        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMoviesOfFranchise(int id)
        {

            var domainFranchise = await _context.Franchises
                                .Where(f => f.Id == id)
                                .Include(f => f.Movies)
                                .FirstOrDefaultAsync();

            if (domainFranchise == null)
            {
                return NotFound();
            }

            var franchiseMovies = _mapper.Map<List<MovieReadDTO>>(domainFranchise.Movies);
            return Ok(franchiseMovies);
        }




        /// <summary>
        /// Returns the characters in a franchise
        /// </summary>
        /// <param name="id">The franchise Id which you want to get the characters of </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersOfFranchise(int id)
        {
            var domainFranchise = await _context.Franchises
                                        .Where(f => f.Id == id)
                                        .Include(f => f.Movies) // get the franchise by id, get include the movies of the franchise, and then include the characters in the movies in the franchise
                                            .ThenInclude(f => f.Characters) //gets the characters of the movies that were included in the previous line
                                        .FirstOrDefaultAsync();

            if (domainFranchise == null)
            {
                return NotFound();
            }
            // Converts the content (characters) of the selection to a flat list
            var franchiseCharacters = domainFranchise.Movies
                                        .SelectMany(m => m.Characters) //gets all characters from movies in the franchise
                                        .ToList(); //converts these character objects to a list into a flat list
            franchiseCharacters.Distinct().ToList();

            var franchiseCharacterDTOs = _mapper.Map<List<CharacterReadDTO>>(franchiseCharacters);
            return Ok(franchiseCharacterDTOs);
        }




        /// <summary>
        /// Assigns movies to a franchise
        /// </summary>
        /// <param name="id">The franchise Id which you want to add movies too </param>
        /// <param name="movieId">Id's of the movies being assigned, in the form of a array i.e. [1, 2, 3, etc..]</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateFranchiseMovies(int id, [FromBody] int[]? movieId)
        {
            // select the movie list from the franchise
            var domainFranchise = await _context.Franchises
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (domainFranchise == null)
            {
                return NotFound();
            }

            // select a list of movies from the database using the given Id list
            var domainMovies = await _context.Movies
                .Where(c => movieId
                .Contains(c.Id))
                .ToListAsync();

            // Update the movies of the franchise
            domainFranchise.Movies = domainMovies;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
