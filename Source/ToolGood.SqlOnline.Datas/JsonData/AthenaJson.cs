/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class AthenaJson
    {
        /// <summary>
        /// AWS Region
        /// </summary>
        public string AwsRegion { get; set; }
        /// <summary>
        /// AWS Access Key ID
        /// </summary>
        public string AwsAccessKeyId { get; set; }
        /// <summary>
        /// AWS Secret Access Key
        /// </summary>
        public string AwsSecretAccessKey { get; set; }
        /// <summary>
        /// Athena staging directory in S3
        /// </summary>
        public string AthenaStagingDirectory { get; set; }
        /// <summary>
        /// Athena workgroup(`primary' by default)
        /// </summary>
        public string AthenaWorkgroup { get; set; }

         
    }

}
