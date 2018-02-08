using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XYApp.Data;
using XYApp.Models;

namespace XYApp.Controllers
{
    public class POIsController : Controller
    {
        private readonly POIContexto _context;

        public POIsController(POIContexto context)
        {
            _context = context;
        }

        // GET: POIs
        public async Task<IActionResult> Index(string ordem, int? pntX, int? pntY, int? dist)
        {
            ViewData["NomeParm"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["pntX"] = pntX;
            ViewData["pntY"] = pntY;
            ViewData["dist"] = dist;

            var pois = from p in _context.POIs
                             select p;

            if (pntX != null && pntY != null && dist != null)
            {
                // formula da distancia entre 2 pontos == raiz quadrada((x2 - x1) + (y2 - y1))
                pois = from p in _context.POIs
                       where Math.Sqrt(Math.Pow(Convert.ToDouble(p.PntX - pntX), 2) + Math.Pow(Convert.ToDouble(p.PntY - pntY), 2)) <= dist
                       select p;
            }

            switch (ordem)
            {
                case "nome_desc":
                    pois = pois.OrderByDescending(p => p.NomePOI);
                    break;      
                default:
                    pois = pois.OrderBy(p => p.NomePOI);
                    break;
            }
            
            return View(await pois.AsNoTracking().ToListAsync());
        }

        // GET: POIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poi = await _context.POIs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (poi == null)
            {
                return NotFound();
            }

            return View(poi);
        }

        // GET: POIs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: POIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomePOI,PntX,PntY")] POI pOI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(pOI);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Logar o erro (descomente a variável ex e escreva um log
                ModelState.AddModelError("", "Não foi possível salvar. " +
                    "Tente novamente, e se o problema persistir " +
                    "chame o suporte.");
            }
            return View(pOI);
        }

        // GET: POIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poi = await _context.POIs.SingleOrDefaultAsync(m => m.ID == id);
            if (poi == null)
            {
                return NotFound();
            }
            return View(poi);
        }

        // POST: POIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atualizarPOI = await _context.POIs.SingleOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<POI>(
                 atualizarPOI,
                 "",
                 s => s.NomePOI, s => s.PntX, s => s.PntY))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Logar o erro (descomente a variável ex e escreva um log
                    ModelState.AddModelError("", "Não foi possível salvar. " +
                        "Tente novamente, e se o problema persistir " +
                        "chame o suporte.");
                }
            }
            return View(atualizarPOI);
        }

        // GET: POIs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var poi = await _context.POIs
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (poi == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "A exclusão falhou. Tente novamente e se o problema persistir " +
                    "contate o suporte.";
            }
            return View(poi);
        }

        // POST: POIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poi = await _context.POIs
              .AsNoTracking()
              .SingleOrDefaultAsync(m => m.ID == id);

            if (poi == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.POIs.Remove(poi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //TODO Logar o erro
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool POIExists(int id)
        {
            return _context.POIs.Any(e => e.ID == id);
        }
    }
}
