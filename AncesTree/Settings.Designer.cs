﻿namespace AncesTree
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnMaleColor = new AncesTree.Controls.ColorButton();
            this.btnFemaleColor = new AncesTree.Controls.ColorButton();
            this.btnUnknownColor = new AncesTree.Controls.ColorButton();
            this.btnBackColor = new AncesTree.Controls.ColorButton();
            this.SuspendLayout();
            // 
            // treePanel21
            // 
            this.treePanel21.BackColor = System.Drawing.Color.Beige;
            this.treePanel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treePanel21.Location = new System.Drawing.Point(323, 37);
            this.treePanel21.Name = "treePanel21";
            this.treePanel21.Size = new System.Drawing.Size(320, 300);
            this.treePanel21.TabIndex = 0;
            this.treePanel21.TreeMargin = 10;
            this.treePanel21.Zoom = 1F;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 11);
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
            this.label2.Location = new System.Drawing.Point(9, 102);
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
            this.lineStyleCombo1.Location = new System.Drawing.Point(116, 102);
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
            this.lineWeightCombo1.Location = new System.Drawing.Point(180, 102);
            this.lineWeightCombo1.Name = "lineWeightCombo1";
            this.lineWeightCombo1.Size = new System.Drawing.Size(52, 21);
            this.lineWeightCombo1.TabIndex = 5;
            this.lineWeightCombo1.SelectedIndexChanged += new System.EventHandler(this.lineWeightCombo1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(80, 102);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Relations Font:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Spouse Font:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(97, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(97, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Male Color:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Female Color:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Unknown Color:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 339);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Back Color:";
            // 
            // btnMaleColor
            // 
            this.btnMaleColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnMaleColor.Location = new System.Drawing.Point(106, 242);
            this.btnMaleColor.Name = "btnMaleColor";
            this.btnMaleColor.Size = new System.Drawing.Size(32, 23);
            this.btnMaleColor.TabIndex = 16;
            this.btnMaleColor.UseVisualStyleBackColor = false;
            this.btnMaleColor.Value = System.Drawing.SystemColors.Control;
            this.btnMaleColor.OnColorChange += new AncesTree.Controls.ColorButton.ColorChanged(this.OnColorChange);
            // 
            // btnFemaleColor
            // 
            this.btnFemaleColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnFemaleColor.Location = new System.Drawing.Point(106, 277);
            this.btnFemaleColor.Name = "btnFemaleColor";
            this.btnFemaleColor.Size = new System.Drawing.Size(32, 23);
            this.btnFemaleColor.TabIndex = 17;
            this.btnFemaleColor.UseVisualStyleBackColor = false;
            this.btnFemaleColor.Value = System.Drawing.SystemColors.Control;
            this.btnFemaleColor.OnColorChange += new AncesTree.Controls.ColorButton.ColorChanged(this.OnColorChange);
            // 
            // btnUnknownColor
            // 
            this.btnUnknownColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnknownColor.Location = new System.Drawing.Point(106, 308);
            this.btnUnknownColor.Name = "btnUnknownColor";
            this.btnUnknownColor.Size = new System.Drawing.Size(32, 23);
            this.btnUnknownColor.TabIndex = 18;
            this.btnUnknownColor.UseVisualStyleBackColor = false;
            this.btnUnknownColor.Value = System.Drawing.SystemColors.Control;
            this.btnUnknownColor.OnColorChange += new AncesTree.Controls.ColorButton.ColorChanged(this.OnColorChange);
            // 
            // btnBackColor
            // 
            this.btnBackColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnBackColor.Location = new System.Drawing.Point(106, 339);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(32, 23);
            this.btnBackColor.TabIndex = 19;
            this.btnBackColor.UseVisualStyleBackColor = false;
            this.btnBackColor.Value = System.Drawing.SystemColors.Control;
            this.btnBackColor.OnColorChange += new AncesTree.Controls.ColorButton.ColorChanged(this.OnColorChange);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 464);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.btnUnknownColor);
            this.Controls.Add(this.btnFemaleColor);
            this.Controls.Add(this.btnMaleColor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Controls.ColorButton btnMaleColor;
        private Controls.ColorButton btnFemaleColor;
        private Controls.ColorButton btnUnknownColor;
        private Controls.ColorButton btnBackColor;
    }
}