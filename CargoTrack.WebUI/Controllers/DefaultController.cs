using CargoTrack.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoTrack.WebUI.Controllers
{
    public class DefaultController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            if (TempData["error"] != null)
            {
                ViewBag.ErrorMessage = TempData["error"].ToString();
            }
            return View();
        }

        public async Task<IActionResult> CargoDetails(string trackCode)
        {
            var cargo = await _context.Cargo
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.OriginBranch)
                .Include(x => x.DestinationBranch)
                .FirstOrDefaultAsync(x => x.TrackCode == trackCode);

            if (cargo == null)
            {
                TempData["error"] = "Bu takip numarası ile bir kargo bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(cargo);
        }
    }
}
