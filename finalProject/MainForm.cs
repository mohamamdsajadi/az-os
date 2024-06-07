using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finalProject
{
    public partial class MainForm : Form
    {
        private List<Student> students;
        private string connectionString = "server=localhost;database=mohammad;user=root;password=1234;";

        public MainForm()
        {
            InitializeComponent();
            students = new List<Student>();
            LoadStudentsFromDatabase();
        }

        private void LoadStudentsFromDatabase()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Students", connection);
                var reader = command.ExecuteReader();
                students.Clear();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        ID = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        Email = reader.GetString("Email"),
                        BirthYear = reader.GetInt32("BirthYear"),
                        EnteredYear = reader.GetInt32("EnteredYear")
                    });
                }
                reader.Close();
            }
        }

        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            Form1 manageStudentsForm = new Form1(students);
            manageStudentsForm.ShowDialog();
        }

        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            ManageCoursesForm manageCoursesForm = new ManageCoursesForm();
            manageCoursesForm.ShowDialog();
        }

        private void btnAssignCourses_Click(object sender, EventArgs e)
        {
            AssignCoursesForm assignCoursesForm = new AssignCoursesForm(students);
            assignCoursesForm.ShowDialog();
        }

        private void btnExportToTxt_Click(object sender, EventArgs e)
        {
            Thread exportThread = new Thread(new ThreadStart(ExportDataToTxt));
            exportThread.Start();
        }

        private void ExportDataToTxt()
        {
            string exportPath = "students_by_course.txt";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(@"
                    SELECT c.Name AS CourseName, s.Name AS StudentName, s.Email, s.BirthYear, s.EnteredYear 
                    FROM Students s
                    JOIN StudentCourses sc ON s.ID = sc.StudentID
                    JOIN Courses c ON sc.CourseID = c.ID
                    ORDER BY c.Name, s.Name", connection);

                using (var writer = new StreamWriter(exportPath))
                {
                    var reader = command.ExecuteReader();
                    string currentCourse = string.Empty;

                    while (reader.Read())
                    {
                        string courseName = reader.GetString("CourseName");
                        if (courseName != currentCourse)
                        {
                            currentCourse = courseName;
                            writer.WriteLine($"Course: {currentCourse}");
                        }
                        string studentInfo = $"  Student: {reader.GetString("StudentName")}, Email: {reader.GetString("Email")}, Birth Year: {reader.GetInt32("BirthYear")}, Entered Year: {reader.GetInt32("EnteredYear")}";
                        writer.WriteLine(studentInfo);
                    }
                    reader.Close();
                }
            }
            MessageBox.Show("Export completed successfully!", "Export to Txt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpenChrome_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("chrome.exe", "https://golestan.sutech.ac.ir/") { UseShellExecute = true });
        }

        private void btnGetNews_Click(object sender, EventArgs e)
        {
            Thread newsThread = new Thread(new ThreadStart(GetNews));
            newsThread.Start();
        }

private async void GetNews()
{
    string url = "https://www.bbc.com/news";

    try
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody); // Debug: print response body

                string pattern = @"<h2[^>]*>(.*?)</h2>";
                MatchCollection matches = Regex.Matches(responseBody, pattern, RegexOptions.Singleline);

                StringBuilder newsContent = new StringBuilder();
                foreach (Match match in matches)
                {
                    string paragraphContent = match.Groups[1].Value;
                    newsContent.AppendLine(paragraphContent);
                }

                MessageBox.Show(newsContent.ToString(), "BBC News", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Error fetching news: {response.StatusCode} - {response.ReasonPhrase}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error fetching news: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}



    }
    }
