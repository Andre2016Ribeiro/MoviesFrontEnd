using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

using appmodel.Models;

namespace appmodel.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient _context;

        public AuthorsController()
        {
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri("https://moviesapi1.azurewebsites.net/api/authors/");
            _context = client;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
              return View(await _context.GetFromJsonAsync<List<Author>>(""));
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var author = await _context.GetFromJsonAsync<Author>(id.ToString());
            if (id == null || author == null)
            {
                return NotFound();
            }

           
                
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Name")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _context.PostAsJsonAsync<Author>("",author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var author=await _context.GetFromJsonAsync<Author>(id.ToString());
            if (id == null || author == null)
            {
                return NotFound();
            }

            
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,Name")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.PutAsJsonAsync<Author>(id.ToString(), author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var author = await _context.GetFromJsonAsync<Author>(id.ToString());

            if (id == null || author == null)
            {
                return NotFound();
            }

           
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteAsync(id.ToString());
            return RedirectToAction(nameof(Index));
        }

        
    }
}
