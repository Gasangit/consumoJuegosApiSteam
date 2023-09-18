namespace consumoJuegosApiSteam.Controllers
{
    public class JuegoGet
    {
        public string requestUrl { set; get; }
        public string requestUri { set; get; }
        public int game { set; get; }

        public JuegoGet(int appid)
        {
            this.requestUrl = "https://api.steampowered.com/";
            this.requestUri = "api/appdetails?appids=";
            this.game = appid;
        }
    }
}
