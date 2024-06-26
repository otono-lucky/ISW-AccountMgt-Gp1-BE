﻿using AccountMgt.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Utility.Email
{
    public interface IEmailService
    {
        Task SendEmailRegistration(EmailDto request);
        Task SendLockoutNotificationAsync(EmailDto request);
        Task SendForgotPasswordEmailAsync(EmailDto request);
    }
}
