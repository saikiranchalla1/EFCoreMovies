﻿using EFCoreMovies.Entities;
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
            return await context.Genres.ToListAsync(); // this will be Asynchronous
        }
    }
}