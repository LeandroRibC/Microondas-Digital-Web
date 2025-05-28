using Xunit;
using MicroondasDigital.Models;
using System.Collections.Generic;
using System.IO;

public class MicroondasServiceTests {
    private const string TestFile = "test_programas.json";

    [Fact]
    public void SalvarECarregarProgramasCustomizados_DevePersistirDados() {
        var service = new MicroondasService(TestFile);
        var programas = new List<ProgramaAquecimento>
        {
            new ProgramaAquecimento { Nome = "Teste", Alimento = "Arroz", TempoSegundos = 100, Potencia = 5, StringAquecimento = "***", Instrucoes = "Teste" }
        };

        service.SalvarProgramasCustomizados(programas);
        var programasLidos = service.CarregarProgramasCustomizados();

        Assert.NotNull(programasLidos);
        Assert.Single(programasLidos);
        Assert.Equal("Teste", programasLidos[0].Nome);

        File.Delete(TestFile);
    }

    [Fact]
    public void StringAquecimentoEmUso_DeveDetectarCaractereDuplicado() {
        var service = new MicroondasService();
        var programas = new List<ProgramaAquecimento>
        {
            new ProgramaAquecimento { StringAquecimento = "###" }
        };

        var resultado = service.StringAquecimentoEmUso(programas, "###");
        Assert.True(resultado);
    }
}