using CargoTrack.Business.Services.Branches;
using CargoTrack.Business.Services.Cities;
using CargoTrack.DTO.DTOs.BranchDtos;
using CargoTrack.WebUI.Consts;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CargoTrack.WebUI.Areas.Admin.Controllers
{
    [Area(Area.Admin)]
    public class BranchController(
        IBranchService _branchService,
        ICityService _cityService) : Controller
    {
        // İlişkisel verileri getirir.
        // Create ve Edit ekranlarında kullanılacak.
        private async Task GetCitiesAsync()
        {
            var cities = await _cityService.GetAllAsync();

            var sortedCities = cities.OrderBy(c => c.Name).ToList();

            ViewBag.Cities = (from city in sortedCities
                              select new SelectListItem
                              {
                                  Value = city.Id.ToString(),
                                  Text = city.Name
                              }).ToList();
        }

        // GET: Admin/Branch
        // READ - Tüm şubeleri getirir.
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllAsync();

            return View(branches);
        }

        // GET: Admin/Branch/Create
        // Create sayfasını açar.
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCitiesAsync();

            return View();
        }

        // POST: Admin/Branch/Create
        // CREATE - Yeni şube oluşturur.
        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDto createBranchDto)
        {
            if (!ModelState.IsValid)
            {
                await GetCitiesAsync();

                return View(createBranchDto);
            }

            await _branchService.CreateAsync(createBranchDto);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Branch/Update/{id}
        // Güncellenecek veriyi getirir.
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
           await GetCitiesAsync();
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            var updateBranchDto = branch.Adapt<UpdateBranchDto>();
            return View(updateBranchDto);

        }

        // POST: Admin/Branch/Update
        // UPDATE - Şubeyi günceller.
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBranchDto updateBranchDto)
        {
            if (!ModelState.IsValid)
            {
                await GetCitiesAsync();

                return View(updateBranchDto);
            }

            await _branchService.UpdateAsync(updateBranchDto);

            return RedirectToAction(nameof(Index));
        }

      
        // POST: Admin/Branch/Delete/{id}
        // DELETE - Şubeyi siler.
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _branchService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}