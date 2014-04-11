// ============================================================================
// <copyright file="ApplicationSettingsFactory.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Configuration
{
    public static class ApplicationSettingsFactory
    {
        private static IApplicationSettings _applicationSettings;

        public static void InitializeApplicationSettingsFactory(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public static IApplicationSettings GetApplicationSettings()
        {
            return _applicationSettings;
        }
    }
}
