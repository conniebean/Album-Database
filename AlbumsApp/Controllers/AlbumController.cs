using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using AlbumsApp.Models;
using AlbumsApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AlbumsApp.Controllers
{
    public class AlbumController : Controller
    {
        public AlbumController(AlbumsDbContext albumsDbContext)
        {
            _albumsDbContext = albumsDbContext;
        }

        public IActionResult ViewAlbums(int id)
        {
            var albums = _albumsDbContext.Albums
                .Where(a => a.StudioId == id)
                .ToList();

            ViewBag.StudioName = _albumsDbContext.Studios
                .Where(s => s.StudioId == id)
                .FirstOrDefault().Name;

            return View(albums);
        }
        [Route("/")]
        public IActionResult List()
        {
            var totalViews = Request.Cookies["total-views"];

            int totalViewsNum = 0;
            if (string.IsNullOrWhiteSpace(totalViews))
            {
                Response.Cookies.Append("total-views", totalViewsNum.ToString());
            }
            else
            {
                totalViewsNum = Convert.ToInt32(totalViews);
                totalViewsNum++;
                Response.Cookies.Append("total-views", totalViewsNum.ToString());
            }

            ViewBag.TotalViews = totalViewsNum.ToString();

            int viewsThisSession = HttpContext.Session.GetInt32("session-views") ?? 0;
            viewsThisSession++;
            HttpContext.Session.SetInt32("session-views", viewsThisSession);

            ViewBag.SessionViews = viewsThisSession;

            var albums = _albumsDbContext.Albums
                .Include(a => a.Studio).OrderByDescending(a => a.YearProduced).ToList();
            return View(albums);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddOrEditAlbumViewModel();
            viewModel.AllStudios = GetAllStudios();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddOrEditAlbumViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _albumsDbContext.Albums.Add(viewModel);
                _albumsDbContext.SaveChanges();

                return RedirectToAction("List", "Album");
            }
            else
            {
                ModelState.AddModelError("", "There were errors in the form - please fix them and try adding again.");
                viewModel.AllStudios = GetAllStudios();
                return View(viewModel);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            AddOrEditAlbumViewModel viewModel = new AddOrEditAlbumViewModel(_albumsDbContext.Albums.Include(a => a.Studio).Where(a => a.AlbumId == id).FirstOrDefault());
            viewModel.AllStudios = GetAllStudios();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddOrEditAlbumViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _albumsDbContext.Albums.Update(viewModel);
                _albumsDbContext.SaveChanges();

                return RedirectToAction("List", "Album");
            }
            else
            {
                ModelState.AddModelError("", "There were errors in the form - please fix them and try updating again.");
                viewModel.AllStudios = GetAllStudios();
                return View(viewModel);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteConfirmation(int id)
        {
            Album album = _albumsDbContext.Albums.Include(a => a.Studio).Where(a => a.AlbumId == id).FirstOrDefault();
            return View(album);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Album album = _albumsDbContext.Albums.Include(a => a.Studio).Where(a => a.AlbumId == id).FirstOrDefault();
            _albumsDbContext.Albums.Remove(album);
            _albumsDbContext.SaveChanges();

            return RedirectToAction("List", "Album");
        }

        [HttpGet("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Studio> GetAllStudios()
        {
            return _albumsDbContext.Studios.OrderBy(s => s.Name).ToList();
        }

        private AlbumsDbContext _albumsDbContext;
    }
}
