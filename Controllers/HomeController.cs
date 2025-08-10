using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebPharmecyDiscountdemo.Data;
using WebPharmecyDiscountdemo.Models;
using WebPharmecyDiscountdemo.Models.DTOs;
using WebPharmecyDiscountdemo.Models.Entities;

namespace WebPharmecyDiscountdemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        // READ - List all discount codes
        public async Task<IActionResult> Index()
        {
            var DicountCodeEntityList = await _appDbContext.TlbDiscountCodes.ToListAsync();

            var discountCodeList = new List<DTODiscountCode>();
            foreach (var d in DicountCodeEntityList)
            {
                var dicountCode = new DTODiscountCode()
                {
                    Id = d.Id,
                    Code = d.Code,
                    Value = d.Value,
                    ValueType = d.ValueType,
                    TotalUsage = d.TotalUsage,
                    PerCustomerUsage = d.PerCustomerUsage,
                    AppliesToAll = d.AppliesToAll,
                    AppliesToUserIds = d.AppliesToUserIds.Split(","),
                    MinimumCartValue = d.MinimumCartValue,
                    StartAt = d.StartAt,
                    EndAt = d.EndAt,
                    UseCount = d.UseCount,
                    IsActive = d.IsActive
                };
                discountCodeList.Add(dicountCode);
            }
            return View(discountCodeList);
        }

        // CREATE - Show form
        public IActionResult Create()
        {


            var customers = _appDbContext.Users
       .Select(u => new SelectListItem
       {
           Value = u.Id, // Identity's primary key is string
           Text = u.FirstName + " " + u.LastName
       })
       .ToList();

            ViewBag.CustomerList = customers;

            return View();
        }

        // CREATE - Save form
        [HttpPost]
        public async Task<IActionResult> Create(DTODiscountCode model)
        {
            if (ModelState.IsValid)
            {
                var entity = new DiscountCode()
                {
                    Code = model.Code,
                    Value = model.Value,
                    ValueType = model.ValueType,
                    TotalUsage = model.TotalUsage,
                    PerCustomerUsage = model.PerCustomerUsage,
                    AppliesToAll = model.AppliesToAll,
                    AppliesToUserIds = string.Join(",", model.AppliesToUserIds),
                    MinimumCartValue = model.MinimumCartValue,
                    StartAt = (DateTime)model.StartAt,
                    EndAt = (DateTime)model.EndAt,
                    UseCount = model.UseCount,
                    CreatedAt = DateTime.Now,
                    IsActive = model.IsActive
                };

                _appDbContext.TlbDiscountCodes.Add(entity);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // UPDATE - Show form
        public async Task<IActionResult> Edit(int id)
        {
            var d = await _appDbContext.TlbDiscountCodes.FindAsync(id);
            if (d == null) return NotFound();

            var dto = new DTODiscountCode()
            {
                Id = d.Id,
                Code = d.Code,
                Value = d.Value,
                ValueType = d.ValueType,
                TotalUsage = d.TotalUsage,
                PerCustomerUsage = d.PerCustomerUsage,
                AppliesToAll = d.AppliesToAll,
                AppliesToUserIds = d.AppliesToUserIds.Split(","),
                MinimumCartValue = d.MinimumCartValue,
                StartAt = d.StartAt,
                EndAt = d.EndAt,
                UseCount = d.UseCount,
                IsActive = d.IsActive
            };
            ViewBag.CustomerList = _appDbContext.Users
    .Select(u => new SelectListItem
    {
        Value = u.Id,
        Text = u.FirstName + " " + u.LastName
    })
    .ToList();
            return View(dto);
        }

        // UPDATE - Save changes
        [HttpPost]
        public async Task<IActionResult> Edit(int id, DTODiscountCode model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var d = await _appDbContext.TlbDiscountCodes.FindAsync(id);
                if (d == null) return NotFound();

                // Unlimited logic
                if (!model.TotalUsage.HasValue || model.TotalUsage <= 0)
                    model.TotalUsage = null;
                if (!model.PerCustomerUsage.HasValue || model.PerCustomerUsage <= 0)
                    model.PerCustomerUsage = null;

                d.Code = model.Code;
                d.Value = model.Value;
                d.ValueType = model.ValueType;
                d.TotalUsage = model.TotalUsage;
                d.PerCustomerUsage = model.PerCustomerUsage;
                d.AppliesToAll = model.AppliesToAll;
                d.AppliesToUserIds = model.AppliesToUserIds != null
                    ? string.Join(",", model.AppliesToUserIds)
                    : string.Empty;
                d.MinimumCartValue = model.MinimumCartValue;
                d.StartAt = (DateTime)model.StartAt;
                d.EndAt = (DateTime)model.EndAt;
                d.UseCount = model.UseCount;
                d.IsActive = model.IsActive;

                _appDbContext.Update(d);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopulate customer list if validation fails
            ViewBag.CustomerList = _appDbContext.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.FirstName + " " + u.LastName
                })
                .ToList();

            return View(model);
        }

        // DELETE - Confirm page
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _appDbContext.TlbDiscountCodes.FindAsync(id);
            if (d == null) return NotFound();

            return View(d);
        }

        // DELETE - Execute
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d = await _appDbContext.TlbDiscountCodes.FindAsync(id);
            if (d == null) return NotFound();

            _appDbContext.TlbDiscountCodes.Remove(d);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
