﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Watchers
{
    public interface IWather
    {
        void Start();
        void Stop();
    }
}