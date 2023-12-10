using NguyenVietTu_211200970.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NguyenVietTu_211200970.Controllers


{
	public class HomeController : Controller
	{

        private QLHangHoaContext db;
        private static int limit = 6;
        public HomeController(QLHangHoaContext context)
        {
            db = context;
        }

        public IActionResult Index()
		{
			IEnumerable<HangHoa> list_hh = (IQueryable<HangHoa>)db.HangHoas.Where(hh => hh.Gia > 100).ToList();
			return View("NguyenVietTu_mainContent", list_hh);
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