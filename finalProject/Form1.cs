using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finalProject
{
    public partial class Form1 : Form
    {
        private List<Student> students;
        private int selectedIndex;
        private string connectionString = "server=localhost;database=mohammad;user=root;password=1234;";

        public Form1(List<Student> students)
        {
            InitializeComponent();
            this.students = students;
            selectedIndex = -1;
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
            UpdateStudentList();
        }

        private void AddStudentToDatabase(Student student)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Students (ID, Name, Email, BirthYear, EnteredYear) VALUES (@ID, @Name, @Email, @BirthYear, @EnteredYear)", connection);
                command.Parameters.AddWithValue("@ID", student.ID);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@BirthYear", student.BirthYear);
                command.Parameters.AddWithValue("@EnteredYear", student.EnteredYear);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateStudentInDatabase(Student student)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE Students SET Name = @Name, Email = @Email, BirthYear = @BirthYear, EnteredYear = @EnteredYear WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", student.ID);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@BirthYear", student.BirthYear);
                command.Parameters.AddWithValue("@EnteredYear", student.EnteredYear);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteStudentFromDatabase(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM Students WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtID.Text, out int id) &&
                int.TryParse(txtBirthYear.Text, out int birthYear) &&
                int.TryParse(txtEnteredYear.Text, out int enteredYear) &&
                !string.IsNullOrEmpty(txtName.Text) &&
                !string.IsNullOrEmpty(txtEmail.Text))
            {
                var student = new Student
                {
                    ID = id,
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    BirthYear = birthYear,
                    EnteredYear = enteredYear
                };
                students.Add(student);
                AddStudentToDatabase(student);
                UpdateStudentList();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please enter valid information for all fields.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < students.Count &&
                int.TryParse(txtID.Text, out int id) &&
                int.TryParse(txtBirthYear.Text, out int birthYear) &&
                int.TryParse(txtEnteredYear.Text, out int enteredYear) &&
                !string.IsNullOrEmpty(txtName.Text) &&
                !string.IsNullOrEmpty(txtEmail.Text))
            {
                var student = students[selectedIndex];
                student.ID = id;
                student.Name = txtName.Text.Trim();
                student.Email = txtEmail.Text.Trim();
                student.BirthYear = birthYear;
                student.EnteredYear = enteredYear;

                UpdateStudentInDatabase(student);
                UpdateStudentList();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a student to edit and enter valid information for all fields.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < students.Count)
            {
                var student = students[selectedIndex];
                students.RemoveAt(selectedIndex);
                DeleteStudentFromDatabase(student.ID);
                UpdateStudentList();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = lstStudents.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < students.Count)
            {
                var student = students[selectedIndex];
                txtID.Text = student.ID.ToString();
                txtName.Text = student.Name;
                txtEmail.Text = student.Email;
                txtBirthYear.Text = student.BirthYear.ToString();
                txtEnteredYear.Text = student.EnteredYear.ToString();
            }
        }

        private void UpdateStudentList()
        {
            lstStudents.DataSource = null;
            lstStudents.DataSource = students;
            lstStudents.DisplayMember = "ToString";
        }

        private void ClearInputFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtBirthYear.Clear();
            txtEnteredYear.Clear();
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int BirthYear { get; set; }
        public int EnteredYear { get; set; }

        public override string ToString()
        {
            return $"{ID}: {Name} ({Email}, Born: {BirthYear}, Entered: {EnteredYear})";
        }
    }
}
