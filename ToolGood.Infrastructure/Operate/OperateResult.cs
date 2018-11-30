using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure
{
    [Serializable]
    public partial class OperateResult
    {
        private bool _isSuccess { get; set; }
        /// <summary>
        /// 附加说明
        /// </summary>
        public string Message { get; set; }

        protected OperateResult()
        {
            _isSuccess = true;
        }
        protected OperateResult(bool isSuccess,string msg)
        {
            _isSuccess = isSuccess;
            Message = msg;
        }

        protected OperateResult(Exception ex) : this()
        {
            bool flag = ex is ArgumentException;
            this._isSuccess = false;
            if (flag) {
                this.Message = ex.Message;
            } else {
                this.Message = "系统错误,请联系管理员";
            }
        }



        /// <summary>
        /// 判断操作是否成功
        /// </summary>
        /// <returns>true表示成功</returns>
        public bool IsSuccess {
            get {
                return this._isSuccess;
            }
        }
        public bool IsFailed {
            get {
                return this._isSuccess == false;
            }
        }
    }

    [Serializable]
    public class OperateResult<T> : OperateResult
    {
        internal OperateResult()
        {
        }
        internal OperateResult(bool isSuccess, string msg):base(isSuccess,msg)
        {
        }

        internal OperateResult(Exception ex) : base(ex)
        {
        }
        public T Value { get; set; }

        /// <summary>
        /// 判断操作是否成功并且值不为空
        /// </summary>
        /// <returns>true表示成功</returns>
        public bool SuccessAndValueNotNull {
            get {
                return base.IsSuccess && this.Value != null;
            }
        }
    }

}
