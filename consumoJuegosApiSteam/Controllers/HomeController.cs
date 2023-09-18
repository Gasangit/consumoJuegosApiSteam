using consumoJuegosApiSteam.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace consumoJuegosApiSteam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string presentClass;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private void asignarPresentClass()
        { 
            this.presentClass = this.GetType().Name;
        }

        
        public async Task<IActionResult> Index()
        {
            JuegoGet Request = new JuegoGet(2294472);
            Data data = new Data();
            asignarPresentClass();

            //List<News> noticias = new List<News>();
            appdetails appsResult = new appdetails();
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(Request.requestUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Response = await Client.GetAsync("https://store.steampowered.com/api/appdetails?appids=561560&cc=tw");
                //Request.requestUrl + Request.requestUri + Request.game
                //+ Request.onlyGet
                //Debug.WriteLine(presentClass + Request.requestUrl + Request.requestUri + Request.onlyGet);
                Debug.WriteLine(presentClass + Response.ToString());

                if (Response.IsSuccessStatusCode)
                {

                    var contenidoBruto = await Response.Content.ReadAsStringAsync();
                    Debug.WriteLine(contenidoBruto.ToString());

                    appsResult = JsonConvert.DeserializeObject<appdetails>(contenidoBruto);

                    //Debug.WriteLine(presentClass + "Items en list noticias : " + noticias.appnews.appid);

                    Debug.WriteLine($"{appsResult.idApp.data.name} {appsResult.idApp.data.precio.final}");

                    return View(appsResult.idApp.data);
                }
            }
            return View(data);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}