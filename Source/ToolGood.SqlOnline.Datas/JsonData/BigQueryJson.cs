/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class BigQueryJson
    {
        /// <summary>
        /// Project ID
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// JSON keyfile for service account
        /// </summary>
        public string KeyFile { get; set; }
        /// <summary>
        ///  Dataset to use
        /// </summary>
        public string DatasetName { get; set; }
        /// <summary>
        /// Location for this dataset
        /// </summary>
        public string DatasetLocation { get; set; }

         
    }
}
