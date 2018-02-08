using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace XYApp.Models
{
    public class POIValidator: AbstractValidator<POI>
    {
        public POIValidator()
        {
            //definição das validações

            RuleFor(x => x.NomePOI)
                .NotEmpty().WithMessage("Informe o nome do POI");

            RuleFor(x => x.PntX)
                .NotEmpty().WithMessage("Informe a coordenda X do POI");

            RuleFor(x => x.PntY)
               .NotEmpty().WithMessage("Informe a coordenda Y do POI");
        }

    }
}
