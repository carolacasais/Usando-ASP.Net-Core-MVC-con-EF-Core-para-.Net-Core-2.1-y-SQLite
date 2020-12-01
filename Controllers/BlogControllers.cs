using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Models;
using EFCore.Data;

namespace EFCore.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;
        public BlogController(BlogContext context)
        {
            _context = context;

        }

        //acciones dentro del controlador para (c)rear, lee(R), act(U)alizar, borra(D)o de registros en la BBDD CRUD

        //(C)reacion

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        // lectu(R)a
        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = _context.Blogs.FirstOrDefault(d => d.BlogId == id);
            if (blog == null)
                return NotFound();
            return View(blog);
        }
        // act(U)alizacion
        [HttpPost]
        public IActionResult Edit(int? id, Blog blog) //action sera invocada desde el menu general de la web app
        {
            if (id == null)
                return NotFound();
            //buscar el registro

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();
            return View(blog);
        }
        // borra(D)o
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = _context.Blogs.FirstOrDefault(b => b.BlogId == id);  //LINQ
            if (blog == null)
                return NotFound();
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var blog = _context.Blogs.Find(id);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public bool BlogExists(int id)
        {
            return _context.Blogs.Any(e=>e.BlogId == id);
        }
    }
}

