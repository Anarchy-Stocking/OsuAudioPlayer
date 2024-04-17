//using OsuData.FileExplainer;

//var manager = new ClassifiedSongsEditorAndExplainerManager(@"C:\Users\Alyce\Desktop\test");
//manager.AddLovedSong("test");

using System;
using System.Threading;
using System.Threading.Tasks;


Console.WriteLine("Application thread ID: {0}",
                    Thread.CurrentThread.ManagedThreadId);
var t = Task.Run(() => 
{
    Console.WriteLine("Task thread ID: {0}",Thread.CurrentThread.ManagedThreadId);
});
t.Wait();

// The example displays the following output:
//       Application thread ID: 1
//       Task thread ID: 3




