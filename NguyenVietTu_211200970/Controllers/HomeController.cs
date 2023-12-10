using NguyenVietTu_211200970.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using System.Security.Cryptography;

namespace NguyenVietTu_211200970.Controllers


{
	public class HomeController : Controller
	{

        private QLHangHoaContext db;
        private static int limit = 2;
        public HomeController(QLHangHoaContext context)
        {
            db = context;
        }

		public IActionResult Index(int? lh)
		{
			IEnumerable<HangHoa> list_hh;

			if (lh.HasValue && lh > 0)
			{
				list_hh = db.HangHoas.Where(hh => hh.Gia > 100 && hh.MaLoai.Equals(lh.Value)).ToList();
			}
			else
			{
				list_hh = db.HangHoas.Where(hh => hh.Gia > 100).ToList();
			}

			int pageNumber = (int)Math.Ceiling((double)list_hh.Count() / limit);
			ViewBag.pageNum = pageNumber;
			return View("NguyenVietTu_mainContent", list_hh.Take(limit));
		}

		public IActionResult ShowProductsFilter(int? lh, string? keyword, int page = 1)
		{
			IEnumerable<HangHoa> list_hh = (IQueryable<HangHoa>)db.HangHoas.Where(hh => hh.Gia > 100);
			
			if(lh > 0)
            {
				list_hh = list_hh.Where(hh => hh.MaLoai.Equals(lh));
				ViewBag.lh = lh;
			}

			if(keyword != null)
			{
				list_hh = list_hh.Where(x => x.TenHang.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
			}

			int pageNum = (int)Math.Ceiling(list_hh.Count() / (float)limit);
			ViewBag.pageNum = pageNum;

			var result = list_hh.Skip(limit * (page - 1)).Take(limit);

			return PartialView("ListProduct", result); // html
		}


		public IActionResult ShowProducts(int lh) 
        {
			IEnumerable<HangHoa> list_hh = db.HangHoas.Where(hh => hh.Gia > 100).ToList();

            if(lh > 0)
            {
                list_hh = list_hh.Where(hh => hh.MaLoai.Equals(lh)); 
            }

			return PartialView("ListProduct" , list_hh); // html
        }





        public IActionResult AddToCart()
        {
       

            ViewBag.CategoryId = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai"); // // select MaLoai, TenLoai from LoaiHang
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(HangHoa hh)
        {
           if(ModelState.IsValid)
            {
                db.HangHoas.Add(hh);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
			ViewBag.CategoryId = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
			return View("AddToCart");

        }


    }
}