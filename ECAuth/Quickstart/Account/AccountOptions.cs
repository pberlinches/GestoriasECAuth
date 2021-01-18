// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace ECAuth.Quickstart.Account
{
    public class AccountOptions
    {
        public static bool ShowLogoutPrompt = false;
        public static bool AutomaticRedirectAfterSignOut = true;
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set LockoutOnFailureAttempt: true
        public static bool LockoutOnFailureAttempt = false;
        public static bool Allow2fa = false;
        public static bool AllowAutoRegistration = false;
        public static IOptions<MyConfig> MyConfig { get; set; }

        //public static int ResetPasswordExpiration
        //{
        //    get
        //    {
        //        string numberString = MyConfig?.Value.ResetPasswordExpiration;

        //        return int.TryParse(numberString, out var number) ? number : 0;
        //    }
        //}

        public static int OldPasswordsToCheck
        {
            get
            {
                string numberString = MyConfig?.Value.OldPassCheck;

                return int.TryParse(numberString, out var number) ? number : 6;
            }
        }
    }
}
