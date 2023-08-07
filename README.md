
# YGOWiki

### YGODatabase ###
Database for the project, tables, data and implementing multilanguage.

### YGOWikiGRPC ###
gRPC Server using .net to consult information from Yu-Gi-Oh!

## Usage/Examples for YGOWikiGRPC


You can make a client for gRPC in the api that you want to consult information from 
gRPC Server - YGOWiki
```c#
var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
    {
        HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });

var client = new Greeter.GreeterClient(channel);
var response = await client.SayHelloAsync(new HelloRequest { Name = ".NET" });
```

You need to replace the url to "http://www.yugiserverwiki.somee.com/".

You can also try it, using Kreya. [Kreya - Calling APIs made easy](https://kreya.app/)

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1691386768/Github/Kreya01_huyzi8.png)

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1691386768/Github/Kreya02_jrzlpm.png)







For more information, you can check this [gRPC-Web in ASP.NET Core gRPC apps](https://learn.microsoft.com/en-us/aspnet/core/grpc/grpcweb?view=aspnetcore-7.0)


## Running Tests

To run tests, run ServerTest.cs from project ServerYGO.NUnit

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1691386166/Github/UnitTests_zyxrnk.png)

## Feedback

If you have any feedback, please reach out to me.
