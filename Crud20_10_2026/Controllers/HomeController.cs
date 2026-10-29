using Crud20_10_2026.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud20_10_2026.Controllers
{
    public class HomeController : Controller
    {
        string cs = System.Configuration
            .ConfigurationManager
            .ConnectionStrings["constr"]
            .ConnectionString;

        // READ
        public ActionResult Index()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Student";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Student student = new Student();

                    student.Id = (int)dr["Id"];
                    student.Name = dr["Name"].ToString();
                    student.Email = dr["Email"].ToString();
                    student.Course = dr["Course"].ToString();

                    students.Add(student);
                }
            }

            return View(students);
        }


        // CREATE - GET
        public ActionResult Create()
        {
            return View();
        }


        // CREATE - POST
        [HttpPost]
        public ActionResult Create(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"INSERT INTO Student
                                 (Name, Email, Course)
                                 VALUES
                                 (@Name, @Email, @Course)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Course", student.Course);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }


        // EDIT - GET
        public ActionResult Edit(int id)
        {
            Student student = new Student();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Student WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    student.Id = (int)dr["Id"];
                    student.Name = dr["Name"].ToString();
                    student.Email = dr["Email"].ToString();
                    student.Course = dr["Course"].ToString();
                }
            }

            return View(student);
        }


        // EDIT - POST
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Student
                                 SET Name=@Name,
                                     Email=@Email,
                                     Course=@Course
                                 WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Course", student.Course);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }


        // DELETE
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Student WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

    }
}