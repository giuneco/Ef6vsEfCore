using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DbContext;
using DataAccessLayer.Model;
using EFCore.Models;

namespace EFCore.Controllers
{
    public class BooksController : Controller
    {
        private readonly MyContext _context;

        public BooksController(MyContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Create
        public IActionResult Create(int libraryId)
        {
            var library = _context.Libraries.Find(libraryId);
            var model = new BookViewModel() {FatherLibrary = library};
            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,LibraryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Libraries", new {id = book.LibraryId});
            }
            return RedirectToAction("Details", "Libraries", new {id = book.LibraryId});
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["categories"] = await _context.Category.ToListAsync();
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,LibraryId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Libraries", new { id = book.LibraryId });
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(int categoryId, int bookId)
        {
            // To add sons to an entity, the easiest way is to specify the foreign key
            var newLink = new BookCategory() {BookId = bookId, CategoryId = categoryId};

            // Add can also be called on context directly
            _context.Add(newLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new {id = bookId});
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCategory(int categoryId, int bookId)
        {
            var book = _context.Books.Find(bookId);
            var bound = book.BookCategories.Single(c => c.CategoryId == categoryId);
            _context.Remove(bound);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = bookId });
        }
        //
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
