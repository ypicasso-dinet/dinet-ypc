using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Querys.GetAll
{
    public class ProductoGetAllQueryValidator : AppBaseValidator<ProductoGetAllQuery>
    {
        public ProductoGetAllQueryValidator()
        {
            // Validations if is needed
        }
    }
}
