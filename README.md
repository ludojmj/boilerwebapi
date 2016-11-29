# [boilerwebapi](https://github.com/ludojmj/boilerwebapi)

Self Hosted .NET 4.6.2 WebApi (OWIN)

* Front-end:
  * /BoilerWebApi.SelfHost/public/index.html + index.js

* Back-end:
  * /BoilerWebApi.SelfHost/Controllers/ProductController.cs

## Swagger
* Launch the http server:
  * boilerwebapi/BoilerWebApi.SelfHost/bin/**BoilerWebApi.SelfHost.exe**

* Open the url:
  * http://localhost:8080/swagger

## Layers

#### BoilerWebApi.SelfHost/public

* index.html
* index.js

  
#### BoilerWebApi.SelfHost/Controllers
 
* ProductController
  * **api/product/async**?input=1
    > ==> GET OK

  * **api/product**?input=0
    > ==> GET KO (intentional BoilerWebApi.BusinessException)
  
  * **api/product**{ 'Id': '1' }
    > ==> POST KO (unintentional System.DivideByZeroException)


#### BoilerWebApi.Logic
 
* IProductLogic.cs
* ProductLogic.cs

 
#### BoilerWebApi.Repository
 
* IProductRepo.cs
* ProductRepo.cs


#### BoilerWebApi.Models
 
* Product.cs


#### BoilerWebApi.Shared
 
* BusinessException.cs
  > ==> Voluntary BusinessException

* ConflictActionResult.cs : IHttpActionResult
  >  ==> Exceptions into HttpActionResult

* GlobalExceptionHandler.cs
  >  ==> Exceptions shielding
