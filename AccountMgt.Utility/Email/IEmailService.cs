﻿using AccountMgt.Data.DTO;
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
    }
}
