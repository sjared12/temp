// File: Controllers/GalleryController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeddingShare.Data;
using WeddingShare.Models;

namespace WeddingShare.Controllers
{
    [Authorize(Roles = "GlobalAdmin,GroupAdmin,GalleryAdmin")]
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null || user.Password != model.Password) // You should hash passwords
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            // Log the user in
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookies", principal);

            return RedirectToAction("Index");
        }

        public IActionResult CustomHomepage(int id)
        {
            var gallery = _context.Galleries.FirstOrDefault(g => g.GalleryId == id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Users
                .Include(u => u.GroupIds)
                .FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            IQueryable<Gallery> galleries = _context.Galleries;

            if (User.IsInRole("GroupAdmin"))
            {
                galleries = galleries.Where(g => user.GroupIds.Contains(g.GroupId));
            }

            return View(await galleries.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await GetCurrentUserAsync();
            var gallery = await _context.Galleries.FirstOrDefaultAsync(g => g.GalleryId == id);

            if (gallery == null)
            {
                return NotFound();
            }

            if (User.IsInRole("GroupAdmin") && !user.GroupIds.Contains(gallery.GroupId))
            {
                return Forbid();
            }
            if (User.IsInRole("GalleryAdmin") && !user.GalleryIds.Contains(gallery.GalleryId))
            {
                return Forbid();
            }

            return View(gallery);
        }

        [Authorize(Roles = "GlobalAdmin,GroupAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "GlobalAdmin,GroupAdmin")]
        public async Task<IActionResult> Create([Bind("Name,Description,CustomHomepageContent,GroupId")] Gallery gallery)
        {
            var user = await GetCurrentUserAsync();

            if (User.IsInRole("GroupAdmin") && !user.GroupIds.Contains(gallery.GroupId))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        [Authorize(Roles = "GlobalAdmin,GroupAdmin,GalleryAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var gallery = await _context.Galleries.FindAsync(id);

            if (gallery == null)
            {
                return NotFound();
            }

            if (User.IsInRole("GroupAdmin") && !user.GroupIds.Contains(gallery.GroupId))
            {
                return Forbid();
            }
            if (User.IsInRole("GalleryAdmin") && !user.GalleryIds.Contains(gallery.GalleryId))
            {
                return Forbid();
            }

            return View(gallery);
        }

        [HttpPost]
        [Authorize(Roles = "GlobalAdmin,GroupAdmin,GalleryAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryId,Name,Description,CustomHomepageContent,GroupId")] Gallery gallery)
        {
            var user = await GetCurrentUserAsync();

            if (id != gallery.GalleryId)
            {
                return NotFound();
            }

            if (User.IsInRole("GroupAdmin") && !user.GroupIds.Contains(gallery.GroupId))
            {
                return Forbid();
            }
            if (User.IsInRole("GalleryAdmin") && !user.GalleryIds.Contains(gallery.GalleryId))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                _context.Update(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var gallery = await _context.Galleries.FirstOrDefaultAsync(g => g.GalleryId == id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery != null)
            {
                _context.Galleries.Remove(gallery);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}