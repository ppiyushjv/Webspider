namespace WebSpider
{
    partial class ProductPriority
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridProduct = new System.Windows.Forms.DataGridView();
            this.txtpart = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VENDOR_TITLE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VDR_PART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeastCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridProduct);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 251);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product List";
            // 
            // gridProduct
            // 
            this.gridProduct.AllowUserToAddRows = false;
            this.gridProduct.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gridProduct.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridProduct.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VENDOR_TITLE_NAME,
            this.VDR_PART,
            this.PART_NUM,
            this.LeastCount,
            this.Priority});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProduct.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridProduct.Location = new System.Drawing.Point(3, 16);
            this.gridProduct.MultiSelect = false;
            this.gridProduct.Name = "gridProduct";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridProduct.RowHeadersVisible = false;
            this.gridProduct.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 8F);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.gridProduct.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProduct.Size = new System.Drawing.Size(714, 232);
            this.gridProduct.TabIndex = 56;
            this.gridProduct.TabStop = false;
            this.gridProduct.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProduct_CellEndEdit);
            this.gridProduct.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridProduct_DataError);
            // 
            // txtpart
            // 
            this.txtpart.Location = new System.Drawing.Point(592, 257);
            this.txtpart.Name = "txtpart";
            this.txtpart.Size = new System.Drawing.Size(125, 20);
            this.txtpart.TabIndex = 2;
            this.txtpart.TextChanged += new System.EventHandler(this.txtpart_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(503, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search Part No.";
            // 
            // VENDOR_TITLE_NAME
            // 
            this.VENDOR_TITLE_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VENDOR_TITLE_NAME.DataPropertyName = "VendorName";
            this.VENDOR_TITLE_NAME.HeaderText = "VENDOR NAME";
            this.VENDOR_TITLE_NAME.Name = "VENDOR_TITLE_NAME";
            this.VENDOR_TITLE_NAME.ReadOnly = true;
            // 
            // VDR_PART
            // 
            this.VDR_PART.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VDR_PART.DataPropertyName = "ProductDescription";
            this.VDR_PART.HeaderText = "VDR PART";
            this.VDR_PART.Name = "VDR_PART";
            this.VDR_PART.ReadOnly = true;
            // 
            // PART_NUM
            // 
            this.PART_NUM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PART_NUM.DataPropertyName = "AdiNumber";
            this.PART_NUM.HeaderText = "PART NUM";
            this.PART_NUM.Name = "PART_NUM";
            this.PART_NUM.ReadOnly = true;
            // 
            // LeastCount
            // 
            this.LeastCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LeastCount.DataPropertyName = "LeastCount";
            this.LeastCount.HeaderText = "Least Count";
            this.LeastCount.Name = "LeastCount";
            // 
            // Priority
            // 
            this.Priority.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Priority.DataPropertyName = "PriorityProduct";
            this.Priority.HeaderText = "Priority";
            this.Priority.Name = "Priority";
            // 
            // ProductPriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 286);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtpart);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ProductPriority";
            this.Text = "ProductPriority";
            this.Load += new System.EventHandler(this.ProductPriority_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.DataGridView gridProduct;
        private System.Windows.Forms.TextBox txtpart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VENDOR_TITLE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn VDR_PART;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeastCount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Priority;
    }
}