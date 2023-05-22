// See https://aka.ms/new-console-template for more information
using Basics;
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");
var options = new GrpcChannelOptions()
{

};

using var channel = GrpcChannel.ForAddress("https://localhost:7067");

var client = new Greeter.GreeterClient(channel);
// Unary
//var response = client.SayHello(new Request() { Name = "Hello" });

//Console.WriteLine(response.Message);
//Console.ReadKey();

//Console.WriteLine("------------");
var callOptions = new CallOptions() { };

//using var serverStream = client.ServerStream(new Request() { Name = "Hello" }, callOptions);
//try
//{
//    await foreach (var item in serverStream.ResponseStream.ReadAllAsync())
//    {
//        Console.WriteLine(item.Message);
//    }
//}
//catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
//{

//    Console.WriteLine(ex.Message + "ups, deadline!");
//}


//try
//{
//    using var call = client.ClientStream();

//    for (var i = 0; i < 100; i++)
//    {
//        Console.WriteLine($"Sending {i}");
//        await call.RequestStream.WriteAsync(new Request { Name = i.ToString() });
//    }

//    await call.RequestStream.CompleteAsync();
//    Response response = await call;

//    Console.WriteLine($"{response.Message} is the last value server received");
//}
//catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
//{
//    Console.WriteLine("Stream cancelled.");
//}



Console.WriteLine("_________");
try
{


    using (var call = client.Bidirectional(deadline: DateTime.UtcNow.AddMilliseconds(1)))
    {
        var responseReaderTask = Task.Run(async () =>
        {
            while (await call.ResponseStream.MoveNext())
            {
                Response message = call.ResponseStream.Current;
                Console.WriteLine("Received " + message.Message);
            }
        });

        var request = new Request();
        for (int i = 0; i < 10; i++)
        {
            request.Name = i.ToString();
            Console.WriteLine("Sending " + request.Name);
            await call.RequestStream.WriteAsync(request);
        }
        await call.RequestStream.CompleteAsync();
        await responseReaderTask;
    }
}
catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
{
    Console.WriteLine("Exceded!");
}

Console.ReadKey();