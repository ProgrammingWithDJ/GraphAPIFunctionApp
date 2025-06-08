using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GraphAPIFunctionApp;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
    {
        // Check if the incoming HTTP request contains form data
        if (req.HasFormContentType)
        {
            // Read the form data from the request and store it in the 'form' variable
            var form = await req.ReadFormAsync();

            // Extract the 'subject' field from the form data
            string subject = form["subject"];

            // Extract the 'content' field from the form data
            string content = form["content"];

            // Send an email using the extracted subject and content
            await new ServiceClass().SendMailAsync(subject, content);

            // Return a successful HTTP response with a message
            return new OkObjectResult("E-mail sent successfully");
        }

        // If the request does not contain form data, return a bad request HTTP response
        return new BadRequestObjectResult(null);
    }
}
