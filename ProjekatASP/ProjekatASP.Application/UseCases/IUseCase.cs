using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases
{
    public interface IUseCase
    {
        public int UseCaseId { get;}

        public string UseCaseName { get;}
        public string UseCaseDescription { get; }
    }
}
