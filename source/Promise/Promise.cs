using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromiseSharp
{
    public class Promise : IPromisable
    {
        private Action<IPromisable> _action;
        private Queue<Action> _then = new Queue<Action>();
        private Action<Exception> _fail;
        private Action _always;
        private bool _resolved = false;
        private bool _rejected = false;
        private bool _fired = false;

        public Promise(Action<IPromisable> action)
        {
            this._action = action;
        }

        public Promise Then(Action action)
        {
            if (!_fired)
                this._then.Enqueue(action);
            return this;
        }

        public Promise Fail(Action<Exception> action)
        {
            if (!_fired)
                this._fail = action;
            return this;
        }

        public Promise Always(Action action)
        {
            if (!_fired)
                this._always = action;
            return this;
        }

        public async Task<Promise> Fire()
        {
            if (!_fired)
            {
                _fired = true;
                return await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _action(this);
                    }
                    catch (Exception ex)
                    {
                        DoFail(ex);
                    }
                    finally
                    {
                        _always();
                    }
                    return this;
                });
            }
            else
                throw new FiredException();
        }

        private void DoThen()
        {
            if (!_rejected)
                while (_then.Count > 0)
                {
                    try { _then.Dequeue()(); }
                    catch { /*do nothing*/ }
                }
        }

        private void DoFail(Exception e)
        {
            if (!_resolved && !_rejected)
                _fail(e);
        }

        public void Reject()
        {
            Reject(null);
        }

        public void Reject(Exception e)
        {
            DoFail(e);
            _rejected = true;
        }

        public void Resolve()
        {
            _resolved = true;
            DoThen();
        }
    }
}