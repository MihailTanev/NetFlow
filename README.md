<div>
    <img src="https://res.cloudinary.com/netflow/image/upload/v1565790934/NetFlow/Images/netflow_h4cich.png" />
</div>	


# [Netflow](https://netflow-aspcore.azurewebsites.net/)  <img src="https://res.cloudinary.com/netflow/image/upload/v1565790933/NetFlow/Images/2019-01-25_23-33-061_uccy3t.jpg" width="48px" height="48px" />

## User Friendly Platform for Digital Exams and Assessments
NetFlow is a digital exam platform that handles the flow of student exams and management of the assessment process.
It has four main user roles: Administrator, Student, Teacher and Publisher. Administrator is authorized to create courses and perform 
a CRUD operations on the users. Administrator can create a new user and assign a role to it. Can search users by username.
Student can register and login to their account. Student can sign up for desired educational courses. Student will be able
to upload his assignment as a .pdf or .zip file up till 10MB. Teacher is authorized to conducting assessment and commenting of
the student assignments. Publisher is authorized to create a blog posts. Authorized users can leave a comments under the blog posts.
A search dialog at the Layout page allows the user to search for specific course or post.

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

[www.netflow-aspcore.azurewebsites.net/](https://netflow-aspcore.azurewebsites.net/)

## Configuration

- Download Visual Studio 2017 or higher
- Configure the connection string in appsettings.json file with the correct SQL Server instance
- Build the solution by pressing "Ctrl + F5"
- The back-office can be accessed via Admin Panel using the pre-created account : username: Administrator, password: admin123

## DevOps pipeline

[![Build Status](https://dev.azure.com/mihailvtanev/NetFlow/_apis/build/status/NetFlow-Azure%20Web%20App%20for%20ASP.NET-CI?branchName=master)](https://dev.azure.com/mihailvtanev/NetFlow/_build/latest?definitionId=23&branchName=master)

This build syncs between Github and Azure DevOps repo