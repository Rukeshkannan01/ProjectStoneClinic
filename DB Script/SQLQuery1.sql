go
-- Login Page ---
create table LoginTable
(
UserName varchar(20),
FirstName varchar(20),
LastName varchar(20),
Password varchar(20),
constraint UK_login unique (UserName)
)
go
insert LoginTable values('Kavi07','Kavi','B','1234')
insert LoginTable values('Rukesh01','RukeshKumar','K','1111')


select * from LoginTable
go

create proc LoginValidate
@username varchar(20),
@password varchar(20)
as
select UserName from LoginTable
where(UserName like @username and Password like @password)
go


------ Add Doctor -------
create table DoctorTable
(
DoctorID int identity(1,1),
FirstName varchar(20),
LastName varchar(20),
Gender varchar(10),
Specialization varchar(30),
VisitingHours varchar(30)
constraint PK_DocId primary key (DoctorID)
)

select * from DoctorTable

create proc AddDoctorInfo
@fname varchar(20),
@lname varchar(30),
@gender varchar(10),
@specialization varchar(30),
@visiting varchar(30)
as
insert into DoctorTable(FirstName,LastName,Gender,Specialization,VisitingHours)
values(@fname,@lname,@gender,@specialization,@visiting)
go

-------- Add Patient --------
create table PatientTable
(
PatientID int identity(1,1),
FirstName varchar(20),
LastName varchar(20),
Gender varchar(10),
Age int, -- range (0-120)
DateOfBirth date,
constraint PK_PatientId primary key (PatientID)
)

go
create proc AddPatientInfo
@fname varchar(20),
@lname varchar(30),
@gender varchar(10),
@age int,
@dob date
as
insert into PatientTable(FirstName,LastName,Gender,Age,DateOfBirth)
values(@fname,@lname,@gender,@age,@dob)

select * from PatientTable


-------- Add Appointment -------

create table AppointmentTable
(
AppointmentID int identity(1,1),
PatientID int,
Specialization varchar(30),
Doctor varchar(20),
VisitDate date,-- Date
AppointmentTime varchar(20) -- Time
constraint PK_Apt primary key (AppointmentID),
constraint FK_Patient foreign key(PatientID) references PatientTable(PatientID)
)


select * from AppointmentTable
go

create procedure InstAppointment
@patientid varchar(20),
@specialization varchar(30),
@doctor varchar(10),
@visitdate date,
@appointmenttime varchar(20)
as
insert into AppointmentTable(PatientID,Specialization, Doctor, VisitDate, AppointmentTime)
values(@patientid,@specialization,@doctor,@visitdate,@appointmenttime)


go

--create proc AddAppointment
--@patientid varchar(20),
--@specialization varchar(30),
--@doctor varchar(10),
--@visitdate date,
--@appointmenttime varchar(20)
--as
--insert into AppointmentTable(PatientID,Specialization, Doctor, VisitDate, AppointmentTime)
--values(@patientid,@specialization,@doctor,@visitdate,@appointmenttime)

--create proc CancelAppointment
--@patientid int
--as
--delete from AppointmentTable where PatientID = @patientid

--------- Cancel Appointment --------
create proc CancelAppointment
(
@patientid int,
@appointmentid int
)
as
delete from AppointmentTable 
where PatientID = @patientid and AppointmentID = @appointmentid
go

------ Show Appointment ---------

--create proc ShowAppointment
--as
--select AppointmentID,PatientID,VisitDate
--from AppointmentTable
go

--create proc ShowAndDelete
--@patientid int
--as
--select from AppointmentTable where PatientID = @patientid

create proc ShowSchedule
as
select AppointmentID, PatientID,  convert(varchar,VisitDate) as VisitDate, AppointmentTime from AppointmentTable
where VisitDate >= convert (varchar, GETDATE())

--select convert(varchar, getdate(),23)

go

------- show appointment by patientID and visitdate -------
create proc ShowById
@patientid int,
@visitdate date
as
select AppointmentID, PatientID, Specialization, Doctor, VisitDate,AppointmentTime from AppointmentTable
where (PatientID = @patientid and VisitDate = @visitdate)




create proc ListAppointment
as
select AppointmentID, PatientID, Specialization, Doctor, VisitDate,AppointmentTime from AppointmentTable


select FirstName, VisitingHours from DoctorTable
where (Specialization = )



