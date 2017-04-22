using System;

namespace PromiseSharp
{
    public interface IPromisable
    {
        void Reject();
        void Reject(Exception e);
        void Resolve();
    }
}