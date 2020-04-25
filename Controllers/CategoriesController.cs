using GuideMe.Models.Identity_models;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace GuideMe.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        //public ActionResult Index()
        //{
        //    return View();
        //}
         
        ApplicationDbContext Context=new ApplicationDbContext();
       
        public ActionResult Getcategory(int id , int? page )

        {
            
            var  wanted_cat = Context.Categories.Find(id);//id
            if (wanted_cat != null)
            {

                var allPlaces = from place in Context.Places
                                      where place.CategoryID == id
                                      select place;

                //return all placec have th same category id
                return View(allPlaces.ToList().ToPagedList(page ?? 1, 6));
            }

            return RedirectToAction("index");
        }

    }
}