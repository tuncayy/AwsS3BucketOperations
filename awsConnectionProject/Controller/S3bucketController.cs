using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using awsConnectionProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace awsConnectionProject.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class S3bucketController : ControllerBase
    {
        private readonly IS3Service _service;

        public S3bucketController(IS3Service service)
        {
            _service = service;
        }

        [HttpPost("{BucketName}")]
        public async Task<IActionResult> createBucket([FromRoute] string bucketName)
        {
            var response = await _service.CreateBucketAsync(bucketName);
            return Ok(response);
        }

        [HttpDelete("{BucketName}")]
        public async Task<IActionResult> deleteBucket([FromRoute] string bucketName)
        {
            var response = await _service.DeleteBucketAsync(bucketName);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddFile/{BucketName}")]
        public async Task<IActionResult> addFile([FromRoute] string bucketName, string fileNameInBucket, string path)
        {
            var response = await _service.AddFile(bucketName, fileNameInBucket, path);

            return Ok(response);
        }
    }
}