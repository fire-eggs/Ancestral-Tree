namespace AncesTree
{
    partial class Settings
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
            this.treePanel21 = new AncesTree.TreePanel2();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lineStyleCombo1 = new WindowsFormsApplication1.LineStyleCombo();
            this.lineWeightCombo1 = new WindowsFormsApplication1.LineWeightCombo();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treePanel21
            // 
            this.treePanel21.BackColor = System.Drawing.Color.Beige;
            this.treePanel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treePanel21.DrawFont = null;
            this.treePanel21.Location = new System.Drawing.Point(392, 35);
            this.treePanel21.Name = "treePanel21";
            this.treePanel21.Size = new System.Drawing.Size(320, 300);
            this.treePanel21.SpouseFont = null;
            this.treePanel21.TabIndex = 0;
            this.treePanel21.TreeMargin = 10;
            this.treePanel21.Zoom = 1F;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Preview:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Box Outline:";
            // 
            // lineStyleCombo1
            // 
            this.lineStyleCombo1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lineStyleCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineStyleCombo1.FormattingEnabled = true;
            this.lineStyleCombo1.Location = new System.Drawing.Point(120, 24);
            this.lineStyleCombo1.Name = "lineStyleCombo1";
            this.lineStyleCombo1.Size = new System.Drawing.Size(57, 21);
            this.lineStyleCombo1.TabIndex = 4;
            this.lineStyleCombo1.SelectedIndexChanged += new System.EventHandler(this.lineStyleCombo1_SelectedIndexChanged);
            // 
            // lineWeightCombo1
            // 
            this.lineWeightCombo1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lineWeightCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineWeightCombo1.FormattingEnabled = true;
            this.lineWeightCombo1.Location = new System.Drawing.Point(184, 24);
            this.lineWeightCombo1.Name = "lineWeightCombo1";
            this.lineWeightCombo1.Size = new System.Drawing.Size(52, 21);
            this.lineWeightCombo1.TabIndex = 5;
            this.lineWeightCombo1.SelectedIndexChanged += new System.EventHandler(this.lineWeightCombo1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(84, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 464);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lineWeightCombo1);
            this.Controls.Add(this.lineStyleCombo1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treePanel21);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreePanel2 treePanel21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private WindowsFormsApplication1.LineStyleCombo lineStyleCombo1;
        private WindowsFormsApplication1.LineWeightCombo lineWeightCombo1;
        private System.Windows.Forms.Button button2;
    }
}