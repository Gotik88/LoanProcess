﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Watchers
{
    public interface IListiner
    {
        void StartListen();
        void StopListening();
    }
}