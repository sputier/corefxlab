using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.IO.Pipelines.Samples.Review
{
    class ConsoleSample: ISample
    {
        public async Task Run()
        {
            using (var memoryPool = new MemoryPool())
            {
                var pipe = new Pipe(new PipeOptions(memoryPool));
                await Task.WhenAll(
                        Task.Run(() => ConsoleToPipe(pipe.Writer)), 
                        Task.Run(() => PipeToConsole(pipe)));

            }
        }

        private async Task PipeToConsole(IPipeReader reader)
        {
            var output = Console.OpenStandardOutput();
            while (true)
            {
                var result = await reader.ReadAsync();
                try
                {
                    if (result.IsCompleted)
                    {
                        break;
                    }

                    foreach (var memory in result.Buffer)
                    {
                       await output.WriteAsync(memory);   
                    }
                }
                finally
                {
                    reader.Advance(result.Buffer.End);
                }   
            }
        }

        private async Task ConsoleToPipe(IPipeWriter writer)
        {
            var input = Console.OpenStandardInput();
            while (true)
            {
                var buffer = writer.Alloc(1);
                try
                {
                    var bytesRead = await input.ReadAsync(buffer.Buffer);
                    var exit = buffer.Buffer.Span.Slice(0, bytesRead).IndexOf((byte) 'Q') != -1;
                    buffer.Advance(bytesRead);

                    if (exit)
                    {
                        writer.Complete();
                        break;
                    }
                }
                finally 
                {
                    await buffer.FlushAsync();
                }
            }
        }
    }
}
