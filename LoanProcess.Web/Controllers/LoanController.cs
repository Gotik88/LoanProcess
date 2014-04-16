// ============================================================================
// <copyright file="LoanListViewModel.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace MvcAuthentication.Controllers
{
    using System.Web.Mvc;
    using LoanProcess.Infrastructure.Caching;
    using LoanProcess.Infrastructure.Caching.Expiration;
    using LoanProcess.ServiceLayer;
    using LoanProcess.ServiceLayer.Messages.Loan;

    public class LoanController : Controller
    {
        private readonly IRequestResponseFactory _serviceFactory;
        private readonly CacheManagerBase _cacheManager;

        public LoanController(CacheManagerBase cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ActionResult List()
        {
            var loanModel = _cacheManager.Get(CacheKeyBuilder.GetKey(), () =>
            {
                var response = _serviceFactory.ProcessRequest<GetLoansRequest, GetLoansResponse>(new GetLoansRequest());


                return response.Loans;
            });


            return View("List", loanModel);
        }
    }
}