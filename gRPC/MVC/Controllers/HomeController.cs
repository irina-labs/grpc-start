using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Speaker.Service.Protos;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpeakerServiceDefinition.SpeakerServiceDefinitionClient speakerService;

        public HomeController(ILogger<HomeController> logger, SpeakerServiceDefinition.SpeakerServiceDefinitionClient speakerService)
        {
            _logger = logger;
            this.speakerService = speakerService;
        }

        public IActionResult Index()
        {
            var speaker = speakerService.GetById(new SpeakerFilterRequest() { Id = 8 });


            return View();
        }

        [HttpGet("stream")]
        public async IAsyncEnumerable<SpeakerResponse> GetAllStream()
        {
            using var call = speakerService.GetAll(new Empty());
            await foreach (SpeakerResponse response in call.ResponseStream.ReadAllAsync())
            {
                await Task.Delay(1000);
                yield return response;
            }
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