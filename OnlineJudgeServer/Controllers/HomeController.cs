using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineJudgeServer.Models;

namespace OnlineJudgeServer.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext _libraryContext;

        public HomeController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public IActionResult Index( )
        {
            return View( );
        }

        public IActionResult Privacy( )
        {
            return View( );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error( )
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("api/book")]
        public IActionResult GetBooks()
        {
            return Ok(_libraryContext.Books.ToList( ));
        }

        [HttpPost("/books/search")]
        public IActionResult SearchBooks([FromBody] SearchInfo searchInfo)
        {
            List<Books> result = new List<Books>();
            if (searchInfo.Start == 0)
            {
                result = _libraryContext.Books.Where(item => item.Title.Contains(searchInfo.KeyWord)).ToList();
            }

            return Ok(result);
        }
        
    }

    public class SearchInfo
    {
        public string KeyWord;
        public int Start;
    }
}
