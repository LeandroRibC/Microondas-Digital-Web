using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize] // exige autenticação Bearer
public class MicroondasController : ControllerBase {
    private readonly MicroondasService _service;

    public MicroondasController(MicroondasService service) {
        _service = service;
    }

    [HttpPost("definir")]
    public IActionResult DefinirTempoEPotencia([FromBody] DefinirTempoDto dto) {
        _service.DefinirTempoEPotencia(dto.Tempo, dto.Potencia);
        return Ok(_service.ObterEstado());
    }

    [HttpPost("iniciar")]
    public IActionResult Iniciar() {
        _service.Iniciar();
        return Ok(_service.ObterEstado());
    }

    [HttpPost("acrescentar")]
    public IActionResult AcrescentarTempo() {
        _service.AcrescentarTempo();
        return Ok(_service.ObterEstado());
    }

    [HttpPost("pausar-cancelar")]
    public IActionResult PausarOuCancelar() {
        _service.PausarOuCancelar();
        return Ok(_service.ObterEstado());
    }

    [HttpGet("estado")]
    public IActionResult Estado() {
        return Ok(_service.ObterEstado());
    }
}
