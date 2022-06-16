using AvProduto.Data;
using AvProduto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvProduto.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _appContext;

        public ProdutoController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        public IActionResult Index()
        {
            var allTasks = _appContext.Produtos.ToList();
            return View(allTasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nome, Modelo, categoria")] Produto produto)
        {

            if (ModelState.IsValid)
            {
                _appContext.Add(produto);
                await _appContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long? Id)
        {


            if (Id == null)
            {
                return NotFound();
            }

            var produto = await _appContext.Produtos.FindAsync(Id);

            if (produto == null)
            {
                return BadRequest();
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var produto = await _appContext.Produtos.FindAsync(Id);
            if (produto == null)
            {
                return BadRequest();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? Id, Produto produto)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dadosAntigos = _appContext.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == Id);

            if (ModelState.IsValid)
            {
                _appContext.Update(produto);
                await _appContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var produto = await _appContext.Produtos.FindAsync(Id);
            if (produto == null)
            {
                return BadRequest();
            }
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? Id)
        {
                  
             var produto = await _appContext.Produtos.FindAsync(Id);
            ViewData["Message"] = "$deseja apagar o produto.Nome";
            _appContext.Produtos.Remove(produto);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
