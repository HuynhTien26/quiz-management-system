namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormTraCuu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public static string MaNhanSuDuocChon = "";
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lsvDuLieu = new ListView();
            columnHeader1 = new ColumnHeader();
            SuspendLayout();
            // 
            // lsvDuLieu
            // 
            lsvDuLieu.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            lsvDuLieu.Dock = DockStyle.Fill;
            lsvDuLieu.FullRowSelect = true;
            lsvDuLieu.Location = new Point(0, 0);
            lsvDuLieu.Name = "lsvDuLieu";
            lsvDuLieu.Size = new Size(800, 450);
            lsvDuLieu.TabIndex = 0;
            lsvDuLieu.UseCompatibleStateImageBehavior = false;
            lsvDuLieu.View = View.Details;
            lsvDuLieu.DoubleClick += lsvDuLieu_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tên Khoa";
            columnHeader1.Width = 200;
            // 
            // FormTraCuu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lsvDuLieu);
            Name = "FormTraCuu";
            Text = "FormTraCuu";
            Load += FormTraCuu_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView lsvDuLieu;
        private ColumnHeader columnHeader1;
    }
}