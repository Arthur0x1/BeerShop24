using AutoMapper;
using Beershop24.Services;

using Beershop24.ViewModels;
using Beershop24.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Beershop24.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Beershop24.Controllers
{
	public class BeerController : Controller
	{
		private readonly IService<Beer> _beerService;
		private readonly IService<Brewery> _breweryService;
		private readonly IService<Variety> _varietyService;

		private readonly IMapper _mapper;

		public BeerController(
			IMapper mapper, IService<Beer> beerService, IService<Brewery> breweryService, IService<Variety> varietyService)
		{
			_mapper = mapper;
			_beerService = beerService;
			_breweryService = breweryService;
			_varietyService = varietyService;
		}

		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Index()  // add using System.Threading.Tasks;
		{
			var list = await _beerService.GetAllAsync();
			List<BeerVM> listVM = _mapper.Map<List<BeerVM>>(list);
			return View(listVM);
		}
		// Asynchronous code does introduce a small amount of overhead at run time, 
		// but for low traffic situations the performance hit is negligible, while for 
		// high traffic situations, the potential performance improvement is substantial.
		// https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.0

		// GET: Beer/Create
		public async Task<IActionResult> Create()
		{

			var beerCreate = new BeerCreateVM()
			{
				Breweries = new SelectList(
					await _breweryService.GetAllAsync(),
					nameof(Brewery.Brouwernr), nameof(Brewery.Naam)),
				Varieties = new SelectList(
					await _varietyService.GetAllAsync(),
					nameof(Variety.Soortnr), nameof(Variety.Soortnaam))
			};

			return View(beerCreate);
		}


		//  POST: Beer/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BeerCreateVM entityVM)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var beer = _mapper.Map<Beer>(entityVM);
					await _beerService.AddAsync(beer);
					return RedirectToAction("Index");
				}
			}
			catch (DataException)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
			}

			catch (Exception)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "call system administrator.");
			}

			entityVM.Breweries = new SelectList(
				await _breweryService.GetAllAsync(),
				nameof(Brewery.Brouwernr), nameof(Brewery.Naam),
				entityVM.Brouwernr
			);

			entityVM.Varieties = new SelectList(
				await _varietyService.GetAllAsync(),
				nameof(Variety.Soortnr), nameof(Variety.Soortnaam),
				entityVM.Soortnr
			);

			return View(entityVM);
		}

		[NonAction]
		private async Task<SelectList> BreweriesAsSelectList(object? selectedItem = null)
		{
			return new SelectList(
				await _breweryService.GetAllAsync(),
				nameof(Brewery.Brouwernr), nameof(Brewery.Naam),
				selectedItem
			);
		}

		//  GET: Beer/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Beer? beer = await _beerService.FindByIdAsync(Convert.ToInt32(id));
			if (beer == null)
			{
				return NotFound();
			}

			var beerEdit = _mapper.Map<BeerEditVM>(beer);
			beerEdit.Breweries = new
				SelectList(await _breweryService.GetAllAsync(), nameof(Beer.Brouwernr), nameof(Beer.Naam), beer.Brouwernr);
			beerEdit.Varieties = new
				SelectList(await _varietyService.GetAllAsync(), "Soortnr", "Soortnaam", beer.Soortnr);

			return View(beerEdit);

		}

		// POST: Beer/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(BeerEditVM entityVM)
		{
			// voorbeeld hoe je aan de ModelState foutboodschappen kunt toevoegen in code
			//ModelState.AddModelError("", "Algemene opmerking");
			//ModelState.AddModelError("Naam", "gelieve naam op te geven");
			if (ModelState.IsValid)
			{
				try
				{
					var beer = _mapper.Map<Beer>(entityVM);
					await _beerService.Update(beer);
					return RedirectToAction("Index");
				}
				catch (DataException)
				{
					//Log the error (uncomment dex variable name and add a line here to write a log.
					ModelState.AddModelError("", "Unable to edit data.");
				}

				catch (Exception)
				{
					ModelState.AddModelError("", "Call the administrator");
				}
			}

			entityVM.Breweries = new SelectList(
				await _breweryService.GetAllAsync(),
				nameof(Brewery.Brouwernr), nameof(Brewery.Naam), entityVM.Brouwernr
			);
			entityVM.Varieties = new SelectList(
				await _varietyService.GetAllAsync(),
				nameof(Variety.Soortnr), nameof(Variety.Soortnaam),
				entityVM.Soortnr
			);

			return View(entityVM);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int? bierId)
		{
			if (bierId == null)
			{
				return NotFound();
			}

			try
			{
				var beer = await _beerService.FindByIdAsync(bierId.Value);
				if (beer == null)
				{
					return NotFound();
				}

				var beerVM = _mapper.Map<BeerVM>(beer);
				return View(beerVM);
			}
			catch (Exception)
			{
				return View();
			}
		}

		// POST: Beer/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? bierId)
		{
			if (bierId == null)
			{
				return NotFound();
			}

			try
			{
				Beer? beer = await _beerService.FindByIdAsync(bierId.Value);
				if (beer == null)
				{
					return NotFound();
				}

				await _beerService.DeleteAsync(beer);
				return RedirectToAction("Index");
			}
			catch (DataException)
			{
				// Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to delete data.");

			}
			catch (Exception)
			{
				ModelState.AddModelError("", "Call the administrator");
			}

			return View();
		}

		public async Task<IActionResult> Select(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var beer = await _beerService.FindByIdAsync(id.Value);
			if (beer != null)
			{
				var cartVM = new CartVM
				{
					Biernr = beer.Biernr,
					Aantal = 1,
					Prijs = 15,
					DateCreated = DateTime.Now,
					Naam = beer.Naam,
				};

				var shopping = HttpContext.Session.GetObject<ShoppingCartVM>("shoppingCart") ?? new ShoppingCartVM
				{
					Cart = new List<CartVM>(),
				};

				shopping!.Cart!.Add(cartVM);
				HttpContext.Session.SetObject("shoppingCart", shopping);
			}

			return RedirectToAction("Index", "ShoppingCart");
		}
	}
}
