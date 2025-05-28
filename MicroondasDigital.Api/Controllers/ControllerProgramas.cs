using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroondasDigital.Negocio.servicos;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgramasController : ControllerBase {
    private readonly ProgramaService _service;

    public ProgramasController(ProgramaService service) {
        _service = service;
    }

    [HttpGet]
    public IActionResult Listar() {
        return Ok(_service.ListarTodos());
    }

    [HttpPost]
    public IActionResult Criar([FromBody] ProgramaAquecimento programa) {
        _service.AdicionarCustomizado(programa);
        return Ok();
    }

    [HttpGet("{nome}")]
    public IActionResult Obter(string nome) {
        var programa = _service.ObterPorNome(nome);
        if (programa == null)
            return NotFound();
        return Ok(programa);
    }
}
