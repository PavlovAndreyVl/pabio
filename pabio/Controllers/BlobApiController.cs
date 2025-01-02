using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using pabio.Services;

namespace pabio.Controllers
{
    [ApiController]
    [Route("api/getblob")]
    public class BlobController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EventApiController> _logger;
        public BlobController(IConfiguration config,
            ILogger<EventApiController> logger)
        {
            _config = config;
            _logger = logger;
        }

        private readonly string _containerName = "sertificates";

        [HttpGet]
        public async Task<IActionResult> DownloadBlob()
        {
            try
            {
                string connectionString = _config["pabio-sa-connection"]!;
                string blobName = "Visionest Institite Andrii Pavlov.pdf";
                // Create a BlobServiceClient
                var blobServiceClient = new BlobServiceClient(connectionString);

                // Get a reference to the container
                var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

                // Get a reference to the blob
                var blobClient = containerClient.GetBlobClient(blobName);

                // Check if the blob exists
                if (!await blobClient.ExistsAsync())
                {
                    return NotFound($"Blob '{blobName}' not found.");
                }

                // Download the blob to a memory stream
                var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);

                // Reset the memory stream's position to the beginning
                memoryStream.Position = 0;

                // Return the blob as a file
                return File(memoryStream, "application/octet-stream", blobName);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
