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
            this.isVectorInverted = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.covariance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.kalmanError = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.led13 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mouseTimeout = new System.Windows.Forms.TextBox();
            this.mouseClickType = new System.Windows.Forms.ComboBox();
            this.gc_commnad = new System.Windows.Forms.TextBox();
            this.onlyFire = new System.Windows.Forms.CheckBox();
            this.maxFirePErSecond = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mouseCoeff = new System.Windows.Forms.TextBox();
            this.autoCalibration = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.calibrate = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.turnTimeOutValue = new System.Windows.Forms.TextBox();
            this.CanMouseMove = new System.Windows.Forms.CheckBox();
            this.canFire = new System.Windows.Forms.CheckBox();
            this.createDetectedImg = new System.Windows.Forms.CheckBox();
            this.saveImgPrefix = new System.Windows.Forms.TextBox();
            this.movingX = new System.Windows.Forms.CheckBox();
            this.movingY = new System.Windows.Forms.CheckBox();
            this.drawImage = new System.Windows.Forms.CheckBox();
            this.IsWriteStream = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(800, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(352, 123);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // avgOffsetObject
            // 
            this.avgOffsetObject.AutoSize = true;
            this.avgOffsetObject.Location = new System.Drawing.Point(206, 29);
            this.avgOffsetObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.avgOffsetObject.Name = "avgOffsetObject";
            this.avgOffsetObject.Size = new System.Drawing.Size(16, 17);
            this.avgOffsetObject.TabIndex = 12;
            this.avgOffsetObject.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 29);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(203, 17);
            this.label11.TabIndex = 11;
            this.label11.Text = "Среднее смещение объекта: ";
            // 
            // autoMovingStatus
            // 
            this.autoMovingStatus.BackColor = System.Drawing.Color.Red;
            this.autoMovingStatus.Location = new System.Drawing.Point(192, 99);
            this.autoMovingStatus.Name = "autoMovingStatus";
            this.autoMovingStatus.Size = new System.Drawing.Size(61, 14);
            this.autoMovingStatus.TabIndex = 4;
            this.autoMovingStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.autoMovingStatus_Paint);
            this.autoMovingStatus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.autoMovingStatus_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Автоматический контроль:";
            // 
            // timeController
            // 
            this.timeController.AutoSize = true;
            this.timeController.Location = new System.Drawing.Point(206, 79);
            this.timeController.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeController.Name = "timeController";
            this.timeController.Size = new System.Drawing.Size(38, 17);
            this.timeController.TabIndex = 9;
            this.timeController.Text = "0 ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Время отклика контроллера: ";
            // 
            // calcTimeValue
            // 
            this.calcTimeValue.AutoSize = true;
            this.calcTimeValue.Location = new System.Drawing.Point(120, 62);
            this.calcTimeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.calcTimeValue.Name = "calcTimeValue";
            this.calcTimeValue.Size = new System.Drawing.Size(38, 17);
            this.calcTimeValue.TabIndex = 7;
            this.calcTimeValue.Text = "0 ms";
            // 
            // calcTimeTitle
            // 
            this.calcTimeTitle.AutoSize = true;
            this.calcTimeTitle.Location = new System.Drawing.Point(8, 62);
            this.calcTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.calcTimeTitle.Name = "calcTimeTitle";
            this.calcTimeTitle.Size = new System.Drawing.Size(123, 17);
            this.calcTimeTitle.TabIndex = 6;
            this.calcTimeTitle.Text = "Время расчётов: ";
            // 
            // YoloRunTimeValue
            // 
            this.YoloRunTimeValue.AutoSize = true;
            this.YoloRunTimeValue.Location = new System.Drawing.Point(120, 46);
            this.YoloRunTimeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.YoloRunTimeValue.Name = "YoloRunTimeValue";
            this.YoloRunTimeValue.Size = new System.Drawing.Size(38, 17);
            this.YoloRunTimeValue.TabIndex = 5;
            this.YoloRunTimeValue.Text = "0 ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Время детекта: ";
            // 
            // isVectorInverted
            // 
            this.isVectorInverted.AutoSize = true;
            this.isVectorInverted.Location = new System.Drawing.Point(480, 15);
            this.isVectorInverted.Name = "isVectorInverted";
            this.isVectorInverted.Size = new System.Drawing.Size(268, 21);
            this.isVectorInverted.TabIndex = 4;
            this.isVectorInverted.Text = "Инверсия вектора движения мышки";
            this.isVectorInverted.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.covariance);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.kalmanError);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(160, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 121);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Фильтр Калмана";
            // 
            // covariance
            // 
            this.covariance.Location = new System.Drawing.Point(173, 60);
            this.covariance.Name = "covariance";
            this.covariance.Size = new System.Drawing.Size(67, 22);
            this.covariance.TabIndex = 3;
            this.covariance.Text = "15";
            this.covariance.TextChanged += new System.EventHandler(this.covariance_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "Ковариант:";
            // 
            // kalmanError
            // 
            this.kalmanError.Location = new System.Drawing.Point(173, 32);
            this.kalmanError.Name = "kalmanError";
            this.kalmanError.Size = new System.Drawing.Size(67, 22);
            this.kalmanError.TabIndex = 1;
            this.kalmanError.Text = "1";
            this.kalmanError.TextChanged += new System.EventHandler(this.kalmanError_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ошибка измерения(R):";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Тест";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
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
            this.groupBox3.Location = new System.Drawing.Point(1158, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(160, 242);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Тест gameController";
            // 
            // led13
            // 
            this.led13.AutoSize = true;
            this.led13.Checked = true;
            this.led13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.led13.Location = new System.Drawing.Point(6, 173);
            this.led13.Name = "led13";
            this.led13.Size = new System.Drawing.Size(130, 21);
            this.led13.TabIndex = 15;
            this.led13.Text = "Включить диод";
            this.led13.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Задержка клика";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Клик";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Координаты";
            // 
            // mouseTimeout
            // 
            this.mouseTimeout.Location = new System.Drawing.Point(6, 145);
            this.mouseTimeout.Name = "mouseTimeout";
            this.mouseTimeout.Size = new System.Drawing.Size(148, 22);
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
            this.mouseClickType.Location = new System.Drawing.Point(6, 97);
            this.mouseClickType.Name = "mouseClickType";
            this.mouseClickType.Size = new System.Drawing.Size(148, 24);
            this.mouseClickType.TabIndex = 8;
            this.mouseClickType.SelectedIndexChanged += new System.EventHandler(this.mouseClickType_SelectedIndexChanged);
            // 
            // gc_commnad
            // 
            this.gc_commnad.Location = new System.Drawing.Point(6, 45);
            this.gc_commnad.Name = "gc_commnad";
            this.gc_commnad.Size = new System.Drawing.Size(148, 22);
            this.gc_commnad.TabIndex = 7;
            // 
            // onlyFire
            // 
            this.onlyFire.AutoSize = true;
            this.onlyFire.Location = new System.Drawing.Point(480, 47);
            this.onlyFire.Name = "onlyFire";
            this.onlyFire.Size = new System.Drawing.Size(288, 21);
            this.onlyFire.TabIndex = 8;
            this.onlyFire.Text = "стрелять есть объект в районе центра";
            this.onlyFire.UseVisualStyleBackColor = true;
            // 
            // maxFirePErSecond
            // 
            this.maxFirePErSecond.Location = new System.Drawing.Point(646, 74);
            this.maxFirePErSecond.Name = "maxFirePErSecond";
            this.maxFirePErSecond.Size = new System.Drawing.Size(67, 22);
            this.maxFirePErSecond.TabIndex = 9;
            this.maxFirePErSecond.Text = "6";
            this.maxFirePErSecond.TextChanged += new System.EventHandler(this.maxFirePErSecond_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(480, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Макс. выстрелов в сек.";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mouseCoeff);
            this.groupBox4.Controls.Add(this.autoCalibration);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.calibrate);
            this.groupBox4.Location = new System.Drawing.Point(1158, 263);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(160, 108);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Калибровка мышки";
            // 
            // mouseCoeff
            // 
            this.mouseCoeff.Location = new System.Drawing.Point(84, 42);
            this.mouseCoeff.Name = "mouseCoeff";
            this.mouseCoeff.Size = new System.Drawing.Size(67, 22);
            this.mouseCoeff.TabIndex = 11;
            this.mouseCoeff.Text = "0,1231";
            this.mouseCoeff.TextChanged += new System.EventHandler(this.mouseCoeff_TextChanged);
            // 
            // autoCalibration
            // 
            this.autoCalibration.AutoSize = true;
            this.autoCalibration.Location = new System.Drawing.Point(9, 75);
            this.autoCalibration.Name = "autoCalibration";
            this.autoCalibration.Size = new System.Drawing.Size(142, 21);
            this.autoCalibration.TabIndex = 13;
            this.autoCalibration.Text = "Авто калибровка";
            this.autoCalibration.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "Поправка: ";
            // 
            // calibrate
            // 
            this.calibrate.AutoSize = true;
            this.calibrate.Location = new System.Drawing.Point(9, 21);
            this.calibrate.Name = "calibrate";
            this.calibrate.Size = new System.Drawing.Size(116, 21);
            this.calibrate.TabIndex = 9;
            this.calibrate.Text = "Калибровать";
            this.calibrate.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(480, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(167, 17);
            this.label12.TabIndex = 13;
            this.label12.Text = "Задержка авт. очереди:";
            // 
            // turnTimeOutValue
            // 
            this.turnTimeOutValue.Location = new System.Drawing.Point(646, 96);
            this.turnTimeOutValue.Name = "turnTimeOutValue";
            this.turnTimeOutValue.Size = new System.Drawing.Size(67, 22);
            this.turnTimeOutValue.TabIndex = 10;
            this.turnTimeOutValue.Text = "0";
            this.turnTimeOutValue.TextChanged += new System.EventHandler(this.turnTimeOutValue_TextChanged);
            // 
            // CanMouseMove
            // 
            this.CanMouseMove.AutoSize = true;
            this.CanMouseMove.Checked = true;
            this.CanMouseMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CanMouseMove.Location = new System.Drawing.Point(1158, 377);
            this.CanMouseMove.Name = "CanMouseMove";
            this.CanMouseMove.Size = new System.Drawing.Size(162, 21);
            this.CanMouseMove.TabIndex = 14;
            this.CanMouseMove.Text = "Перемещать мышку";
            this.CanMouseMove.UseVisualStyleBackColor = true;
            // 
            // canFire
            // 
            this.canFire.AutoSize = true;
            this.canFire.Checked = true;
            this.canFire.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canFire.Location = new System.Drawing.Point(1158, 404);
            this.canFire.Name = "canFire";
            this.canFire.Size = new System.Drawing.Size(92, 21);
            this.canFire.TabIndex = 15;
            this.canFire.Text = "Стрелять";
            this.canFire.UseVisualStyleBackColor = true;
            // 
            // createDetectedImg
            // 
            this.createDetectedImg.AutoSize = true;
            this.createDetectedImg.Location = new System.Drawing.Point(5, 77);
            this.createDetectedImg.Name = "createDetectedImg";
            this.createDetectedImg.Size = new System.Drawing.Size(149, 21);
            this.createDetectedImg.TabIndex = 16;
            this.createDetectedImg.Text = "Создать картинки";
            this.createDetectedImg.UseVisualStyleBackColor = true;
            this.createDetectedImg.CheckedChanged += new System.EventHandler(this.createDetectedImg_CheckedChanged);
            // 
            // saveImgPrefix
            // 
            this.saveImgPrefix.Location = new System.Drawing.Point(5, 99);
            this.saveImgPrefix.Name = "saveImgPrefix";
            this.saveImgPrefix.Size = new System.Drawing.Size(149, 22);
            this.saveImgPrefix.TabIndex = 4;
            this.saveImgPrefix.Text = "d";
            // 
            // movingX
            // 
            this.movingX.AutoSize = true;
            this.movingX.Checked = true;
            this.movingX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.movingX.Location = new System.Drawing.Point(1158, 431);
            this.movingX.Name = "movingX";
            this.movingX.Size = new System.Drawing.Size(116, 21);
            this.movingX.TabIndex = 17;
            this.movingX.Text = "Двигать по X";
            this.movingX.UseVisualStyleBackColor = true;
            // 
            // movingY
            // 
            this.movingY.AutoSize = true;
            this.movingY.Checked = true;
            this.movingY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.movingY.Location = new System.Drawing.Point(1158, 458);
            this.movingY.Name = "movingY";
            this.movingY.Size = new System.Drawing.Size(120, 21);
            this.movingY.TabIndex = 18;
            this.movingY.Text = "Двигать  по Y";
            this.movingY.UseVisualStyleBackColor = true;
            // 
            // drawImage
            // 
            this.drawImage.AutoSize = true;
            this.drawImage.Checked = true;
            this.drawImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawImage.Location = new System.Drawing.Point(1159, 485);
            this.drawImage.Name = "drawImage";
            this.drawImage.Size = new System.Drawing.Size(166, 21);
            this.drawImage.TabIndex = 19;
            this.drawImage.Text = "Отрисовка картинки";
            this.drawImage.UseVisualStyleBackColor = true;
            // 
            // IsWriteStream
            // 
            this.IsWriteStream.AutoSize = true;
            this.IsWriteStream.Location = new System.Drawing.Point(1159, 512);
            this.IsWriteStream.Name = "IsWriteStream";
            this.IsWriteStream.Size = new System.Drawing.Size(109, 21);
            this.IsWriteStream.TabIndex = 20;
            this.IsWriteStream.Text = "Записывать";
            this.IsWriteStream.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 762);
            this.Controls.Add(this.IsWriteStream);
            this.Controls.Add(this.drawImage);
            this.Controls.Add(this.movingY);
            this.Controls.Add(this.movingX);
            this.Controls.Add(this.saveImgPrefix);
            this.Controls.Add(this.createDetectedImg);
            this.Controls.Add(this.canFire);
            this.Controls.Add(this.CanMouseMove);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.turnTimeOutValue);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.maxFirePErSecond);
            this.Controls.Add(this.onlyFire);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.isVectorInverted);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox kalmanError;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox gc_commnad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mouseTimeout;
        private System.Windows.Forms.ComboBox mouseClickType;
        private System.Windows.Forms.CheckBox onlyFire;
        private System.Windows.Forms.TextBox maxFirePErSecond;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox calibrate;
        private System.Windows.Forms.CheckBox autoCalibration;
        private System.Windows.Forms.Label avgOffsetObject;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox covariance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox turnTimeOutValue;
        private System.Windows.Forms.TextBox mouseCoeff;
        private System.Windows.Forms.CheckBox CanMouseMove;
        private System.Windows.Forms.CheckBox canFire;
        private System.Windows.Forms.CheckBox led13;
        private System.Windows.Forms.CheckBox createDetectedImg;
        private System.Windows.Forms.TextBox saveImgPrefix;
        private System.Windows.Forms.CheckBox movingX;
        private System.Windows.Forms.CheckBox movingY;
        private System.Windows.Forms.CheckBox drawImage;
        private System.Windows.Forms.CheckBox IsWriteStream;
    }
}

