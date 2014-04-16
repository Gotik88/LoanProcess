// ============================================================================
// <copyright file="IWather.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    public interface IWather
    {
        void Start();
        void Stop();
    }
}
