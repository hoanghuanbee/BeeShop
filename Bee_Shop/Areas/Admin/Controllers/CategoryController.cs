using Bee_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

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

        public IActionResult Index()
        {
            var categories = _context.Categories
                .Include(c => c.Children)
                .OrderBy(c => c.CategoryPosition)
                .ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public IActionResult CreateChild(int supplyId)
        {
            ViewBag.DanhMucCha = _context.Categories
                .Where(c => c.SupplyId == null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateChild(Category category)
        {
            ViewBag.DanhMucCha = _context.Categories
                .Where(c => c.SupplyId == null)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

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

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            ViewBag.DanhMucCha = _context.Categories
                .Where(c => c.SupplyId == null && c.Id != id)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(category.Slug))
                    category.Slug = GenerateSlug(category.CategoryName);

                if (_context.Categories.Any(c => c.Slug == category.Slug && c.Id != category.Id))
                {
                    ModelState.AddModelError("Slug", "Slug đã tồn tại.");
                }
                else
                {
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Nếu lỗi, cần gán lại danh sách danh mục cha để không bị null
            ViewBag.DanhMucCha = _context.Categories
                .Where(c => c.SupplyId == null && c.Id != category.Id)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteParent(int id)
        {
            var category = _context.Categories
                .Include(c => c.Children)
                .FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            if (category.Children?.Any() == true)
            {
                TempData["Error"] = "Không thể xóa danh mục cha vì vẫn còn danh mục con!";
                return RedirectToAction(nameof(Index));
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Success"] = "Đã xóa danh mục cha thành công!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteChild(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["Success"] = "Đã xóa danh mục con thành công!";
            return RedirectToAction(nameof(Index));
        }




        // Generate SEO-friendly Slug
        private string GenerateSlug(string input)
        {
            if (string.IsNullOrEmpty(input)) return "category";

            input = input.ToLowerInvariant().Trim();

            // Remove accents
            input = RemoveDiacritics(input);

            // Replace spaces with hyphens
            input = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", "-");

            // Remove special characters
            input = System.Text.RegularExpressions.Regex.Replace(input, @"[^a-z0-9\-]", "");

            return input;
        }

        private string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var chars = normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
        }
    }
}
