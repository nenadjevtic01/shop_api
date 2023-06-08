using ProjekatASP.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.Commands
{
    public interface ICreateCartItemCommand : ICommand<CreateCartItemDto>
    {
    }
}
