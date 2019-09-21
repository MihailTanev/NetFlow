<div>
    <img src="https://res.cloudinary.com/netflow/image/upload/v1565790934/NetFlow/Images/netflow_h4cich.png" />
</div>	


# [Netflow](https://netflow-aspcore.azurewebsites.net/)  <img src="https://res.cloudinary.com/netflow/image/upload/v1565790933/NetFlow/Images/2019-01-25_23-33-061_uccy3t.jpg" width="48px" height="48px" />

## User Friendly Platform for Digital Exams and Assessments
NetFlow is a digital exam platform that handles the flow of student exams and management of the assessment process.
It has four main user roles: Administrator, Student, Teacher and Publisher. 

Administrator:

 - Create, Edit, Delete courses
 - Create, Edit, Delete users
 - Search users by username
 - Assign a role to users

Student:

- Create an account
- Sign up for desired educational courses
- Uploading assignment as a .pdf or .zip file up till 10MB

Teacher:

- Conduct assessment
- Comment student assignments
- Has a list of assigned courses

Publisher:
- Create, Edit, Delete posts

Authorized users can leave a comments under each post. 
A search dialog at the Layout page allows the user to search for specific course.

## Implementation Technologies

-  Visual Studio Enterprise Edition 2019
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
- Build the solution "Ctrl + F5"
- The back-office can be accessed via Admin Panel using the pre-created account : username: Administrator, password: admin123;
Publisher,publisher123; Student,student123; Teacher,teacher123

## DevOps pipeline

[![Build Status](https://dev.azure.com/mihailtanevazuredevops/NetFlow/_apis/build/status/NetFlow-CI?branchName=master)](https://dev.azure.com/mihailtanevazuredevops/NetFlow/_build/latest?definitionId=1&branchName=master)
This build syncs between Github and Azure DevOps repo