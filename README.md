# [boilerwebapi](https://github.com/ludojmj/boilerwebapi)
## .NET 4.6.2 WebApi (OWIN)

```
$ git clone https://github.com/ludojmj/boilerwebapi.git
  - Self Hosted: BoilerWebApi project > Properties > Output type > Console Application
  - IIS Hosted : BoilerWebApi project > Properties > Output type > Class Library
```

* Front-end:
  * /BoilerWebApi/public/index.html + index.js + index.css

* Back-end:
  * /BoilerWebApi/Controllers/ProductController.cs
  * /BoilerWebApi/Controllers/OtherProductController.cs

## Swagger
* Launch the http server:
  * boilerwebapi/BoilerWebApi/bin/**BoilerWebApi.exe**

* Open the url:
  * http://localhost:8080/swagger

## Layers

#### BoilerWebApi/public

* index.html
* index.js
* index.css

  
#### BoilerWebApi/Controllers
 
* ProductController id=1 => KO
  * **api/product**?id=1
    >  ==> GET KO (intentional BoilerWebApi.BusinessException)
  
  * **api/product/async**?id=1
    >  ==> GET KO (intentional BoilerWebApi.BusinessException)

* ProductController id=0 => OK
  * **api/product**?id=0
    >  ==> GET OK
  
  * **api/product/async**?id=0
    >  ==> GET OK


* OtherProductController input.Id="1" => KO
  * **api/otherproduct** { Id = "1", Lib = "Label1" }
    >  ==> POSTT KO (unintentional System.DivideByZeroException)
  
  * **api/product/async**  { Id = "1", Lib = "Label1" }
    >  ==> POST KO (unintentional System.DivideByZeroException)

* OtherProductController input.Id="0" => OK
  * **api/product**  { Id = "0", Lib = "Label1" }
    >  ==> POST OK
  
  * **api/product/async**  { Id = "0", Lib = "Label1" }
    >  ==> POST OK

 
#### BoilerWebApi.Repository
 
* IProductRepo.cs
* IOtherProductRepo.cs

* ProductRepo.cs
* OtherProductRepo.cs


#### BoilerWebApi.Models
 
* Product.cs


#### BoilerWebApi.Shared
 
* ErrorContent.cs
  > ==> Formatted message object { Message, MessageDetail }

* BusinessException.cs : Exception
  > ==> Voluntary BusinessException

* ConflictActionResult.cs : IHttpActionResult
  >  ==> Exceptions into HttpActionResult

* GlobalExceptionHandler.cs : ExceptionHandler
  >  ==> Exceptions shielding
