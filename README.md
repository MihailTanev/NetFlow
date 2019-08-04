<div>
    <img src="https://mihailtanev.blob.core.windows.net/netflow/netflow.PNG?sp=r&st=2019-08-03T14:28:04Z&se=2019-08-03T22:28:04Z&spr=https&sv=2018-03-28&sig=zsSu99GKnbYhySzSOUAFzbTWeraeX6%2F8mPUSy4mJcE8%3D&sr=b" />
</div>	


# [Netflow](https://netflow-core.azurewebsites.net/)  <img src="https://mihailtanev.blob.core.windows.net/netflow/2019-01-25_23-33-061.jpg?sp=r&st=2019-08-03T14:29:17Z&se=2019-08-03T22:29:17Z&spr=https&sv=2018-03-28&sig=AWGoBdxZJRvuG7eF%2FqhEdwkJm5TuyOomUVpQJpjtkj0%3D&sr=b" width="48px" height="48px" />

## User Friendly Platform for Digital Exams and Assessments
NetFlow is a digital exam platform that handles the flow of student exams and management of the assessment process.
It has three main user roles: Administrator, Student and Teacher. Administrator is authorized to perform a CRUD operations on the users. 
Student can register and login to their account. Student can view and sign up for desired educational courses. Student will be able
to upload his assignment as a .pdf or .zip file up till 10MB. Teacher is authorized to create courses and conducting assessment of
the student assignments. A search dialog at the Layout page allows the user to search for specific course or post.

## Implementation Technologies

-  Visual Studio Community Edition 2019
-  ASP.NET MVC CORE 2.2
-  Microsoft SQL Server
-  Cloudinary
-  Azure DevOps
-  Entity Framework Core
-  AutoMapper  
-  Razor
-  Bootstrap 4
-  HTML & CSS

## Online Demo (Azure Website)

[www.netflow-core.azurewebsites.net](http://netflow-core.azurewebsites.net)

## Configuration

- Download Visual Studio 2017 or higher
- Configure the connection string in appsettings.json file with the correct SQL Server instance
- Build the solution by pressing "Ctrl + F5"
- The back-office can be accessed via Admin Panel using the pre-created account : username: Administrator, password: admin123

## DevOps pipeline

[![Build Status](https://dev.azure.com/mihailvtanev/NetFlow/_apis/build/status/NetFlow-Azure%20Web%20App%20for%20ASP.NET-CI?branchName=master)](https://dev.azure.com/mihailvtanev/NetFlow/_build/latest?definitionId=23&branchName=master)

This build syncs between Github and Azure DevOps repo