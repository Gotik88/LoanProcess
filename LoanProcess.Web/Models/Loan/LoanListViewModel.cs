// ============================================================================
// <copyright file="LoanListViewModel.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace MvcAuthentication.Models.Loan
{
    using System.Collections.Generic;

    public partial class LoanListViewModel : BaseViewModel
    {
        public LoanListViewModel()
        {
            Loans = new List<LoanViewModel>();
        }

        public IList<LoanViewModel> Loans { get; set; }
    }
}