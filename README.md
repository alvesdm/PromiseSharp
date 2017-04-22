# PromiseSharp
A C# Promise package for .net core.

How to use it?

await new Promise((promise) =>
{
    if (something)
        promise.Resolve();
    else
        promise.Reject();
})
.Then(() => { Console.WriteLine("Nice we..."); }) // HIT
.Then(() => { Console.WriteLine("... did it!!!"); }) // HIT
.Fail((e) => { Console.WriteLine($"Well.. nice we did it, otherwhise we'd get this error:{e.Message}"); })
.Always(() => { Console.WriteLine("I don't care whether we did it or not...Move on!"); }) // HIT
.Fire();
