namespace finalProject
{
    partial class AssignCoursesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbStudents;
        private System.Windows.Forms.ComboBox cmbCourses;
        private System.Windows.Forms.Button btnAssignCourse;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbStudents = new System.Windows.Forms.ComboBox();
            this.cmbCourses = new System.Windows.Forms.ComboBox();
            this.btnAssignCourse = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // cmbStudents
            // 
            this.cmbStudents.FormattingEnabled = true;
            this.cmbStudents.Location = new System.Drawing.Point(12, 12);
            this.cmbStudents.Name = "cmbStudents";
            this.cmbStudents.Size = new System.Drawing.Size(260, 21);
            this.cmbStudents.TabIndex = 0;

            // 
            // cmbCourses
            // 
            this.cmbCourses.FormattingEnabled = true;
            this.cmbCourses.Location = new System.Drawing.Point(12, 39);
            this.cmbCourses.Name = "cmbCourses";
            this.cmbCourses.Size = new System.Drawing.Size(260, 21);
            this.cmbCourses.TabIndex = 1;

            // 
            // btnAssignCourse
            // 
            this.btnAssignCourse.Location = new System.Drawing.Point(12, 66);
            this.btnAssignCourse.Name = "btnAssignCourse";
            this.btnAssignCourse.Size = new System.Drawing.Size(75, 23);
            this.btnAssignCourse.TabIndex = 2;
            this.btnAssignCourse.Text = "Assign";
            this.btnAssignCourse.UseVisualStyleBackColor = true;
            this.btnAssignCourse.Click += new System.EventHandler(this.btnAssignCourse_Click);

            // 
            // AssignCoursesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.btnAssignCourse);
            this.Controls.Add(this.cmbCourses);
            this.Controls.Add(this.cmbStudents);
            this.Name = "AssignCoursesForm";
            this.Text = "Assign Courses";
            this.ResumeLayout(false);
        }
    }
}
