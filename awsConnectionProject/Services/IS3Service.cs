using awsConnectionProject.Models;
using System.Threading.Tasks;

namespace awsConnectionProject.Services
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);
        Task<S3Response> DeleteBucketAsync(string bucketName);

        Task<S3Response> AddFile(string bucketName, string fileNameInBucket, string path);
    }
}
