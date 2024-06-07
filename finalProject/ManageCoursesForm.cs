using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finalProject
{
    public partial class ManageCoursesForm : Form
    {
        private List<Course> courses;
        private string connectionString = "server=localhost;database=mohammad;user=root;password=1234;";

        public ManageCoursesForm()
        {
            InitializeComponent();
            courses = new List<Course>();
            LoadCoursesFromDatabase();
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

        private void AddCourseToDatabase(Course course)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Courses (Name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", course.Name);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateCourseInDatabase(Course course)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE Courses SET Name = @Name WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", course.ID);
                command.Parameters.AddWithValue("@Name", course.Name);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteCourseFromDatabase(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM Courses WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCourseName.Text))
            {
                var course = new Course
                {
                    Name = txtCourseName.Text.Trim()
                };
                courses.Add(course);
                AddCourseToDatabase(course);
                UpdateCourseList();
                txtCourseName.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid course name.");
            }
        }

        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            if (selectedCourseIndex >= 0 && selectedCourseIndex < courses.Count &&
                !string.IsNullOrEmpty(txtCourseName.Text))
            {
                var course = courses[selectedCourseIndex];
                course.Name = txtCourseName.Text.Trim();
                UpdateCourseInDatabase(course);
                UpdateCourseList();
                txtCourseName.Clear();
            }
            else
            {
                MessageBox.Show("Please select a course to edit and enter a valid name.");
            }
        }

        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            if (selectedCourseIndex >= 0 && selectedCourseIndex < courses.Count)
            {
                var course = courses[selectedCourseIndex];
                courses.RemoveAt(selectedCourseIndex);
                DeleteCourseFromDatabase(course.ID);
                UpdateCourseList();
                txtCourseName.Clear();
            }
            else
            {
                MessageBox.Show("Please select a course to delete.");
            }
        }

        private void lstCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCourseIndex = lstCourses.SelectedIndex;
            if (selectedCourseIndex >= 0 && selectedCourseIndex < courses.Count)
            {
                var course = courses[selectedCourseIndex];
                txtCourseName.Text = course.Name;
            }
        }

        private void UpdateCourseList()
        {
            lstCourses.DataSource = null;
            lstCourses.DataSource = courses;
            lstCourses.DisplayMember = "Name";
        }
    }

    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
