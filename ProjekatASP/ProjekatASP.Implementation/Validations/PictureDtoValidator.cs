using FluentValidation;
using ProjekatASP.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.Validations
{
    public class PictureDtoValidator : AbstractValidator<PictureDto>
    {
        public PictureDtoValidator()
        {
            RuleFor(x => x.Src).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Src is required");
            RuleFor(x => x.Alt).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Alt is required.");
        }
    }
}
