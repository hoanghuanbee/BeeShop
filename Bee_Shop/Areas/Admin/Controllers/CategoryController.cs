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

        public IActionResult Index(string? search, string? sort)
        {
            // Load toàn bộ danh mục bao gồm danh mục con
            var allCategories = _context.Categories
                .Include(c => c.Children)
                .AsEnumerable(); // Dùng AsEnumerable để xử lý logic phức hơn sau

            // Lọc theo từ khóa
            if (!string.IsNullOrEmpty(search))
            {
                // Lọc danh mục cha nếu tên chứa từ khóa hoặc có danh mục con khớp
                allCategories = allCategories.Where(c =>
                    c.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (c.Children != null && c.Children.Any(child =>
                        child.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase)))
                ).ToList();

                // Lấy thêm các danh mục con trùng tên nếu có
                var matchedChildren = _context.Categories
                    .Where(c => c.SupplyId != null && c.CategoryName.Contains(search))
                    .ToList();

                // Gộp cha của con được tìm
                foreach (var child in matchedChildren)
                {
                    var parent = _context.Categories.Include(p => p.Children).FirstOrDefault(p => p.Id == child.SupplyId);
                    if (parent != null && !allCategories.Any(c => c.Id == parent.Id))
                    {
                        allCategories = allCategories.Append(parent);
                    }
                }
            }

            // Sắp xếp
            allCategories = sort switch
            {
                "name_desc" => allCategories.OrderByDescending(c => c.CategoryName),
                "position" => allCategories.OrderBy(c => c.CategoryPosition ?? 0),
                "position_desc" => allCategories.OrderByDescending(c => c.CategoryPosition ?? 0),
                _ => allCategories.OrderBy(c => c.CategoryName)
            };

            return View(allCategories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterCategory(string? search, string? sort)
        {
            // Lấy danh mục cha cùng danh mục con
            var allParents = _context.Categories
                .Where(c => c.SupplyId == null)
                .Include(c => c.Children)
                .ToList();

            // Tạo danh sách kết quả
            var result = new List<Category>();

            foreach (var parent in allParents)
            {
                bool isParentMatch = string.IsNullOrEmpty(search) ||
                    parent.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase);

                bool hasMatchingChild = parent.Children != null &&
                    parent.Children.Any(child =>
                        child.CategoryName.Contains(search ?? "", StringComparison.OrdinalIgnoreCase));

                if (isParentMatch || hasMatchingChild)
                {
                    var filteredParent = new Category
                    {
                        Id = parent.Id,
                        CategoryName = parent.CategoryName,
                        CategoryPosition = parent.CategoryPosition,
                        Slug = parent.Slug,
                        SupplyId = parent.SupplyId,
                        Children = new List<Category>()
                    };

                    if (isParentMatch)
                    {
                        // Hiển thị tất cả con nếu cha khớp
                        filteredParent.Children = parent.Children.ToList();
                    }
                    else
                    {
                        // Chỉ hiển thị các con phù hợp
                        filteredParent.Children = parent.Children
                            .Where(c => c.CategoryName.Contains(search ?? "", StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    result.Add(filteredParent);
                }
            }

            // Sắp xếp
            result = sort switch
            {
                "name_desc" => result.OrderByDescending(c => c.CategoryName).ToList(),
                "position" => result.OrderBy(c => c.CategoryPosition ?? 0).ToList(),
                "position_desc" => result.OrderByDescending(c => c.CategoryPosition ?? 0).ToList(),
                _ => result.OrderBy(c => c.CategoryName).ToList()
            };

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.SupplyId = null;

                // Chuẩn hóa tên trước khi kiểm tra
                var normalizedName = category.CategoryName.Trim().ToLower();

                // 👉 Kiểm tra tên danh mục đã tồn tại (không phân biệt hoa thường)
                if (_context.Categories.Any(c => c.CategoryName.ToLower() == normalizedName && c.SupplyId == null))
                {
                    ModelState.AddModelError("CategoryName", "Tên danh mục cha đã tồn tại.");
                    return View(category);
                }

                // Tạo Slug nếu chưa có
                if (string.IsNullOrEmpty(category.Slug))
                    category.Slug = GenerateSlug(category.CategoryName);

                // 👉 Kiểm tra Slug trùng
                if (_context.Categories.Any(c => c.Slug == category.Slug))
                {
                    ModelState.AddModelError("Slug", "Slug đã tồn tại.");
                    return View(category);
                }

                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "✅ Đã thêm danh mục cha thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
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
                var normalizedName = category.CategoryName.Trim().ToLower();

                // ✅ Kiểm tra trùng tên trong cùng một danh mục cha
                if (_context.Categories.Any(c =>
                    c.SupplyId == category.SupplyId &&
                    c.CategoryName.ToLower() == normalizedName))
                {
                    ModelState.AddModelError("CategoryName", "Tên danh mục con đã tồn tại trong danh mục cha này.");
                    return View(category);
                }

                // Tạo slug nếu chưa có
                if (string.IsNullOrEmpty(category.Slug))
                    category.Slug = GenerateSlug(category.CategoryName);

                // ✅ Kiểm tra trùng slug
                if (_context.Categories.Any(c => c.Slug == category.Slug))
                {
                    ModelState.AddModelError("Slug", "Slug đã tồn tại.");
                    return View(category);
                }

                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "✅ Đã thêm danh mục con thành công!";
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

            input = input.Replace("đ", "d").Replace("Đ", "D");

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
