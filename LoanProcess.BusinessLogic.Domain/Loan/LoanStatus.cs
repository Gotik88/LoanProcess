
namespace LoanProcess.BusinessLogic.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum LoanStatus
    {
        Requested,
        InProgress,
        Approved,
        Funding,
        Borrowed
    }
}
