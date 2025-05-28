using MicroondasDigital.Models;
using System;
using Xunit;

public class MicroondasTests {
    [Fact]
    public void IniciarAquecimento_DeveRetornarStringCorreta() {
        var microondas = new Microondas(3, 2);
        var resultado = microondas.IniciarAquecimento();
        Assert.Contains("Aquecendo: ...... Aquecimento concluído.", resultado);
    }

    [Fact]
    public void FormatarTempo_DeveFormatarCorretamente() {
        var microondas = new Microondas(75, 5);
        Assert.Equal("1:15", microondas.FormatarTempo());
    }

    [Theory]
    [InlineData(0, 5)]
    [InlineData(121, 5)]
    [InlineData(10, 0)]
    [InlineData(10, 11)]
    public void Construtor_DeveLancarExcecaoParaParametrosInvalidos(int tempo, int potencia) {
        Assert.Throws<ArgumentException>(() => new Microondas(tempo, potencia));
    }
}