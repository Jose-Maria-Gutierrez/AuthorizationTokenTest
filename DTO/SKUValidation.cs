using System.ComponentModel.DataAnnotations;

namespace TestApis.DTO
{
    public class SKUValidation : ValidationAttribute
    {
        public string GetErrorMessage() => "formato SKU incorrecto";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int i;
            char car;
            ProductoDTO producto = (ProductoDTO) validationContext.ObjectInstance;
            string sku = producto.SKU;
            sku = sku.ToUpper();
            
            i = 0;
            while (i<(sku.Length)/2 && sku[i] >= 65 && sku[i] <= 90)
            {
                i++;
            }

            if(i == (sku.Length) / 2)
            {
                while (i<sku.Length && sku[i] >= 48 && sku[i] <= 57)
                {
                    i++;
                }
            }

            if (i<sku.Length)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

    }
}
