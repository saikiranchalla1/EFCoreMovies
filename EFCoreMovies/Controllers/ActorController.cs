using EFCoreMovies.Entities;
using EFCoreMovies.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ActorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Actor>> Get(int page = 1, int recordsToTake = 2)
        {
            return await context.Actors.AsNoTracking()
                .OrderBy(g => g.Name)
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }

    }
}
