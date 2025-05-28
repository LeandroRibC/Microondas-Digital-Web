using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MicroondasDigital.Models;

namespace MicroondasDigital.Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    private readonly string _jsonFilePath = "programas_customizados.json";

    public IActionResult Index() {
        List<ProgramaAquecimento> programas = new List<ProgramaAquecimento>()
        {
            new ProgramaAquecimento
            {
                Nome = "Pipoca",
                Alimento = "Pipoca (de micro-ondas)",
                TempoSegundos = 180,
                Potencia = 7,
                StringAquecimento = "~~~",
                Instrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."
            },
            new ProgramaAquecimento
            {
                Nome = "Leite",
                Alimento = "Leite",
                TempoSegundos = 300,
                Potencia = 5,
                StringAquecimento = "###",
                Instrucoes = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."
            },
            new ProgramaAquecimento
            {
                Nome = "Carnes de boi",
                Alimento = "Carne em pedaço ou fatias",
                TempoSegundos = 840,
                Potencia = 4,
                StringAquecimento = "@@@",
                Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
            },
            new ProgramaAquecimento
            {
                Nome = "Frango",
                Alimento = "Frango (qualquer corte)",
                TempoSegundos = 480,
                Potencia = 7,
                StringAquecimento = "+++",
                Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
            },
            new ProgramaAquecimento
            {
                Nome = "Feijão",
                Alimento = "Feijão congelado",
                TempoSegundos = 480,
                Potencia = 9,
                StringAquecimento = "&&&",
                Instrucoes = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."
            }
        };
        TempData["Programas"] = Newtonsoft.Json.JsonConvert.SerializeObject(programas);

        return View();
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
