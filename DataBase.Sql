
CREATE DATABASE StudentDB6;

USE StudentDB6;


CREATE TABLE Student
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100),
    Course VARCHAR(100)
)


select * from Student;

insert into Student (Name,Email,Course)values('Ankit','A@gmail.com','B.tech');
