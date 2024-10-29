namespace TarifRehberi
{
	partial class Form2
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
			this.dataGridViewRecipes = new System.Windows.Forms.DataGridView();
			this.searchBox = new System.Windows.Forms.TextBox();
			this.ingredientSearchBox = new System.Windows.Forms.TextBox();
			this.searchButton = new System.Windows.Forms.Button();
			this.comboBoxCategory = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxIngredientCount = new System.Windows.Forms.ComboBox();
			this.comboBoxCostRange = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBoxSort = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecipes)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewRecipes
			// 
			this.dataGridViewRecipes.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
			this.dataGridViewRecipes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRecipes.Location = new System.Drawing.Point(12, 102);
			this.dataGridViewRecipes.Name = "dataGridViewRecipes";
			this.dataGridViewRecipes.Size = new System.Drawing.Size(1319, 667);
			this.dataGridViewRecipes.TabIndex = 0;
			this.dataGridViewRecipes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewRecipes_CellDoubleClick);
			// 
			// searchBox
			// 
			this.searchBox.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.searchBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.searchBox.Location = new System.Drawing.Point(97, 21);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(156, 20);
			this.searchBox.TabIndex = 1;
			// 
			// ingredientSearchBox
			// 
			this.ingredientSearchBox.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.ingredientSearchBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ingredientSearchBox.Location = new System.Drawing.Point(97, 64);
			this.ingredientSearchBox.Name = "ingredientSearchBox";
			this.ingredientSearchBox.Size = new System.Drawing.Size(156, 20);
			this.ingredientSearchBox.TabIndex = 2;
			// 
			// searchButton
			// 
			this.searchButton.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.searchButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.searchButton.Location = new System.Drawing.Point(259, 21);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(69, 66);
			this.searchButton.TabIndex = 3;
			this.searchButton.Text = "Ara";
			this.searchButton.UseVisualStyleBackColor = false;
			this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// comboBoxCategory
			// 
			this.comboBoxCategory.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboBoxCategory.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.comboBoxCategory.FormattingEnabled = true;
			this.comboBoxCategory.Location = new System.Drawing.Point(1187, 18);
			this.comboBoxCategory.Name = "comboBoxCategory";
			this.comboBoxCategory.Size = new System.Drawing.Size(99, 21);
			this.comboBoxCategory.TabIndex = 4;
			this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(151, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "arama çubuğu";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(661, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "sıralama";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label3.Location = new System.Drawing.Point(1167, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "filtreleme";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label4.Location = new System.Drawing.Point(1097, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "kategori";
			// 
			// comboBoxIngredientCount
			// 
			this.comboBoxIngredientCount.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboBoxIngredientCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.comboBoxIngredientCount.FormattingEnabled = true;
			this.comboBoxIngredientCount.Location = new System.Drawing.Point(1187, 45);
			this.comboBoxIngredientCount.Name = "comboBoxIngredientCount";
			this.comboBoxIngredientCount.Size = new System.Drawing.Size(99, 21);
			this.comboBoxIngredientCount.TabIndex = 9;
			this.comboBoxIngredientCount.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// comboBoxCostRange
			// 
			this.comboBoxCostRange.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboBoxCostRange.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.comboBoxCostRange.FormattingEnabled = true;
			this.comboBoxCostRange.Location = new System.Drawing.Point(1187, 72);
			this.comboBoxCostRange.Name = "comboBoxCostRange";
			this.comboBoxCostRange.Size = new System.Drawing.Size(99, 21);
			this.comboBoxCostRange.TabIndex = 10;
			this.comboBoxCostRange.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label5.Location = new System.Drawing.Point(1097, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "malzeme sayısı";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label6.Location = new System.Drawing.Point(1100, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(39, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "maliyet";
			// 
			// comboBoxSort
			// 
			this.comboBoxSort.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboBoxSort.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.comboBoxSort.FormattingEnabled = true;
			this.comboBoxSort.Location = new System.Drawing.Point(627, 45);
			this.comboBoxSort.Name = "comboBoxSort";
			this.comboBoxSort.Size = new System.Drawing.Size(121, 21);
			this.comboBoxSort.TabIndex = 13;
			this.comboBoxSort.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.ClientSize = new System.Drawing.Size(1343, 781);
			this.Controls.Add(this.comboBoxSort);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboBoxCostRange);
			this.Controls.Add(this.comboBoxIngredientCount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxCategory);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.ingredientSearchBox);
			this.Controls.Add(this.searchBox);
			this.Controls.Add(this.dataGridViewRecipes);
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecipes)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewRecipes;
		private System.Windows.Forms.TextBox searchBox;
		private System.Windows.Forms.TextBox ingredientSearchBox;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.ComboBox comboBoxCategory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxIngredientCount;
		private System.Windows.Forms.ComboBox comboBoxCostRange;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox comboBoxSort;
	}
}