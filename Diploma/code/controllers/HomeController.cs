namespace SimplePages.Controllers
{
    public class HomeController : Controller
    {
        // Метод для получения домашней страницы.
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
