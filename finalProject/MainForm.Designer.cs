namespace finalProject
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Button btnManageCourses;
        private System.Windows.Forms.Button btnAssignCourses;
        private System.Windows.Forms.Button btnManageStudents;
        private System.Windows.Forms.Button btnExportToTxt;
        private System.Windows.Forms.Button btnOpenChrome;
        private System.Windows.Forms.Button btnGetNews;

        private void InitializeComponent()
        {
            this.btnManageStudents = new System.Windows.Forms.Button();
            this.btnManageCourses = new System.Windows.Forms.Button();
            this.btnAssignCourses = new System.Windows.Forms.Button();
            this.btnExportToTxt = new System.Windows.Forms.Button();
            this.btnOpenChrome = new System.Windows.Forms.Button();
            this.btnGetNews = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // btnManageCourses
            // 
            this.btnManageCourses.Location = new System.Drawing.Point(100, 50);
            this.btnManageCourses.Name = "btnManageCourses";
            this.btnManageCourses.Size = new System.Drawing.Size(150, 50);
            this.btnManageCourses.TabIndex = 1;
            this.btnManageCourses.Text = "Manage Courses";
            this.btnManageCourses.UseVisualStyleBackColor = true;
            this.btnManageCourses.Click += new System.EventHandler(this.btnManageCourses_Click);
            
            // 
            // btnAssignCourses
            // 
            this.btnAssignCourses.Location = new System.Drawing.Point(100, 120);
            this.btnAssignCourses.Name = "btnAssignCourses";
            this.btnAssignCourses.Size = new System.Drawing.Size(150, 50);
            this.btnAssignCourses.TabIndex = 2;
            this.btnAssignCourses.Text = "Assign Courses";
            this.btnAssignCourses.UseVisualStyleBackColor = true;
            this.btnAssignCourses.Click += new System.EventHandler(this.btnAssignCourses_Click);
            
            // 
            // btnManageStudents
            // 
            this.btnManageStudents.Location = new System.Drawing.Point(100, 190);
            this.btnManageStudents.Name = "btnManageStudents";
            this.btnManageStudents.Size = new System.Drawing.Size(150, 50);
            this.btnManageStudents.TabIndex = 0;
            this.btnManageStudents.Text = "Manage Student List";
            this.btnManageStudents.UseVisualStyleBackColor = true;
            this.btnManageStudents.Click += new System.EventHandler(this.btnManageStudents_Click);
            
            // 
            // btnExportToTxt
            // 
            this.btnExportToTxt.Location = new System.Drawing.Point(100, 260);
            this.btnExportToTxt.Name = "btnExportToTxt";
            this.btnExportToTxt.Size = new System.Drawing.Size(150, 50);
            this.btnExportToTxt.TabIndex = 3;
            this.btnExportToTxt.Text = "Export to Txt";
            this.btnExportToTxt.UseVisualStyleBackColor = true;
            this.btnExportToTxt.Click += new System.EventHandler(this.btnExportToTxt_Click);
            
            // 
            // btnOpenChrome
            // 
            this.btnOpenChrome.Location = new System.Drawing.Point(100, 330);
            this.btnOpenChrome.Name = "btnOpenChrome";
            this.btnOpenChrome.Size = new System.Drawing.Size(150, 50);
            this.btnOpenChrome.TabIndex = 4;
            this.btnOpenChrome.Text = "Open Chrome";
            this.btnOpenChrome.UseVisualStyleBackColor = true;
            this.btnOpenChrome.Click += new System.EventHandler(this.btnOpenChrome_Click);
            
            // 
            // btnGetNews
            // 
            this.btnGetNews.Location = new System.Drawing.Point(100, 400);
            this.btnGetNews.Name = "btnGetNews";
            this.btnGetNews.Size = new System.Drawing.Size(150, 50);
            this.btnGetNews.TabIndex = 5;
            this.btnGetNews.Text = "Get News";
            this.btnGetNews.UseVisualStyleBackColor = true;
            this.btnGetNews.Click += new System.EventHandler(this.btnGetNews_Click);
            
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 470);
            this.Controls.Add(this.btnManageCourses);
            this.Controls.Add(this.btnAssignCourses);
            this.Controls.Add(this.btnManageStudents);
            this.Controls.Add(this.btnExportToTxt);
            this.Controls.Add(this.btnOpenChrome);
            this.Controls.Add(this.btnGetNews);
            this.Name = "MainForm";
            this.Text = "Student Manager Main Form";
            this.ResumeLayout(false);
        }
    }
}
