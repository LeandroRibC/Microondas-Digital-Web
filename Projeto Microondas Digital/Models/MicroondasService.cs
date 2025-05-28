using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MicroondasDigital.Models {
    public class MicroondasService {
        private readonly string _jsonFilePath;

        public MicroondasService(string jsonFilePath = "programas_customizados.json") {
            _jsonFilePath = jsonFilePath;
        }

        public List<ProgramaAquecimento> CarregarProgramasCustomizados() {
            if (File.Exists(_jsonFilePath)) {
                string json = File.ReadAllText(_jsonFilePath);
                return JsonConvert.DeserializeObject<List<ProgramaAquecimento>>(json) ?? new List<ProgramaAquecimento>();
            }
            return new List<ProgramaAquecimento>();
        }

        public void SalvarProgramasCustomizados(List<ProgramaAquecimento> programas) {
            string json = JsonConvert.SerializeObject(programas, Formatting.Indented);
            File.WriteAllText(_jsonFilePath, json);
        }

        public bool StringAquecimentoEmUso(List<ProgramaAquecimento> programas, string stringAquecimento) {
            return programas.Exists(p => p.StringAquecimento == stringAquecimento) || stringAquecimento == ".";
        }
    }
}