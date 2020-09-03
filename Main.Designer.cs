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
            this.button1 = new System.Windows.Forms.Button();
            this.createDetectedImg = new System.Windows.Forms.CheckBox();
            this.saveImgPrefix = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.IsWriteStream = new System.Windows.Forms.CheckBox();
            this.drawImage = new System.Windows.Forms.CheckBox();
            this.movingY = new System.Windows.Forms.CheckBox();
            this.movingX = new System.Windows.Forms.CheckBox();
            this.canFire = new System.Windows.Forms.CheckBox();
            this.CanMouseMove = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mouseCoeff = new System.Windows.Forms.TextBox();
            this.autoCalibration = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.calibrate = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.led13 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mouseTimeout = new System.Windows.Forms.TextBox();
            this.mouseClickType = new System.Windows.Forms.ComboBox();
            this.gc_commnad = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.avgOffsetObject = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.autoMovingStatus = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.timeController = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.calcTimeValue = new System.Windows.Forms.Label();
            this.calcTimeTitle = new System.Windows.Forms.Label();
            this.YoloRunTimeValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.onlyFire = new System.Windows.Forms.CheckBox();
            this.isVectorInverted = new System.Windows.Forms.CheckBox();
            this.maxFirePErSecond = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.turnTimeOutValue = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.covariance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.kalmanError = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(900, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "Загрузить YOLO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // createDetectedImg
            // 
            this.createDetectedImg.AutoSize = true;
            this.createDetectedImg.Location = new System.Drawing.Point(892, 392);
            this.createDetectedImg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createDetectedImg.Name = "createDetectedImg";
            this.createDetectedImg.Size = new System.Drawing.Size(118, 17);
            this.createDetectedImg.TabIndex = 16;
            this.createDetectedImg.Text = "Создать картинки";
            this.createDetectedImg.UseVisualStyleBackColor = true;
            this.createDetectedImg.CheckedChanged += new System.EventHandler(this.createDetectedImg_CheckedChanged);
            // 
            // saveImgPrefix
            // 
            this.saveImgPrefix.Location = new System.Drawing.Point(892, 409);
            this.saveImgPrefix.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveImgPrefix.Name = "saveImgPrefix";
            this.saveImgPrefix.Size = new System.Drawing.Size(113, 20);
            this.saveImgPrefix.TabIndex = 4;
            this.saveImgPrefix.Text = "d";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1313, 595);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.saveImgPrefix);
            this.tabPage1.Controls.Add(this.createDetectedImg);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1305, 569);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Robot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1305, 569);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Разметка";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.IsWriteStream);
            this.groupBox5.Controls.Add(this.drawImage);
            this.groupBox5.Controls.Add(this.movingY);
            this.groupBox5.Controls.Add(this.movingX);
            this.groupBox5.Controls.Add(this.canFire);
            this.groupBox5.Controls.Add(this.CanMouseMove);
            this.groupBox5.Location = new System.Drawing.Point(1164, 337);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(133, 155);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = " Дополнительно: ";
            // 
            // IsWriteStream
            // 
            this.IsWriteStream.AutoSize = true;
            this.IsWriteStream.Location = new System.Drawing.Point(6, 128);
            this.IsWriteStream.Margin = new System.Windows.Forms.Padding(2);
            this.IsWriteStream.Name = "IsWriteStream";
            this.IsWriteStream.Size = new System.Drawing.Size(88, 17);
            this.IsWriteStream.TabIndex = 26;
            this.IsWriteStream.Text = "Записывать";
            this.IsWriteStream.UseVisualStyleBackColor = true;
            // 
            // drawImage
            // 
            this.drawImage.AutoSize = true;
            this.drawImage.Checked = true;
            this.drawImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawImage.Location = new System.Drawing.Point(6, 106);
            this.drawImage.Margin = new System.Windows.Forms.Padding(2);
            this.drawImage.Name = "drawImage";
            this.drawImage.Size = new System.Drawing.Size(131, 17);
            this.drawImage.TabIndex = 25;
            this.drawImage.Text = "Отрисовка картинки";
            this.drawImage.UseVisualStyleBackColor = true;
            // 
            // movingY
            // 
            this.movingY.AutoSize = true;
            this.movingY.Checked = true;
            this.movingY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.movingY.Location = new System.Drawing.Point(5, 84);
            this.movingY.Margin = new System.Windows.Forms.Padding(2);
            this.movingY.Name = "movingY";
            this.movingY.Size = new System.Drawing.Size(97, 17);
            this.movingY.TabIndex = 24;
            this.movingY.Text = "Двигать  по Y";
            this.movingY.UseVisualStyleBackColor = true;
            // 
            // movingX
            // 
            this.movingX.AutoSize = true;
            this.movingX.Checked = true;
            this.movingX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.movingX.Location = new System.Drawing.Point(5, 62);
            this.movingX.Margin = new System.Windows.Forms.Padding(2);
            this.movingX.Name = "movingX";
            this.movingX.Size = new System.Drawing.Size(94, 17);
            this.movingX.TabIndex = 23;
            this.movingX.Text = "Двигать по X";
            this.movingX.UseVisualStyleBackColor = true;
            // 
            // canFire
            // 
            this.canFire.AutoSize = true;
            this.canFire.Checked = true;
            this.canFire.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canFire.Location = new System.Drawing.Point(5, 40);
            this.canFire.Margin = new System.Windows.Forms.Padding(2);
            this.canFire.Name = "canFire";
            this.canFire.Size = new System.Drawing.Size(73, 17);
            this.canFire.TabIndex = 22;
            this.canFire.Text = "Стрелять";
            this.canFire.UseVisualStyleBackColor = true;
            // 
            // CanMouseMove
            // 
            this.CanMouseMove.AutoSize = true;
            this.CanMouseMove.Checked = true;
            this.CanMouseMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CanMouseMove.Location = new System.Drawing.Point(5, 18);
            this.CanMouseMove.Margin = new System.Windows.Forms.Padding(2);
            this.CanMouseMove.Name = "CanMouseMove";
            this.CanMouseMove.Size = new System.Drawing.Size(130, 17);
            this.CanMouseMove.TabIndex = 21;
            this.CanMouseMove.Text = "Перемещать мышку";
            this.CanMouseMove.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mouseCoeff);
            this.groupBox4.Controls.Add(this.autoCalibration);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.calibrate);
            this.groupBox4.Location = new System.Drawing.Point(1164, 205);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(133, 127);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " Калибровка мышки ";
            // 
            // mouseCoeff
            // 
            this.mouseCoeff.Location = new System.Drawing.Point(65, 38);
            this.mouseCoeff.Margin = new System.Windows.Forms.Padding(2);
            this.mouseCoeff.Name = "mouseCoeff";
            this.mouseCoeff.Size = new System.Drawing.Size(51, 20);
            this.mouseCoeff.TabIndex = 11;
            this.mouseCoeff.Text = "0,1231";
            // 
            // autoCalibration
            // 
            this.autoCalibration.AutoSize = true;
            this.autoCalibration.Location = new System.Drawing.Point(7, 74);
            this.autoCalibration.Margin = new System.Windows.Forms.Padding(2);
            this.autoCalibration.Name = "autoCalibration";
            this.autoCalibration.Size = new System.Drawing.Size(113, 17);
            this.autoCalibration.TabIndex = 13;
            this.autoCalibration.Text = "Авто калибровка";
            this.autoCalibration.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 41);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Поправка: ";
            // 
            // calibrate
            // 
            this.calibrate.AutoSize = true;
            this.calibrate.Location = new System.Drawing.Point(7, 17);
            this.calibrate.Margin = new System.Windows.Forms.Padding(2);
            this.calibrate.Name = "calibrate";
            this.calibrate.Size = new System.Drawing.Size(92, 17);
            this.calibrate.TabIndex = 9;
            this.calibrate.Text = "Калибровать";
            this.calibrate.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.led13);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.mouseTimeout);
            this.groupBox3.Controls.Add(this.mouseClickType);
            this.groupBox3.Controls.Add(this.gc_commnad);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(1164, 3);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(133, 197);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Тест gameController";
            // 
            // led13
            // 
            this.led13.AutoSize = true;
            this.led13.Checked = true;
            this.led13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.led13.Location = new System.Drawing.Point(4, 141);
            this.led13.Margin = new System.Windows.Forms.Padding(2);
            this.led13.Name = "led13";
            this.led13.Size = new System.Drawing.Size(102, 17);
            this.led13.TabIndex = 15;
            this.led13.Text = "Включить диод";
            this.led13.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 101);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Задержка клика";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 63);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Клик";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Координаты";
            // 
            // mouseTimeout
            // 
            this.mouseTimeout.Location = new System.Drawing.Point(4, 118);
            this.mouseTimeout.Margin = new System.Windows.Forms.Padding(2);
            this.mouseTimeout.Name = "mouseTimeout";
            this.mouseTimeout.Size = new System.Drawing.Size(112, 20);
            this.mouseTimeout.TabIndex = 9;
            this.mouseTimeout.Text = "100";
            // 
            // mouseClickType
            // 
            this.mouseClickType.AllowDrop = true;
            this.mouseClickType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mouseClickType.FormattingEnabled = true;
            this.mouseClickType.Items.AddRange(new object[] {
            "None",
            "LeftBtn",
            "RightBtn"});
            this.mouseClickType.Location = new System.Drawing.Point(4, 79);
            this.mouseClickType.Margin = new System.Windows.Forms.Padding(2);
            this.mouseClickType.Name = "mouseClickType";
            this.mouseClickType.Size = new System.Drawing.Size(112, 21);
            this.mouseClickType.TabIndex = 8;
            // 
            // gc_commnad
            // 
            this.gc_commnad.Location = new System.Drawing.Point(4, 37);
            this.gc_commnad.Margin = new System.Windows.Forms.Padding(2);
            this.gc_commnad.Name = "gc_commnad";
            this.gc_commnad.Size = new System.Drawing.Size(112, 20);
            this.gc_commnad.TabIndex = 7;
            this.gc_commnad.Text = "10x10";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 173);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 19);
            this.button2.TabIndex = 6;
            this.button2.Text = "Тест";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.avgOffsetObject);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.autoMovingStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.timeController);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.calcTimeValue);
            this.groupBox1.Controls.Add(this.calcTimeTitle);
            this.groupBox1.Controls.Add(this.YoloRunTimeValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(892, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(264, 100);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // avgOffsetObject
            // 
            this.avgOffsetObject.AutoSize = true;
            this.avgOffsetObject.Location = new System.Drawing.Point(154, 24);
            this.avgOffsetObject.Name = "avgOffsetObject";
            this.avgOffsetObject.Size = new System.Drawing.Size(13, 13);
            this.avgOffsetObject.TabIndex = 12;
            this.avgOffsetObject.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Среднее смещение объекта: ";
            // 
            // autoMovingStatus
            // 
            this.autoMovingStatus.BackColor = System.Drawing.Color.Red;
            this.autoMovingStatus.Location = new System.Drawing.Point(144, 80);
            this.autoMovingStatus.Margin = new System.Windows.Forms.Padding(2);
            this.autoMovingStatus.Name = "autoMovingStatus";
            this.autoMovingStatus.Size = new System.Drawing.Size(46, 11);
            this.autoMovingStatus.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Автоматический контроль:";
            // 
            // timeController
            // 
            this.timeController.AutoSize = true;
            this.timeController.Location = new System.Drawing.Point(154, 64);
            this.timeController.Name = "timeController";
            this.timeController.Size = new System.Drawing.Size(29, 13);
            this.timeController.TabIndex = 9;
            this.timeController.Text = "0 ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Время отклика контроллера: ";
            // 
            // calcTimeValue
            // 
            this.calcTimeValue.AutoSize = true;
            this.calcTimeValue.Location = new System.Drawing.Point(90, 50);
            this.calcTimeValue.Name = "calcTimeValue";
            this.calcTimeValue.Size = new System.Drawing.Size(29, 13);
            this.calcTimeValue.TabIndex = 7;
            this.calcTimeValue.Text = "0 ms";
            // 
            // calcTimeTitle
            // 
            this.calcTimeTitle.AutoSize = true;
            this.calcTimeTitle.Location = new System.Drawing.Point(6, 50);
            this.calcTimeTitle.Name = "calcTimeTitle";
            this.calcTimeTitle.Size = new System.Drawing.Size(95, 13);
            this.calcTimeTitle.TabIndex = 6;
            this.calcTimeTitle.Text = "Время расчётов: ";
            // 
            // YoloRunTimeValue
            // 
            this.YoloRunTimeValue.AutoSize = true;
            this.YoloRunTimeValue.Location = new System.Drawing.Point(90, 37);
            this.YoloRunTimeValue.Name = "YoloRunTimeValue";
            this.YoloRunTimeValue.Size = new System.Drawing.Size(29, 13);
            this.YoloRunTimeValue.TabIndex = 5;
            this.YoloRunTimeValue.Text = "0 ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Время детекта: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(881, 558);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.onlyFire);
            this.groupBox6.Controls.Add(this.isVectorInverted);
            this.groupBox6.Controls.Add(this.maxFirePErSecond);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.turnTimeOutValue);
            this.groupBox6.Location = new System.Drawing.Point(892, 110);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(264, 119);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = " Управление огнём ";
            // 
            // onlyFire
            // 
            this.onlyFire.AutoSize = true;
            this.onlyFire.Location = new System.Drawing.Point(5, 49);
            this.onlyFire.Margin = new System.Windows.Forms.Padding(2);
            this.onlyFire.Name = "onlyFire";
            this.onlyFire.Size = new System.Drawing.Size(223, 17);
            this.onlyFire.TabIndex = 8;
            this.onlyFire.Text = "стрелять есть объект в районе центра";
            this.onlyFire.UseVisualStyleBackColor = true;
            // 
            // isVectorInverted
            // 
            this.isVectorInverted.AutoSize = true;
            this.isVectorInverted.Location = new System.Drawing.Point(5, 23);
            this.isVectorInverted.Margin = new System.Windows.Forms.Padding(2);
            this.isVectorInverted.Name = "isVectorInverted";
            this.isVectorInverted.Size = new System.Drawing.Size(212, 17);
            this.isVectorInverted.TabIndex = 4;
            this.isVectorInverted.Text = "Инверсия вектора движения мышки";
            this.isVectorInverted.UseVisualStyleBackColor = true;
            // 
            // maxFirePErSecond
            // 
            this.maxFirePErSecond.Location = new System.Drawing.Point(132, 67);
            this.maxFirePErSecond.Margin = new System.Windows.Forms.Padding(2);
            this.maxFirePErSecond.Name = "maxFirePErSecond";
            this.maxFirePErSecond.Size = new System.Drawing.Size(53, 20);
            this.maxFirePErSecond.TabIndex = 9;
            this.maxFirePErSecond.Text = "6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 70);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Макс. выстрелов в сек.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 93);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Задержка авт. очереди:";
            // 
            // turnTimeOutValue
            // 
            this.turnTimeOutValue.Location = new System.Drawing.Point(131, 91);
            this.turnTimeOutValue.Margin = new System.Windows.Forms.Padding(2);
            this.turnTimeOutValue.Name = "turnTimeOutValue";
            this.turnTimeOutValue.Size = new System.Drawing.Size(51, 20);
            this.turnTimeOutValue.TabIndex = 10;
            this.turnTimeOutValue.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.covariance);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.kalmanError);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(892, 234);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(264, 98);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Фильтр Калмана";
            // 
            // covariance
            // 
            this.covariance.Location = new System.Drawing.Point(130, 49);
            this.covariance.Margin = new System.Windows.Forms.Padding(2);
            this.covariance.Name = "covariance";
            this.covariance.Size = new System.Drawing.Size(51, 20);
            this.covariance.TabIndex = 3;
            this.covariance.Text = "15";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Ковариант:";
            // 
            // kalmanError
            // 
            this.kalmanError.Location = new System.Drawing.Point(130, 26);
            this.kalmanError.Margin = new System.Windows.Forms.Padding(2);
            this.kalmanError.Name = "kalmanError";
            this.kalmanError.Size = new System.Drawing.Size(51, 20);
            this.kalmanError.TabIndex = 1;
            this.kalmanError.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ошибка измерения(R):";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 596);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "DroidVision";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox createDetectedImg;
        private System.Windows.Forms.TextBox saveImgPrefix;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label avgOffsetObject;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel autoMovingStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timeController;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label calcTimeValue;
        private System.Windows.Forms.Label calcTimeTitle;
        private System.Windows.Forms.Label YoloRunTimeValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox IsWriteStream;
        private System.Windows.Forms.CheckBox drawImage;
        private System.Windows.Forms.CheckBox movingY;
        private System.Windows.Forms.CheckBox movingX;
        private System.Windows.Forms.CheckBox canFire;
        private System.Windows.Forms.CheckBox CanMouseMove;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox mouseCoeff;
        private System.Windows.Forms.CheckBox autoCalibration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox calibrate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox led13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mouseTimeout;
        private System.Windows.Forms.ComboBox mouseClickType;
        private System.Windows.Forms.TextBox gc_commnad;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox covariance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox kalmanError;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox onlyFire;
        private System.Windows.Forms.CheckBox isVectorInverted;
        private System.Windows.Forms.TextBox maxFirePErSecond;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox turnTimeOutValue;
    }
}

