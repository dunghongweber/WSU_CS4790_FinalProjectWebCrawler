using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrawlerApp.Client.Models;

namespace CrawlerApp.Client.Controllers
{
    [Route("api/[controller]")]
    public class CrawlerLinkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrawlerLinkController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "All Links";
            

            var model = _context.MyLinks.ToList();
            return View(model);
        }

        [HttpGet("Crawled")]
        public IActionResult GetCrawledLinks()
        {
            ViewData["Title"] = "Crawled Links";

            var model = _context.MyLinks.Where(e => e.Crawled == 1).ToList();
            return View(model);
        }

        [HttpGet("UnCrawled")]
        public IActionResult GetUnCrawledLinks()
        {
            ViewData["Title"] = "UnCrawled Links";

            var model = _context.MyLinks.Where(e => e.Crawled == 0).ToList();
            return View(model);
        }


    }
}
