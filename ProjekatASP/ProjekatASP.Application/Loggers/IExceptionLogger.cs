﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.Loggers
{
    public interface IExceptionLogger
    {
        public void Log(Exception ex);
    }
}
