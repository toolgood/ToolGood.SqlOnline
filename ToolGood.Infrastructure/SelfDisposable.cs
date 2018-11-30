using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure
{
    public class SelfDisposable : IDisposable
    {
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeCode()
        {
        }

        private void Dispose(bool disposing)
        {
            bool flag = !this._isDisposed && disposing;
            if (flag) {
                this.DisposeCode();
            }
            this._isDisposed = true;
        }

        ~SelfDisposable()
        {
            this.Dispose(false);
        }

        private bool _isDisposed;
    }
}
