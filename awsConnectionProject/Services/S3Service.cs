using Amazon.S3;
using Amazon.S3.Util;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using awsConnectionProject.Models;
using Amazon.S3.Transfer;

namespace awsConnectionProject.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                if (await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName) == false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true,
                    }; 
                    
                    var response = await _client.PutBucketAsync(putBucketRequest);

                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode,
                    };
                }
                else
                {
                    return new S3Response
                    {
                        Message = "bucket name is not unique",
                        
                    };
                }

               

            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Status = e.StatusCode,
                    Message = e.Message
                };
                throw;
            }
        }

        public async Task<S3Response> DeleteBucketAsync(string bucketName)
        {
            try
            {
                
                    var deleteBucketRequest = new DeleteBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true,
                    };

                    
                    var response = await _client.DeleteBucketAsync(deleteBucketRequest);
                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode,
                    };
                



            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Status = e.StatusCode,
                    Message = e.Message
                };
                throw;
            }
        }

        private const string FilePath = "";
        public async Task<S3Response> AddFile(string bucketName, string fileNameInBucket, string filePath)
        {
            TransferUtility transferUtility = new TransferUtility();
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                request.BucketName = bucketName; //no subdirectory just bucket name
            
            request.Key = fileNameInBucket ; //file name up in S3
            request.FilePath = filePath; //local file name
            transferUtility.Upload(request); //commensing the transfer

            return new S3Response { Message = "true" };
        }
    }
}
