# Code First Framework for C#

What is code first ?

Entity Framework introduced Code-First approach from Entity Framework 4.1. Code-First is mainly useful in Domain Driven Design. 
With the Code-First approach, you can focus on the domain design and start creating classes as per your domain requirement rather than design your database first and then create the classes which match your database design. Code-First APIs will create the database on the fly based on your entity classes and configuration. 

(Reffered from http://www.entityframeworktutorial.net)

In our current solution we are having 5 main projects

  1.CF.Model
  
  This project contains the model classes (Database tables) and Enums. Model classes are grouped by based on database schema and they save   in different folders named by schema.
  
  2.CF.Data
  
  3.CF.Business
  
  4.CF.ViewModel
  
  5.CF.Web
