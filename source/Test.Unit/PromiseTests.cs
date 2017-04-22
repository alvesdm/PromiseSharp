using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromiseSharp;
using System.Threading.Tasks;

namespace Test.Unit
{
    [TestClass]
    public class PromiseTests
    {
        [TestMethod]
        public async Task Fire_FireOnce_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                promise.Resolve();
            })
            .Then(() => { i += 2; }) // HIT
            .Fail((e) => { i += 3; }) 
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(7));
        }

        [TestMethod]
        public async Task Fire_FireMoreThanOnceRunOnce_Succeed()
        {
            var i = 0;
            var x = await new Promise((promise) =>
            {
                i++;
                promise.Resolve();
            })
            .Then(() => { i += 2; }) // HIT
            .Fail((e) => { i += 3; }) 
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(7));
            await Assert.ThrowsExceptionAsync<FiredException>(async ()=> { await x.Fire(); });
        }

        [TestMethod]
        public async Task Then_AddMoreThanOne_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                promise.Resolve();
            })
            .Then(() => { i += 2; }) // HIT
            .Then(() => { i += 3; }) // HIT
            .Fail((e) => { i += 4; }) 
            .Always(() => { i += 5; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(11));
        }

        [TestMethod]
        public async Task Fire_Reject_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                promise.Reject();
            })
            .Then(() => { i += 2; }) 
            .Fail((e) => { i += 3; }) // HIT
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(8));
        }

        [TestMethod]
        public async Task Fire_RejectBeforeResolveShallReject_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                promise.Reject();
                promise.Resolve();
            })
            .Then(() => { i += 2; }) 
            .Fail((e) => { i += 3; }) // HIT
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(8));
        }

        [TestMethod]
        public async Task Fire_ResolveBeforeRejectShallResolve_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                promise.Resolve();
                promise.Reject();
            })
            .Then(() => { i += 2; }) // HIT
            .Fail((e) => { i += 3; }) 
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(7));
        }

        [TestMethod]
        public async Task Fire_ExceptionBeforeResolveShallExecuteFailAndAlways_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                throw new Exception("Bangg!!!");
                promise.Resolve();
            })
            .Then(() => { i += 2; }) 
            .Fail((e) => { i += 3; }) // HIT
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(8));
        }

        [TestMethod]
        public async Task Fire_ExceptionBeforeRejectShallExecuteAlwaysOnly_Succeed()
        {
            var i = 0;
            await new Promise((promise) =>
            {
                i++;
                throw new Exception("Bangg!!!");
                promise.Reject();
            })
            .Then(() => { i += 2; }) 
            .Fail((e) => { i += 3; }) // HIT
            .Always(() => { i += 4; }) // HIT
            .Fire();

            Assert.IsTrue(i.Equals(8));
        }
    }
}
