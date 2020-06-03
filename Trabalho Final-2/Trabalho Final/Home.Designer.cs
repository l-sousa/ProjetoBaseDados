namespace Trabalho_Final
{
    partial class Home
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AdiConsulta = new System.Windows.Forms.Button();
            this.btnProcuraConsulta = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // AdiConsulta
            // 
            this.AdiConsulta.Location = new System.Drawing.Point(94, 471);
            this.AdiConsulta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AdiConsulta.Name = "AdiConsulta";
            this.AdiConsulta.Size = new System.Drawing.Size(209, 34);
            this.AdiConsulta.TabIndex = 13;
            this.AdiConsulta.Text = "Adicionar Consulta";
            this.AdiConsulta.UseVisualStyleBackColor = true;
            this.AdiConsulta.Click += new System.EventHandler(this.AdiConsulta_Click);
            // 
            // btnProcuraConsulta
            // 
            this.btnProcuraConsulta.Location = new System.Drawing.Point(399, 428);
            this.btnProcuraConsulta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProcuraConsulta.Name = "btnProcuraConsulta";
            this.btnProcuraConsulta.Size = new System.Drawing.Size(105, 28);
            this.btnProcuraConsulta.TabIndex = 12;
            this.btnProcuraConsulta.Text = "Procurar";
            this.btnProcuraConsulta.UseVisualStyleBackColor = true;
            this.btnProcuraConsulta.Click += new System.EventHandler(this.btnProcuraConsulta_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.textBox1.Location = new System.Drawing.Point(94, 428);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 28);
            this.textBox1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Consultas";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(94, 55);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(826, 357);
            this.dataGridView1.TabIndex = 9;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AdiConsulta);
            this.Controls.Add(this.btnProcuraConsulta);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Home";
            this.Size = new System.Drawing.Size(1020, 523);
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AdiConsulta;
        private System.Windows.Forms.Button btnProcuraConsulta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
