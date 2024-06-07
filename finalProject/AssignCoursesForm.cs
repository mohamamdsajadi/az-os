using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finalProject
{
    public partial class AssignCoursesForm : Form
    {
        private List<Student> students;
        private List<Course> courses;
        private string connectionString = "server=localhost;database=mohammad;user=root;password=1234;";

        public AssignCoursesForm(List<Student> students)
        {
            InitializeComponent();
            this.students = students;
            courses = new List<Course>();
            LoadCoursesFromDatabase();
            LoadStudentsToComboBox();
        }

        private void LoadCoursesFromDatabase()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Courses", connection);
                var reader = command.ExecuteReader();
                courses.Clear();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        ID = reader.GetInt32("ID"),
                        Name = reader.GetString("Name")
                    });
                }
                reader.Close();
            }
            UpdateCourseList();
        }

        private void LoadStudentsToComboBox()
        {
            cmbStudents.DataSource = null;
            cmbStudents.DataSource = students;
            cmbStudents.DisplayMember = "Name";
        }

        private void btnAssignCourse_Click(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedIndex >= 0 && cmbCourses.SelectedIndex >= 0)
            {
                var selectedStudent = (Student)cmbStudents.SelectedItem;
                var selectedCourse = (Course)cmbCourses.SelectedItem;

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new MySqlCommand("INSERT INTO StudentCourses (StudentID, CourseID) VALUES (@StudentID, @CourseID)", connection);
                    command.Parameters.AddWithValue("@StudentID", selectedStudent.ID);
                    command.Parameters.AddWithValue("@CourseID", selectedCourse.ID);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show($"Assigned {selectedStudent.Name} to {selectedCourse.Name}");
            }
            else
            {
                MessageBox.Show("Please select both a student and a course.");
            }
        }

        private void UpdateCourseList()
        {
            cmbCourses.DataSource = null;
            cmbCourses.DataSource = courses;
            cmbCourses.DisplayMember = "Name";
        }
    }
}
