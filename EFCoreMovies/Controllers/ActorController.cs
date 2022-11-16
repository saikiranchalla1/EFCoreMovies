using EFCoreMovies.DTOs;
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


        [HttpGet]
        public async Task<IEnumerable<Actor>> GetProjection(int page = 1, int recordsToTake = 2)
        {
            return await context.Actors.AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(a => new Actor { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth})
                .Paginate(page, recordsToTake)
                .ToListAsync();
            // this will set biography to null, which can be avoided using DTOs shows in next method
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> GetProjectionUsingDTO(int page = 1, int recordsToTake = 2)
        {
            return await context.Actors.AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(a => new ActorDTO { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }


        // Project to retrieve only IDs of the actors
        [HttpGet("ids")]
        public async Task<IEnumerable<int>> GetIds()
        {
            return await context.Actors.Select(a => a.Id).ToListAsync();
        }
    }
}
