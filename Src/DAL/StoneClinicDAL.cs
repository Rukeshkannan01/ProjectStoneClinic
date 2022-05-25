using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using StoneClinic.Models;

namespace StoneClinic.DAL
{
    public class StoneClinicDAL
    {
        public string cnn = "";

        public StoneClinicDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cnn = builder.GetSection("ConnectionStrings:ClinicLink").Value;
        }
        public int LoginValidate(Staff s)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("LoginValidate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", s.UserName);
            cmd.Parameters.AddWithValue("@password", s.Password);
            con.Open();
            IDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                return (1);
            con.Close();
            return (0);
        }

        public int AddPatientInfo(Patient p)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("AddPatientInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fname", p.FirstName);
            cmd.Parameters.AddWithValue("@lname", p.LastName);
            cmd.Parameters.AddWithValue("@gender", p.Sex);
            cmd.Parameters.AddWithValue("@age", p.Age);
            cmd.Parameters.AddWithValue("@dob", p.DateOfBirth);
            con.Open();
            int inst = cmd.ExecuteNonQuery();
            con.Close();            
            return inst;
        }

        public int AddDoctorInfo(Doctor d)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("AddDoctorInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fname", d.FirstName);
            cmd.Parameters.AddWithValue("@lname", d.LastName);
            cmd.Parameters.AddWithValue("@gender", d.Sex);
            cmd.Parameters.AddWithValue("@specialization", d.Specialization);
            cmd.Parameters.AddWithValue("@visiting", d.VisitingHours);
            con.Open();
            int inst = cmd.ExecuteNonQuery();
            con.Close();
            return inst;
        }
        //public int AddAppointment(Appointment a)
        //{
        //    SqlConnection con = new SqlConnection(cnn);
        //    SqlCommand cmd = new SqlCommand("AddAppointment", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@patientid", a.PatientID);
        //    cmd.Parameters.AddWithValue("@specialization", a.Specialization);
        //    cmd.Parameters.AddWithValue("@doctor", a.Doctor);
        //    cmd.Parameters.AddWithValue("@visitdate", a.VisitDate);
        //    cmd.Parameters.AddWithValue("@appointmenttime", a.AppointmentTime);
        //    con.Open();
        //    int inst = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return inst;
        //}

        public int ScheduleAppointment(Appointment a)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("InstAppointment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@patientid", a.PatientID);
            cmd.Parameters.AddWithValue("@specialization", a.Specialization);
            cmd.Parameters.AddWithValue("@doctor", a.Doctor);
            cmd.Parameters.AddWithValue("@visitdate", a.VisitDate);
            cmd.Parameters.AddWithValue("@appointmenttime", a.AppointmentTime);
            con.Open();
            int inst = cmd.ExecuteNonQuery();
            con.Close();
            return inst;
        }
        public List<Appointment> ShowAppointment()
        {
            List<Appointment> listSchedule = new List<Appointment>();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("ShowSchedule", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listSchedule.Add(new Appointment()
                        {
                            AppointmentID = int.Parse(reader["AppointmentID"].ToString()),
                            PatientID = int.Parse(reader["PatientID"].ToString()),                            
                            VisitDate = reader["VisitDate"].ToString(),
                            AppointmentTime = reader["AppointmentTime"].ToString()
                        }); ;
                    }
                }
            }
            return listSchedule;
        }

        //public List<Appointment> ListAppointmentById(int PatientID, string VisitDate)
        //{
        //    List<Appointment> listappt = new List<Appointment>();
        //    using (SqlConnection con = new SqlConnection(cnn))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("ListAppointment", con))
        //        {
        //            if (con.State == ConnectionState.Closed)
        //                con.Open();

        //            cmd.Parameters.AddWithValue("@patientid", PatientID);
        //            cmd.Parameters.AddWithValue("@visitdate", VisitDate);
        //            IDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                listappt.Add(new Appointment()
        //                {
        //                    PatientID = int.Parse(reader["PatientID"].ToString()),
        //                    Specialization = reader["Specialization"].ToString(),
        //                    Doctor = reader["Doctor"].ToString(),
        //                    VisitDate = reader["VisitDate"].ToString(),
        //                    AppointmentTime = reader["AppointmentTime"].ToString()
        //                }); ;
        //            }
        //        }
        //    }
        //    return listappt;
        //}
        public bool DeleteAppointment(int id,int aptid)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("CancelAppointment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@patientid", id);
            cmd.Parameters.AddWithValue("@appointmentid", aptid);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }

        public bool ListAppointmentById(int id, string visitDate)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("ShowById", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@patientid", id);
            cmd.Parameters.AddWithValue("@visitdate", visitDate);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }


        public List<Appointment> ListAppointment(int id, string visitdate)
        {
            List<Appointment> listSchedule = new List<Appointment>();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("ShowById", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.Parameters.Add("@patientid", SqlDbType.Int);
                    cmd.Parameters["@patientid"].Value = id;
                    cmd.Parameters.Add("@visitdate");
                    cmd.Parameters["@visitdate"].Value = visitdate;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listSchedule.Add(new Appointment()
                        {
                            AppointmentID = int.Parse(reader["AppointmentID"].ToString()),
                            PatientID = int.Parse(reader["PatientID"].ToString()),
                            Specialization = reader["Specialization"].ToString(),
                            Doctor = reader["Doctor"].ToString(),
                            VisitDate = reader["VisitDate"].ToString(),
                            AppointmentTime = reader["AppointmentTime"].ToString()
                        }); ;
                    }
                }
            }
            return listSchedule;
        }


        //public int CancelAppointment(Appointment c)
        //{
        //    SqlConnection con = new SqlConnection(cnn);
        //    SqlCommand cmd = new SqlCommand("CancelAppointment", con);
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@patientid", c.PatientID);
        //    con.Open();
        //    int result = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return result;
        //}


    }
}
