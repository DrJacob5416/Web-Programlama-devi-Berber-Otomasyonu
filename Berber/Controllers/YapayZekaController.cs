using Berber.Models.ServiceModels;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Berber.Controllers
{
    [Authorize]
    public class YapayZekaController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        public YapayZekaController(UserManager<ApplicationUser> userManager, IHttpClientFactory httpClientFactory)
        {
            this.userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] Dictionary<int, SelectedPhoto> selectedPhotos)
        {
            string yuztipi = "";
            string sactipi = "";
            string sakal = "";
            foreach (var photo in selectedPhotos)
            {
                if (photo.Key == 1)
                    yuztipi = photo.Value.Value;
                if (photo.Key == 2)
                    sactipi = photo.Value.Value;
                if (photo.Key == 3)
                    sakal = photo.Value.Value;
            }
            var request = $"Yüz tipi {yuztipi}, saç tipi {sactipi} ve {sakal} bir erker için üç saç modeli öner.";
            string apiKey = "";
            string model = "gpt-4o";
            ChatClient client = new ChatClient(model, apiKey);
            ChatCompletion completion = client.CompleteChat(request);
            var response = completion.Content[0].Text;
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var yapayZekaModel = new YapayZekaModel()
            {
                Request = request,
                Response = response,
                ApplicationUserId = user.Id
            };
            var httpClient = _httpClientFactory.CreateClient();
            var createResponse = await httpClient.PostAsJsonAsync("https://localhost:7281/api/BerberApi/CreateYapayZeka", yapayZekaModel);

            if (createResponse.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Answers), "YapayZeka");
            }
            else
            {
                return StatusCode((int)createResponse.StatusCode, "Veri kaydetme sırasında bir hata oluştu.");
            }
        }
        public async Task<IActionResult> Answers()
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            var client = _httpClientFactory.CreateClient();
            var getAnswers = await client.GetAsync($"https://localhost:7281/api/BerberApi/AllAnswers/{user.Id}");
            if (!getAnswers.IsSuccessStatusCode)
            {
                return StatusCode((int)getAnswers.StatusCode, "Veritabanımızda kayıtlı geçmiş sorgunuz yok");
            }

            var answers = await getAnswers.Content.ReadFromJsonAsync<List<Models.Tables.YapayZeka>>();
            if (answers != null && answers.Any())
            {
                foreach (var item in answers)
                {
                    item.Response = Regex.Replace(item.Response, @"\*\*(.*?)\*\*", "<strong>$1</strong>");
                    item.Response = item.Response.Replace("\\n", "<br>");
                    item.Response = item.Response.Replace("\n\n", "</p><p>").Replace("\n", "<br />");
                    item.Response = $"<p>{item.Response}</p>";
                }
            }
            return View(answers.OrderByDescending(m => m.Time).ToList());
        }
    }
}
