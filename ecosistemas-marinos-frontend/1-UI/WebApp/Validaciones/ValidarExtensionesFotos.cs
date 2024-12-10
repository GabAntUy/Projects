using System.ComponentModel.DataAnnotations;

namespace WebApp.Validaciones
{
    public class ValidarExtensionesFotos : ValidationAttribute
    {
        private readonly List<string> _extensionesPermitidas;

        public ValidarExtensionesFotos(string extensionesPermitidas)
        {
            _extensionesPermitidas = extensionesPermitidas.Split(',').ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var archivos = value as List<IFormFile>;

            if (archivos != null)
            {
                foreach (var archivo in archivos)
                {
                    var extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();

                    if (!_extensionesPermitidas.Contains(extensionArchivo))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
