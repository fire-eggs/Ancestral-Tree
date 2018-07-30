namespace AncesTree.Controls
{
    partial class PenStyle
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
            this.colorButton1 = new AncesTree.Controls.ColorButton();
            this.lineStyleCombo1 = new WindowsFormsApplication1.LineStyleCombo();
            this.lineWeightCombo1 = new WindowsFormsApplication1.LineWeightCombo();
            this.SuspendLayout();
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = System.Drawing.SystemColors.Control;
            this.colorButton1.Location = new System.Drawing.Point(2, 3);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(39, 23);
            this.colorButton1.TabIndex = 0;
            this.colorButton1.UseVisualStyleBackColor = false;
            this.colorButton1.Value = System.Drawing.SystemColors.Control;
            this.colorButton1.OnColorChange += new AncesTree.Controls.ColorButton.ColorChanged(this.colorButton1_OnColorChange);
            // 
            // lineStyleCombo1
            // 
            this.lineStyleCombo1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lineStyleCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineStyleCombo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineStyleCombo1.FormattingEnabled = true;
            this.lineStyleCombo1.Location = new System.Drawing.Point(47, 3);
            this.lineStyleCombo1.Name = "lineStyleCombo1";
            this.lineStyleCombo1.Size = new System.Drawing.Size(74, 23);
            this.lineStyleCombo1.TabIndex = 1;
            this.lineStyleCombo1.SelectedIndexChanged += new System.EventHandler(this.lineStyleCombo1_SelectedIndexChanged);
            // 
            // lineWeightCombo1
            // 
            this.lineWeightCombo1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lineWeightCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineWeightCombo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineWeightCombo1.FormattingEnabled = true;
            this.lineWeightCombo1.Location = new System.Drawing.Point(128, 3);
            this.lineWeightCombo1.Name = "lineWeightCombo1";
            this.lineWeightCombo1.Size = new System.Drawing.Size(75, 23);
            this.lineWeightCombo1.TabIndex = 2;
            this.lineWeightCombo1.SelectedIndexChanged += new System.EventHandler(this.lineWeightCombo1_SelectedIndexChanged);
            // 
            // PenStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lineWeightCombo1);
            this.Controls.Add(this.lineStyleCombo1);
            this.Controls.Add(this.colorButton1);
            this.Name = "PenStyle";
            this.Size = new System.Drawing.Size(211, 31);
            this.ResumeLayout(false);

        }

        #endregion

        private ColorButton colorButton1;
        private WindowsFormsApplication1.LineStyleCombo lineStyleCombo1;
        private WindowsFormsApplication1.LineWeightCombo lineWeightCombo1;
    }
}
