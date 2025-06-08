using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GraphAPIFunctionApp
{
    internal class ServiceClass
    {
        #region Constants
        private const string TenantId = ""; // Your tenant id
        private const string ClientId = ""; // Your client id
        private const string ClientSecret = ""; // Your client secret

        private const string ToEmail = ""; // The email to send the message
        private const string FromEmail = ""; // The email from send the message
        #endregion

        public async Task SendMailAsync(string subject, string content)
        {
            // Create a new instance of the ClientSecretCredential class using the tenant ID, client ID, and client secret
            var credentials = new ClientSecretCredential(TenantId, ClientId, ClientSecret);

            // Use the credentials to create a new instance of the GraphServiceClient class
            var graphClient = new GraphServiceClient(credentials);

            // Create a new instance of the Message class to represent the email
            var message = new Message
            {
                // Set the subject of the email
                Subject = subject,

                // Set the body of the email, specifying the content type as HTML
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = content,
                },

                // Set the recipient(s) of the email
                ToRecipients = new List<Recipient>() {
        // Create a new instance of the Recipient class to represent the recipient
        new Recipient{
            // Set the email address of the recipient
            EmailAddress = new EmailAddress{
                Address = ToEmail
            }
        }
    }
            };

            // Create a new instance of the SendMailPostRequestBody class to represent the email request body
            var body = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
            {
                // Set the message to be sent
                Message = message
            };

            // Use the graph client to send the email to the specified user
            await graphClient.Users[FromEmail].SendMail.PostAsync(body);
        }
    }
}