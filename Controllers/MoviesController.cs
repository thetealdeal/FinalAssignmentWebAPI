using Microsoft.AspNetCore.Mvc;
using FinalAssignmentWebAPI.Models;
using AutoMapper;
using FinalAssignmentWebAPI.Data;
using FinalAssignmentWebAPI.Data.DTOs.MoviesDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using MessagePack;
using FinalAssignmentWebAPI.Data.DTOs.CharacterDTOs;
using NuGet.Packaging;

namespace FinalAssignmentWebAPI.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(MoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }



        /// <summary>
        /// Returns all movies
        /// </summary>
        /// <param> Returns all movies </param>
        /// <returns></returns>
        [HttpGet("[action]/all")]
        public async Task<ActionResult<List<MovieReadDTO>>> GetAll()
        {
            // Query db
            var movieList = await _context.Movies.Include(c => c.Characters).ToListAsync();

            // convert into Movie read dtos

            var movieDtoList = _mapper.Map<List<MovieReadDTO>>(movieList);

            // return list of Movies

            return Ok(movieDtoList);
        }



        /// <summary>
        /// Returns a single movie by id
        /// </summary>
        /// <param name="id">The id of the movie you want returned </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetById(int id)
        {
            var domainMovie = await _context.Movies.FindAsync(id);

            if (domainMovie == null)
            {
                return NotFound();
            }

            var MovieDTO = _mapper.Map<MovieReadDTO>(domainMovie);

            return Ok(MovieDTO);
        }




        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="id">The id of the movie which you want to update </param>
        /// /// <param name="updatedMovie">The information you want to update. Fill in all the fields, Id's need to match. </param>
        /// <returns></returns>
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDTO updatedMovie) //int id, 
        {
            if (id != updatedMovie.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == true)
            {
                var domainMovie = _mapper.Map<Movie>(updatedMovie);

                _context.Entry(domainMovie).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return BadRequest("Object is invalid");
            }
        }




        /// <summary>
        /// Creates a new Movie
        /// </summary>
        /// <param name="newMovieDTO">The new movie information. Assigning characters and franchise can be done elsewhere </param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<MovieReadDTO>> CreateMovie([FromBody] MovieCreateDTO newMovieDTO)
        {
            if (ModelState.IsValid == true)
            {
                var newDomainMovie = _mapper.Map<Movie>(newMovieDTO);

                await _context.Movies.AddAsync(newDomainMovie);
                await _context.SaveChangesAsync();

                var readDTO = _mapper.Map<MovieReadDTO>(newDomainMovie);
                return CreatedAtAction(nameof(GetById), new { id = readDTO.Id }, readDTO);
            }
            else
            {
                return BadRequest();
            }
        }




        /// <summary>
        /// Deletes a Movie
        /// </summary>
        /// <param name="id">The Id of the movie you want to delete </param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }




        //CUSTOM ENDPOINTS
        /// <summary>
        /// Returns the characters of a Movie
        /// </summary>
        /// <param name="id">The movie Id which you want to get the Characters of </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersOfMovie(int id)
        {

            var domainMovie = _context.Movies. // selects a Movie matching the given Id, including the Characters matching the movie  
                                Where(m => m.Id == id)
                                .Include(c => c.Characters)
                                .FirstOrDefault();

            if (domainMovie == null)
            {
                return NotFound();
            }

            var moviesCharacters = _mapper.Map<List<CharacterReadDTO>>(domainMovie.Characters); // gets a list of the movie characters via readDTO
            return Ok(moviesCharacters);
        }




        /// <summary>
        /// Assigns Characters to an Movie
        /// </summary>
        /// <param name="id">The movie Id which you want to add Characters too </param>
        /// <param name="characterId">Id's of the characters being assigned, in the form of a array i.e. [1, 2, 3, etc..]</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateMovieCharacters(int id, [FromBody] int[] characterId)
        {
            // select the character list from the movie
            var domainMovie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (domainMovie == null)
            {
                return NotFound();
            }

            // select a list of characters from the database using the given Id list
            var domainCharacters = await _context.Characters
                .Where(c => characterId
                .Contains(c.Id))
                .ToListAsync();

            // Update the characters of the movie (
            domainMovie.Characters = domainCharacters;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
