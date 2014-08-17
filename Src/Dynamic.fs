module internal FsPlot.Dynamic

// http://fssnip.net/2U

open System
open System.Runtime.CompilerServices
open Microsoft.CSharp.RuntimeBinder
 
let (?) (inst:obj) name (arg:'T) : 'R =
    let convertSite = 
        CallSite<Func<CallSite, Object, 'R>>.Create
            (Binder.Convert(CSharpBinderFlags.None, typeof<'R>, null))
 
    let callSite = 
        CallSite<Func<CallSite, Object, 'T, Object>>.Create
            (Binder.InvokeMember
            ( CSharpBinderFlags.None, name, null, null, 
                [| CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) |]))
 
    convertSite.Target.Invoke
        (convertSite, callSite.Target.Invoke(callSite, inst, arg))