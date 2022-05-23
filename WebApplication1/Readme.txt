=> I used XML documentation for my commenting in the controller's folder which was done by (triple forward slash). This results in the comments displaying beside the endpoints when the application is ran.
=> Warnings can be suppressed from the solutions property window.
=> Used automapper instead of the regular manual mapper. 
=> Used a return type of the Get method for the HttpPost.
=> Used Code-first approach for the database. 
=> The connection string is different compared to the previous ones.
=> Use postman more 
=> Had a better grasp of Dto and used it in the controller(and why we should use Dto's and not expose the domain model to the outside world)
=> Foreign Key and how it binds to a table.
=> Eager loading was used to check where the national park id equalled the trailsId.
=> Versioning our API's and having different documents for them. 
=> API versioning requires we remove our APIExploresettings and create a class for swag gen.
table no tracking works for methods like get 
while table works for update and others that you need to track
=> Picture is an array of bytes not string here.
=> for the mvc project,the url of the swagger was used in the SD.
=> a razor.runtime nuget package was installed and added to the startup of the mvc.
=> In order to consume our API's we need to make http calls.
=> Implemented the base calass keyword in the NAtionalParkRepo and TrailPArkRepo
=> used dataset.net ,toastr,fontawesome,JqueryUi,sweetalert
=> Upsert represents Update and delete. In the web controller action method, the reason the Id is nullable is because for create you dont have an Id while for update,there's already an Id.

