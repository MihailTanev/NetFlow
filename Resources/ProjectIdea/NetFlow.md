# Project - NetFlow

## Type - User Friendly Platform for Digital Exams and Assessments

## Description

NetFlow is a digital exam platform that handles the flow of student exams and management of the assessment process. 
It has three main user roles: Administrator, Student and Teacher.
Administrator is authorized to perform a CRUD operations on the users. 
Student can register and login to their account. 
Student can view and sign up for desired educational courses. 
Student will be able to upload his assignment as a .pdf or .zip file up till 10MB. 
Teacher is authorized to create courses and conducting assessment of the student assignments. 
A search dialog at the Layout page allows the user to
search for specific course or post. The main implementation technologies used for the project are :
Visual Studio Community Edition 2019, ASP.NET MVC CORE 2.2,
Microsoft SQL Server, Entity Framework Core, AutoMapper, Razor, GitHub, Bootstrap 4, HTML & CSS.

## Entities

### User
  - Id (string)
  - Username (string)
  - Password (string)
  - Email (string)
  - First Name (string)
  - Last Name (string)
  - Phone Number (string)
  - Post (Collection of Posts)
  - Course (Collection of Courses)
  - Comment (Collection of Comments)
### Course
  - Id (string)
  - Name (string)
  - Description (string)
  - Credit (int)</li>
  - StartDate (datetime)
  - EndDate (dateTime)
  - Students (Collection of Students)
### Enrollment
  - StudentId (User)
  - CourseId (Course)
  - Grade (enum) 
  - Assignment (byte)
### Post
  - Id (int)</li>
  - Description (string)
  - CreatedDate (dateTime)
  - User (User)
  - Comments (list of Comment)
### Comment
  - Id (string)
  - Content (string)
  - Post (Post)
  - User (User)