using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("/api.genres")]
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext context;

        public GenresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            // return context.Genres.ToList(); // <- This is Synchronous
            return await context.Genres.AsNoTracking().ToListAsync(); // this will be Asynchronous
            // using AsNoTracking improves the performance of the application
            // use AsTracking to allow for tracking
        }

        [HttpGet("first")]
        // public async Task<Genre> GetFirst()
        public async Task<ActionResult<Genre>> GetFirst()
        {
            // return await context.Genres.FirstAsync();

            // return await context.Genres.FirstAsync(g => g.Name.Contains("m")); // <= return first genre that contains the letter "m"

            // return await context.Genres.FirstAsync(g => g.Name.Contains("z")); // <= Throws an exception because there is no Genre with the letter 'z'
            // return await context.Genres.FirstOrDefaultAsync(g => g.Name.Contains("z")); // <= Returns null

            var genre = await context.Genres.FirstOrDefaultAsync(g => g.Name.Contains("z"));

            if (genre is null)
            {
                return NotFound(); // for this to work change return type to Task<ActionResult<Genre>>
            }

            return genre;
        }
    }
}
