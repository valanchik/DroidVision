namespace YoloDetection
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.autoMovingStatus = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.timeController = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.calcTimeValue = new System.Windows.Forms.Label();
            this.calcTimeTitle = new System.Windows.Forms.Label();
            this.YoloRunTimeValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.isVectorInverted = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(16, 145);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1136, 591);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Загрузить YOLO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoMovingStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.timeController);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.calcTimeValue);
            this.groupBox1.Controls.Add(this.calcTimeTitle);
            this.groupBox1.Controls.Add(this.YoloRunTimeValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(800, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(352, 123);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // autoMovingStatus
            // 
            this.autoMovingStatus.BackColor = System.Drawing.Color.Red;
            this.autoMovingStatus.Location = new System.Drawing.Point(192, 87);
            this.autoMovingStatus.Name = "autoMovingStatus";
            this.autoMovingStatus.Size = new System.Drawing.Size(61, 14);
            this.autoMovingStatus.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Автоматический контроль:";
            // 
            // timeController
            // 
            this.timeController.AutoSize = true;
            this.timeController.Location = new System.Drawing.Point(206, 67);
            this.timeController.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeController.Name = "timeController";
            this.timeController.Size = new System.Drawing.Size(38, 17);
            this.timeController.TabIndex = 9;
            this.timeController.Text = "0 ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Время отклика контроллера: ";
            // 
            // calcTimeValue
            // 
            this.calcTimeValue.AutoSize = true;
            this.calcTimeValue.Location = new System.Drawing.Point(120, 50);
            this.calcTimeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.calcTimeValue.Name = "calcTimeValue";
            this.calcTimeValue.Size = new System.Drawing.Size(38, 17);
            this.calcTimeValue.TabIndex = 7;
            this.calcTimeValue.Text = "0 ms";
            // 
            // calcTimeTitle
            // 
            this.calcTimeTitle.AutoSize = true;
            this.calcTimeTitle.Location = new System.Drawing.Point(8, 50);
            this.calcTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.calcTimeTitle.Name = "calcTimeTitle";
            this.calcTimeTitle.Size = new System.Drawing.Size(123, 17);
            this.calcTimeTitle.TabIndex = 6;
            this.calcTimeTitle.Text = "Время расчётов: ";
            // 
            // YoloRunTimeValue
            // 
            this.YoloRunTimeValue.AutoSize = true;
            this.YoloRunTimeValue.Location = new System.Drawing.Point(120, 34);
            this.YoloRunTimeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.YoloRunTimeValue.Name = "YoloRunTimeValue";
            this.YoloRunTimeValue.Size = new System.Drawing.Size(38, 17);
            this.YoloRunTimeValue.TabIndex = 5;
            this.YoloRunTimeValue.Text = "0 ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Время детекта: ";
            // 
            // isVectorInverted
            // 
            this.isVectorInverted.AutoSize = true;
            this.isVectorInverted.Location = new System.Drawing.Point(279, 76);
            this.isVectorInverted.Name = "isVectorInverted";
            this.isVectorInverted.Size = new System.Drawing.Size(268, 21);
            this.isVectorInverted.TabIndex = 4;
            this.isVectorInverted.Text = "Инверсия вектора движения мышки";
            this.isVectorInverted.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 762);
            this.Controls.Add(this.isVectorInverted);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label YoloRunTimeValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label calcTimeValue;
        private System.Windows.Forms.Label calcTimeTitle;
        private System.Windows.Forms.Label timeController;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel autoMovingStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox isVectorInverted;
    }
}

