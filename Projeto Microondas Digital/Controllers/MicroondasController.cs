using Microsoft.AspNetCore.Mvc;
using MicroondasDigital.Models;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

// utilizando o TempData pois o viewbag com o list estava dando conflitos //

namespace MicroondasDigital.Controllers {
    public class MicroondasController : Controller {
        private readonly string _jsonFilePath = "programas_customizados.json";

        public IActionResult Index() {
            var programasPreDefinidos = new List<ProgramaAquecimento>
            {
                new ProgramaAquecimento { Nome = "Pipoca", Alimento = "Pipoca (de micro-ondas)", TempoSegundos = 180, Potencia = 7, StringAquecimento = "~~~", Instrucoes = "Observar o barulho de estouros do milho." },
                new ProgramaAquecimento { Nome = "Leite", Alimento = "Leite", TempoSegundos = 300, Potencia = 5, StringAquecimento = "###", Instrucoes = "Cuidado com aquecimento de líquidos." },
                new ProgramaAquecimento { Nome = "Carnes de boi", Alimento = "Carne em pedaço ou fatias", TempoSegundos = 840, Potencia = 4, StringAquecimento = "@@@", Instrucoes = "Vire o conteúdo na metade do processo." },
                new ProgramaAquecimento { Nome = "Frango", Alimento = "Frango (qualquer corte)", TempoSegundos = 480, Potencia = 7, StringAquecimento = "+++", Instrucoes = "Vire o conteúdo na metade do processo." },
                new ProgramaAquecimento { Nome = "Feijão", Alimento = "Feijão congelado", TempoSegundos = 480, Potencia = 9, StringAquecimento = "&&&", Instrucoes = "Deixe o recipiente destampado." }
            };

            var programasCustomizados = CarregarProgramasCustomizados();
            var todosProgramas = programasPreDefinidos.Concat(programasCustomizados).ToList();

            TempData["Programas"] = JsonConvert.SerializeObject(todosProgramas);
            TempData.Keep("Programas"); 

            return View("/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Aquecer(int tempo, int potencia) {
            ViewBag.TempoRestante = $"{tempo} segundos restantes";
            ViewBag.Resultado = "Aquecimento iniciado.";
            return View("/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public IActionResult PausarOuCancelar() {
            ViewBag.TempoRestante = "Nenhum aquecimento em andamento";
            ViewBag.Resultado = "Aquecimento cancelado.";
            return View("/Views/Home/Index.cshtml");
        }

        public IActionResult InicioRapido() {
            ViewBag.TempoRestante = "30 segundos restantes";
            ViewBag.Resultado = "Aquecimento iniciado com Início Rápido.";
            return View("/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public IActionResult SalvarPrograma(ProgramaAquecimento novoPrograma) {
            var programasCustomizados = CarregarProgramasCustomizados();

            var programasPreDefinidos = new List<ProgramaAquecimento>
            {
        new ProgramaAquecimento { Nome = "Pipoca", Alimento = "Pipoca (de micro-ondas)", TempoSegundos = 180, Potencia = 7, StringAquecimento = "~~~", Instrucoes = "Observar o barulho de estouros do milho." },
        new ProgramaAquecimento { Nome = "Leite", Alimento = "Leite", TempoSegundos = 300, Potencia = 5, StringAquecimento = "###", Instrucoes = "Cuidado com aquecimento de líquidos." },
        new ProgramaAquecimento { Nome = "Carnes de boi", Alimento = "Carne em pedaço ou fatias", TempoSegundos = 840, Potencia = 4, StringAquecimento = "@@@", Instrucoes = "Vire o conteúdo na metade do processo." },
        new ProgramaAquecimento { Nome = "Frango", Alimento = "Frango (qualquer corte)", TempoSegundos = 480, Potencia = 7, StringAquecimento = "+++", Instrucoes = "Vire o conteúdo na metade do processo." },
        new ProgramaAquecimento { Nome = "Feijão", Alimento = "Feijão congelado", TempoSegundos = 480, Potencia = 9, StringAquecimento = "&&&", Instrucoes = "Deixe o recipiente destampado." }
    };

            if (programasCustomizados.Any(p => p.StringAquecimento == novoPrograma.StringAquecimento) || novoPrograma.StringAquecimento == ".") {
                ModelState.AddModelError("StringAquecimento", "Este caractere de aquecimento já está em uso.");

                var todosProgramas = programasPreDefinidos.Concat(programasCustomizados).ToList();
                TempData["Programas"] = JsonConvert.SerializeObject(todosProgramas);
                TempData.Keep("Programas");
                return View("/Views/Home/Index.cshtml", novoPrograma);
            }

            novoPrograma.Nome = "Custom " + novoPrograma.Nome;
            programasCustomizados.Add(novoPrograma);
            SalvarProgramasCustomizados(programasCustomizados);

            var todosProgramas2 = programasPreDefinidos.Concat(programasCustomizados).ToList();
            TempData["Programas"] = JsonConvert.SerializeObject(todosProgramas2);

            return RedirectToAction("Index");
        }


        private List<ProgramaAquecimento> CarregarProgramasCustomizados() {
            if (System.IO.File.Exists(_jsonFilePath)) {
                string json = System.IO.File.ReadAllText(_jsonFilePath);
                return JsonConvert.DeserializeObject<List<ProgramaAquecimento>>(json) ?? new List<ProgramaAquecimento>();
            }
            return new List<ProgramaAquecimento>();
        }

        private void SalvarProgramasCustomizados(List<ProgramaAquecimento> programas) {
            string json = JsonConvert.SerializeObject(programas, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonFilePath, json);
        }
    }
}
