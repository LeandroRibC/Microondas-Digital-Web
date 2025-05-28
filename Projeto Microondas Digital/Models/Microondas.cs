namespace MicroondasDigital.Models {
    public class Microondas {
        public int Tempo { get; set; } 
        public int Potencia { get; set; } = 10; 

        public Microondas(int tempo, int potencia = 10) {
            if (tempo < 1 || tempo > 120)
                throw new ArgumentException("O tempo deve estar entre 1 segundo e 2 minutos.");
            if (potencia < 1 || potencia > 10)
                throw new ArgumentException("A potência deve ser entre 1 e 10.");

            Tempo = tempo;
            Potencia = potencia;
        }

        public string IniciarAquecimento() {
            string resultado = "Aquecendo: ";
            int tempoRestante = Tempo;
            int quantidadePontos = 0;

            // A quantidade de pontos varia com a potência
            for (int i = 0; i < Tempo; i++) {
                quantidadePontos += Potencia;
                resultado += new string('.', Potencia);
            }

            return resultado + " Aquecimento concluído.";
        }

        public string FormatarTempo() {
            int minutos = Tempo / 60;
            int segundos = Tempo % 60;
            return $"{minutos}:{segundos:D2}";
        }
    }
}
