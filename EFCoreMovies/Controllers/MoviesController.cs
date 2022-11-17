using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await context.Movies
                .Include(m => m.Genres)
                .Include(m => m.CinemaHalls)
                    .ThenInclude(ch => ch.Cinema)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetForMovieDTO(int id)
        {
            var movie = await context.Movies
                .Include(m => m.Genres)
                .Include(m => m.CinemaHalls)
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.MoviesActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList(); // <= removies duplicate cinemas



            return movieDTO;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetForFilteringAndOrdering(int id)
        {
            var movie = await context.Movies
                .Include(m => m.Genres.OrderByDescending(g => g.Name).Where(g => !g.Name.Contains("m")))
                .Include(m => m.CinemaHalls.OrderByDescending(ch => ch.Cinema.Name))
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.MoviesActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("automapper/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithAutoMapper(int id) // try the request by commenting Genres in MoviesDTO
        {
            var movieDTO = await context.Movies
               .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null)
            {
                return NotFound();
            }


            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("selectloading/{id:int}")]
        public async Task<ActionResult> GetSelectLoading(int id)
        {
            var movieDTO = await context.Movies.Select(m => new
            {
                Id = m.Id,
                Title = m.Title,
                Genres = m.Genres.Select(g => g.Name).OrderByDescending(n => n).ToList()
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null)
            {
                return NotFound();
            }

            return Ok(movieDTO);
        }

        [HttpGet("explicitLoading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetExplicit(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            await context.Entry(movie).Collection(p => p.Genres).LoadAsync();
            
            // var genresCount = await context.Entry(movie).Collection(p => p.Genres).Query().CountAsync(); // <= We're not limited to loading the data. We can also use this for queries such as Counts

            var movieDTO = mapper.Map<MovieDTO>(movie);

            return Ok(new
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                // GenresCount = genresCount
            });
        }


    }
}
