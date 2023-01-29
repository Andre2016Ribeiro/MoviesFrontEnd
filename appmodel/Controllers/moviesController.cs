using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using appmodel.Models;
using Microsoft.AspNetCore.Authorization;

namespace appmodel.Controllers
{
    public class moviesController : Controller
    {
        private readonly HttpClient _context;

        public moviesController()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://moviesapi1.azurewebsites.net/api/movies/");
            _context = client;
        }

        [Authorize]
        // GET: movies
        public async Task<IActionResult> Index()
        {
              return View(await _context.GetFromJsonAsync<List<movie>>(""));
        }

        // GET: movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var movie=await _context.GetFromJsonAsync<movie>(id.ToString());
            if (id == null || movie == null)
            {
                return NotFound();
            }

            
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title")] movie movie)
        {
            if (ModelState.IsValid)
            {
                await _context.PostAsJsonAsync<movie>("", movie);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var movie = await _context.GetFromJsonAsync<movie>(id.ToString());
            if (id == null || movie == null)
            {
                return NotFound();
            }

            
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title")] movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.PutAsJsonAsync<movie>(id.ToString(),movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var movie = await _context.GetFromJsonAsync<movie>(id.ToString());
            if (id == null || movie == null)
            {
                return NotFound();
            }

            
                
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteAsync(id.ToString());
            return RedirectToAction(nameof(Index));
        }

      
    }
}
