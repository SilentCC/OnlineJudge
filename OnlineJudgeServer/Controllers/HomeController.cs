using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

        [HttpGet("/books/search")]
        public IActionResult SearchBooks(string keyword ,int start)
        {
            List<Books> result = new List<Books>();
            if (start == 0)
            {
                result = _libraryContext.Books.Where(item => item.Title.Contains(keyword)).ToList();
                
                
            }

            var res = new List<SampleBook>();

            foreach (var x in result)
            {
                res.Add(new SampleBook
                {
                    author = Convert(x.Author),
                    available_num = new Random().Next(60,80),
                    binding = x.Binding,
                    call_number = x.CallNumber,
                    class_num = x.ClassNum,
                    id =x.Id,
                    imgs = new Image
                    {
                        large = "@string",
                        medium = "@string",
                        small = "https://www.library-online.cn/imgs/picture-ISBN-978-7-5004-9379-2.jpg"
                    },
                    isbn = long.Parse(x.Isbn),
                    pages = x.Page,
                    price = x.Price,
                    pubdate = x.Pubdate.ToString(),
                    publisher = x.Publisher,
                    review_num = new Random().Next(80,100),
                    title = x.Title,
                    total_num = new Random().Next(80,100),
                    total_score = new Random().NextDouble()*10,
                    translator = new List<string>(),
                    words = x.Word

                });
            }
            return Ok(res);
        }
        
        private List<string> Convert(string x)
        {
            var ans = new List<string>();
            string s = "";
            for (int i = 0; i < x.Length; i++)
            {
                
                if (x[i] != ',' && x[i] != '"' && x[i] != '[' && x[i] != ']')
                {
                    s += x[i];
                }
                else
                {
                    if (s != "")
                    {
                        ans.Add(s);
                        s = "";
                    }
                }
            }

            return ans;
        }
        
    }

    public class SampleBook
    {
        public List<string> author;
        public int available_num;
        public string binding;
        public string call_number;
        public string class_num;
        public int id;
        public Image imgs;

        public long isbn;
        public int pages;
        public double price;
        public string pubdate;
        public string publisher;
        public int review_num;

        public string title;
        public int total_num;
        public double total_score;

        public List<string> translator;

        public int words;
    }

    public class Image
    {
        public string small;
        public string medium;
        public string large;
    }

    public class SearchInfo
    {
        public string KeyWord;
        public int Start;
    }
}
