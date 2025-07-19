using Bee_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bee_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly BeeShopDbContext _context;

        public CategoryController(BeeShopDbContext context)
        {
            _context = context;
        }

        // GET: Danh sách danh mục
        public IActionResult Index()
        {
            var categories = _context.Categories
                .Include(c => c.Children)
                .OrderBy(c => c.CategoryPosition)
                .ToList();

            return View(categories);
        }

        // GET: Thêm danh mục cha
        public IActionResult Create()
        {
            return View();
        }

        // POST: Thêm danh mục cha
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.SupplyId = null;
                if (string.IsNullOrEmpty(category.Slug))
                    category.Slug = GenerateSlug(category.CategoryName);

                if (_context.Categories.Any(c => c.Slug == category.Slug))
                {
                    ModelState.AddModelError("Slug", "Slug đã tồn tại.");
                    return View(category);
                }

                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Thêm danh mục con
        public IActionResult CreateChild(int supplyId)
        {
            ViewBag.DanhMucCha = _context.Categories
                .Where(c => c.SupplyId == null)
                .OrderBy(c => c.CategoryName)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

            var model = new Category
            {
                SupplyId = supplyId
            };

            return View(model);
        }

        // POST: Thêm danh mục con
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateChild(Category category)
        {
            ViewBag.DanhMucCha = _context.Categories.Where(c => c.SupplyId == null).ToList();

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(category.Slug))
                    category.Slug = GenerateSlug(category.CategoryName);

                if (_context.Categories.Any(c => c.Slug == category.Slug))
                {
                    ModelState.AddModelError("Slug", "Slug đã tồn tại.");
                    return View(category);
                }

                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Sửa danh mục
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            ViewBag.DanhMucCha = _context.Categories
                .Where(c => c.SupplyId == null && c.Id != id)
                .ToList(); 

            return View(category);
        }

        // POST: Sửa danh mục
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories
                .Include(c => c.Children)
                .FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            // Set SupplyId = null cho danh mục con (nếu có)
            if (category.Children?.Any() == true)
            {
                foreach (var child in category.Children)
                {
                    child.SupplyId = null;
                }
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: Xóa danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.Categories
                .Include(c => c.Children)
                .FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            if (category.Children?.Any() == true)
            {
                foreach (var child in category.Children)
                {
                    child.SupplyId = null;
                }
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Slug Generator
        private string GenerateSlug(string input)
        {
            if (string.IsNullOrEmpty(input)) return "category";
            return input.Trim().ToLower().Replace(" ", "-");
        }
    }
}
