using System;

namespace OpenNI2
{

    public abstract class DisposableBase : IDisposable
    {
        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableBase()
        {
            Dispose(false);
        }
    }
}
