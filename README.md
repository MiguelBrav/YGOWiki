
# YGOWiki

### YGODatabase ###
Database for the project, tables, data and implementing multilanguage.

### YGOWikiGRPC ###
gRPC Server using .net to consult information from Yu-Gi-Oh!

### YGOClient ###
REST API gateway consuming the gRPC server.

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

You need to replace the url to "https://ygowiki.application-service.work/".

You can also try it, using Kreya. [Kreya - Calling APIs made easy](https://kreya.app/)

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1695790335/Github/gRPCServer_egef6p.png)

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1691386768/Github/Kreya02_jrzlpm.png)


## Usage/Examples for YGOClient


An API composed solely of GET requests, with Swagger documentation available, that connects to a gRPC server to retrieve information.
The available languages ​​are es-mx (Spanish - Español) and en-us (English - Ingles)
YGO Client
```
curl -X 'GET' \
  'https://ygoclient.application-service.work/Attribute/all/es-mx' \
  -H 'accept: */*'
```
![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1706388137/Github/x9tns28fs6l0ecnvdypw.png)

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1706388209/Github/fd0b5b13mpddl9zfrj13.png)

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1706388237/Github/dvjsmmq0khjm29iidian.png)

You can consume the API using this URL: "https://ygoclient.application-service.work/".

For more information, you can check this swagger doc online about the ygo client. [YGO Client - Swagger Documentation](https://ygoclient.application-service.work/swagger/index.html)


## Running Tests

To run tests, run ServerTest.cs from project ServerYGO.NUnit

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1691386166/Github/UnitTests_zyxrnk.png)

## Versioning

Updated from .NET 6 to .NET 8 (20/10/25)

## Package References

All NuGet packages updated to compatible .NET 8 versions. (20/10/25)

## Feedback

If you have any feedback, please reach out to me.
