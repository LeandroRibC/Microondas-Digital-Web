namespace MicroondasDigital.Api.DTOs {
    public class LoginDto {
        public string Usuario { get; set; } = string.Empty;      // inicializado
        public string SenhaSha256 { get; set; } = string.Empty;  // inicializado
    }
}
