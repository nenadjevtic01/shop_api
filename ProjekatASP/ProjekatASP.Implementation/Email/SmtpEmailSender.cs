﻿using ProjekatASP.Application.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.Email
{
    public class SmtpEmailSender : IEmail
    {

        private string _fromEmail;
        private string _password;
        private int _port;
        private string _host;

        public SmtpEmailSender(string fromEmail, string password, int port, string host)
        {
            _fromEmail = fromEmail;
            _password = password;
            _port = port;
            _host = host;
        }

        public void Send(MessageDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_fromEmail, _password),
                UseDefaultCredentials = false
            };

            var message = new MailMessage(_fromEmail, dto.To);
            message.Subject = dto.Title;
            message.Body = dto.Body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }

    }
}
