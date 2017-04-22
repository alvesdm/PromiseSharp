AppVeyor: [![Build status](https://ci.appveyor.com/api/projects/status/6ci067uqifvds6ho?svg=true)](https://ci.appveyor.com/project/alvesdm/promisesharp)

# PromiseSharp
A C# Promise package for .net core.

How to use it?

```csharp
await new Promise((promise) =>
{
    if (something)
        promise.Resolve();
    else
        promise.Reject(new Exception("Bangg!!!"));
})
.Then(() => { Console.WriteLine("Nice we..."); }) // HIT
.Then(() => { Console.WriteLine("... did it!!!"); }) // HIT
.Fail((e) => { Console.WriteLine($"Well.. nice we did it, otherwhise we'd get this error:{e.Message}"); })
.Always(() => { Console.WriteLine("I don't care whether we did it or not...Move on!"); }) // HIT
.Fire();
```

# License

Code released under the MIT license.
