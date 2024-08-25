namespace Tanfolyam_nyilvántartás
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            button1 = new Button();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button2 = new Button();
            tanarbox = new ComboBox();
            button3 = new Button();
            KurzusList = new ListBox();
            Diakok_Button = new Button();
            DiakokBox = new ComboBox();
            KurzusButton = new Button();
            KurzusBox = new ComboBox();
            FelvDiakButton = new Button();
            AktivalasButton = new Button();
            LezarasButton = new Button();
            KuAdatButton = new Button();
            panel1 = new Panel();
            AModButton = new Button();
            button5 = new Button();
            LetszamButton = new Button();
            BevButton = new Button();
            BefizetesButton = new Button();
            LetszamDiag = new System.Windows.Forms.DataVisualization.Charting.Chart();
            IntervalKezdet = new DateTimePicker();
            IntervalVege = new DateTimePicker();
            LetszMutatoButton = new Button();
            KezdetLabel = new Label();
            VegeLabel = new Label();
            BevetelDiag = new System.Windows.Forms.DataVisualization.Charting.Chart();
            BevetMutatButton = new Button();
            DiagselectBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            KatBox = new ComboBox();
            ModositButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LetszamDiag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BevetelDiag).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(44, 64, 84);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(526, 63);
            button1.Name = "button1";
            button1.Size = new Size(75, 24);
            button1.TabIndex = 0;
            button1.Text = "Bevitel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.Lavender;
            comboBox1.Cursor = Cursors.Hand;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(219, 35);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(109, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Lavender;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = SystemColors.Desktop;
            textBox1.Location = new Point(219, 64);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 23);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Lavender;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(219, 93);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Számlázási Név";
            textBox2.Size = new Size(301, 23);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.Lavender;
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(219, 122);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Számlázási Cím";
            textBox3.Size = new Size(301, 23);
            textBox3.TabIndex = 4;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(34, 44, 74);
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = SystemColors.ControlLightLight;
            button2.Location = new Point(13, 100);
            button2.Name = "button2";
            button2.Size = new Size(109, 44);
            button2.TabIndex = 5;
            button2.Text = "Adat felvétel";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // tanarbox
            // 
            tanarbox.BackColor = Color.Lavender;
            tanarbox.Cursor = Cursors.Hand;
            tanarbox.DropDownStyle = ComboBoxStyle.DropDownList;
            tanarbox.FlatStyle = FlatStyle.Popup;
            tanarbox.FormattingEnabled = true;
            tanarbox.Location = new Point(219, 151);
            tanarbox.Name = "tanarbox";
            tanarbox.Size = new Size(301, 23);
            tanarbox.TabIndex = 6;
            tanarbox.SelectedIndexChanged += tanarbox_SelectedIndexChanged;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(34, 44, 74);
            button3.Cursor = Cursors.Hand;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = SystemColors.ControlLightLight;
            button3.Location = new Point(15, 198);
            button3.Name = "button3";
            button3.Size = new Size(109, 44);
            button3.TabIndex = 7;
            button3.Text = "Tanáraink";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // KurzusList
            // 
            KurzusList.AllowDrop = true;
            KurzusList.BackColor = Color.Lavender;
            KurzusList.BorderStyle = BorderStyle.None;
            KurzusList.ForeColor = SystemColors.MenuText;
            KurzusList.FormattingEnabled = true;
            KurzusList.ItemHeight = 15;
            KurzusList.Location = new Point(219, 93);
            KurzusList.Name = "KurzusList";
            KurzusList.Size = new Size(301, 15);
            KurzusList.TabIndex = 8;
            // 
            // Diakok_Button
            // 
            Diakok_Button.BackColor = Color.FromArgb(34, 44, 74);
            Diakok_Button.Cursor = Cursors.Hand;
            Diakok_Button.FlatStyle = FlatStyle.Popup;
            Diakok_Button.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Diakok_Button.ForeColor = SystemColors.ControlLightLight;
            Diakok_Button.Location = new Point(15, 248);
            Diakok_Button.Name = "Diakok_Button";
            Diakok_Button.Size = new Size(109, 44);
            Diakok_Button.TabIndex = 9;
            Diakok_Button.Text = "Diákok";
            Diakok_Button.UseVisualStyleBackColor = false;
            Diakok_Button.Click += Diakok_Button_Click;
            // 
            // DiakokBox
            // 
            DiakokBox.BackColor = Color.Lavender;
            DiakokBox.Cursor = Cursors.Hand;
            DiakokBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DiakokBox.FlatStyle = FlatStyle.Popup;
            DiakokBox.FormattingEnabled = true;
            DiakokBox.Location = new Point(219, 180);
            DiakokBox.Name = "DiakokBox";
            DiakokBox.Size = new Size(301, 23);
            DiakokBox.TabIndex = 10;
            DiakokBox.Visible = false;
            DiakokBox.SelectedIndexChanged += DiakokBox_SelectedIndexChanged;
            // 
            // KurzusButton
            // 
            KurzusButton.BackColor = Color.FromArgb(34, 44, 74);
            KurzusButton.Cursor = Cursors.Hand;
            KurzusButton.FlatStyle = FlatStyle.Popup;
            KurzusButton.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            KurzusButton.ForeColor = SystemColors.ControlLightLight;
            KurzusButton.Location = new Point(15, 298);
            KurzusButton.Name = "KurzusButton";
            KurzusButton.Size = new Size(109, 44);
            KurzusButton.TabIndex = 11;
            KurzusButton.Text = "Kurzus Beállításai";
            KurzusButton.UseVisualStyleBackColor = false;
            KurzusButton.Click += KurzusButton_Click;
            // 
            // KurzusBox
            // 
            KurzusBox.BackColor = Color.Lavender;
            KurzusBox.Cursor = Cursors.Hand;
            KurzusBox.DropDownStyle = ComboBoxStyle.DropDownList;
            KurzusBox.FlatStyle = FlatStyle.Popup;
            KurzusBox.FormattingEnabled = true;
            KurzusBox.Location = new Point(219, 211);
            KurzusBox.Name = "KurzusBox";
            KurzusBox.Size = new Size(301, 23);
            KurzusBox.TabIndex = 12;
            // 
            // FelvDiakButton
            // 
            FelvDiakButton.BackColor = Color.FromArgb(44, 64, 84);
            FelvDiakButton.Cursor = Cursors.Hand;
            FelvDiakButton.FlatStyle = FlatStyle.Popup;
            FelvDiakButton.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            FelvDiakButton.ForeColor = Color.Transparent;
            FelvDiakButton.Location = new Point(526, 93);
            FelvDiakButton.Name = "FelvDiakButton";
            FelvDiakButton.Size = new Size(75, 25);
            FelvDiakButton.TabIndex = 13;
            FelvDiakButton.Text = "Felvesz";
            FelvDiakButton.UseVisualStyleBackColor = false;
            FelvDiakButton.Click += FelvDiakButton_Click;
            // 
            // AktivalasButton
            // 
            AktivalasButton.BackColor = Color.FromArgb(44, 64, 84);
            AktivalasButton.Cursor = Cursors.Hand;
            AktivalasButton.FlatStyle = FlatStyle.Popup;
            AktivalasButton.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            AktivalasButton.ForeColor = Color.Transparent;
            AktivalasButton.Location = new Point(219, 122);
            AktivalasButton.Name = "AktivalasButton";
            AktivalasButton.Size = new Size(95, 23);
            AktivalasButton.TabIndex = 14;
            AktivalasButton.Text = "Aktiválás";
            AktivalasButton.UseVisualStyleBackColor = false;
            AktivalasButton.Click += AktivalasButton_Click;
            // 
            // LezarasButton
            // 
            LezarasButton.BackColor = Color.FromArgb(44, 64, 84);
            LezarasButton.Cursor = Cursors.Hand;
            LezarasButton.FlatStyle = FlatStyle.Popup;
            LezarasButton.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LezarasButton.ForeColor = SystemColors.ButtonHighlight;
            LezarasButton.Location = new Point(320, 122);
            LezarasButton.Name = "LezarasButton";
            LezarasButton.Size = new Size(96, 23);
            LezarasButton.TabIndex = 15;
            LezarasButton.Text = "Lezárás";
            LezarasButton.UseVisualStyleBackColor = false;
            LezarasButton.Click += LezarasButton_Click;
            // 
            // KuAdatButton
            // 
            KuAdatButton.BackColor = Color.FromArgb(44, 64, 84);
            KuAdatButton.Cursor = Cursors.Hand;
            KuAdatButton.FlatStyle = FlatStyle.Popup;
            KuAdatButton.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            KuAdatButton.ForeColor = Color.Transparent;
            KuAdatButton.Location = new Point(422, 123);
            KuAdatButton.Name = "KuAdatButton";
            KuAdatButton.Size = new Size(98, 23);
            KuAdatButton.TabIndex = 16;
            KuAdatButton.Text = "Kurzus adatai";
            KuAdatButton.UseVisualStyleBackColor = false;
            KuAdatButton.Click += KuAdatButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(31, 31, 44);
            panel1.Controls.Add(AModButton);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(LetszamButton);
            panel1.Controls.Add(BevButton);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(Diakok_Button);
            panel1.Controls.Add(KurzusButton);
            panel1.Location = new Point(-1, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(139, 12000);
            panel1.TabIndex = 17;
            // 
            // AModButton
            // 
            AModButton.BackColor = Color.FromArgb(34, 44, 74);
            AModButton.FlatStyle = FlatStyle.Popup;
            AModButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            AModButton.ForeColor = SystemColors.ButtonHighlight;
            AModButton.Location = new Point(15, 150);
            AModButton.Name = "AModButton";
            AModButton.Size = new Size(107, 42);
            AModButton.TabIndex = 15;
            AModButton.Text = "Adat módosítás";
            AModButton.UseVisualStyleBackColor = false;
            AModButton.Click += AModButton_Click;
            // 
            // button5
            // 
            button5.AccessibleRole = AccessibleRole.None;
            button5.BackgroundImage = (Image)resources.GetObject("button5.BackgroundImage");
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Enabled = false;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(8, 4);
            button5.Name = "button5";
            button5.Size = new Size(123, 87);
            button5.TabIndex = 14;
            button5.UseVisualStyleBackColor = true;
            // 
            // LetszamButton
            // 
            LetszamButton.BackColor = Color.FromArgb(34, 44, 74);
            LetszamButton.FlatStyle = FlatStyle.Popup;
            LetszamButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LetszamButton.ForeColor = SystemColors.ButtonHighlight;
            LetszamButton.Location = new Point(15, 348);
            LetszamButton.Name = "LetszamButton";
            LetszamButton.Size = new Size(109, 44);
            LetszamButton.TabIndex = 13;
            LetszamButton.Text = "Létszám";
            LetszamButton.UseVisualStyleBackColor = false;
            LetszamButton.Click += LetszamButton_Click;
            // 
            // BevButton
            // 
            BevButton.BackColor = Color.FromArgb(34, 44, 74);
            BevButton.FlatStyle = FlatStyle.Popup;
            BevButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BevButton.ForeColor = SystemColors.ButtonHighlight;
            BevButton.Location = new Point(15, 398);
            BevButton.Name = "BevButton";
            BevButton.Size = new Size(109, 44);
            BevButton.TabIndex = 12;
            BevButton.Text = "Bevételek";
            BevButton.UseVisualStyleBackColor = false;
            BevButton.Click += BevButton_Click;
            // 
            // BefizetesButton
            // 
            BefizetesButton.BackColor = Color.FromArgb(44, 64, 84);
            BefizetesButton.Cursor = Cursors.Hand;
            BefizetesButton.FlatStyle = FlatStyle.Popup;
            BefizetesButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BefizetesButton.ForeColor = SystemColors.ControlLightLight;
            BefizetesButton.Location = new Point(526, 74);
            BefizetesButton.Name = "BefizetesButton";
            BefizetesButton.Size = new Size(75, 34);
            BefizetesButton.TabIndex = 18;
            BefizetesButton.Text = "Befizetés";
            BefizetesButton.UseVisualStyleBackColor = false;
            BefizetesButton.Click += BefizetesButton_Click;
            // 
            // LetszamDiag
            // 
            LetszamDiag.BackColor = Color.Lavender;
            LetszamDiag.BorderlineColor = Color.Black;
            chartArea1.Name = "ChartArea1";
            LetszamDiag.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            LetszamDiag.Legends.Add(legend1);
            LetszamDiag.Location = new Point(152, 153);
            LetszamDiag.Name = "LetszamDiag";
            LetszamDiag.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            LetszamDiag.Series.Add(series1);
            LetszamDiag.Size = new Size(449, 305);
            LetszamDiag.TabIndex = 19;
            LetszamDiag.Text = "Létszám";
            // 
            // IntervalKezdet
            // 
            IntervalKezdet.CalendarMonthBackground = Color.Lavender;
            IntervalKezdet.Location = new Point(267, 92);
            IntervalKezdet.Name = "IntervalKezdet";
            IntervalKezdet.Size = new Size(220, 23);
            IntervalKezdet.TabIndex = 20;
            // 
            // IntervalVege
            // 
            IntervalVege.CalendarMonthBackground = Color.Lavender;
            IntervalVege.CalendarTitleBackColor = Color.Indigo;
            IntervalVege.Location = new Point(267, 124);
            IntervalVege.Name = "IntervalVege";
            IntervalVege.Size = new Size(220, 23);
            IntervalVege.TabIndex = 21;
            // 
            // LetszMutatoButton
            // 
            LetszMutatoButton.BackColor = Color.FromArgb(44, 64, 84);
            LetszMutatoButton.FlatStyle = FlatStyle.Popup;
            LetszMutatoButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LetszMutatoButton.ForeColor = SystemColors.ControlLightLight;
            LetszMutatoButton.Location = new Point(526, 104);
            LetszMutatoButton.Name = "LetszMutatoButton";
            LetszMutatoButton.Size = new Size(75, 33);
            LetszMutatoButton.TabIndex = 22;
            LetszMutatoButton.Text = "Szűrés";
            LetszMutatoButton.UseVisualStyleBackColor = false;
            LetszMutatoButton.Click += LetszMutatoButton_Click;
            // 
            // KezdetLabel
            // 
            KezdetLabel.AutoSize = true;
            KezdetLabel.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            KezdetLabel.ForeColor = SystemColors.ButtonHighlight;
            KezdetLabel.Location = new Point(219, 95);
            KezdetLabel.Name = "KezdetLabel";
            KezdetLabel.Size = new Size(45, 16);
            KezdetLabel.TabIndex = 23;
            KezdetLabel.Text = "Kezdet";
            // 
            // VegeLabel
            // 
            VegeLabel.AutoSize = true;
            VegeLabel.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            VegeLabel.ForeColor = SystemColors.ButtonHighlight;
            VegeLabel.Location = new Point(219, 124);
            VegeLabel.Name = "VegeLabel";
            VegeLabel.Size = new Size(36, 16);
            VegeLabel.TabIndex = 24;
            VegeLabel.Text = "Vége";
            // 
            // BevetelDiag
            // 
            BevetelDiag.BackColor = Color.Lavender;
            chartArea2.Name = "ChartArea1";
            BevetelDiag.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            BevetelDiag.Legends.Add(legend2);
            BevetelDiag.Location = new Point(152, 153);
            BevetelDiag.Name = "BevetelDiag";
            BevetelDiag.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            BevetelDiag.Series.Add(series2);
            BevetelDiag.Size = new Size(449, 305);
            BevetelDiag.TabIndex = 25;
            BevetelDiag.Text = "Bevétel";
            // 
            // BevetMutatButton
            // 
            BevetMutatButton.BackColor = Color.FromArgb(44, 64, 84);
            BevetMutatButton.FlatStyle = FlatStyle.Popup;
            BevetMutatButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BevetMutatButton.ForeColor = SystemColors.ControlLightLight;
            BevetMutatButton.Location = new Point(493, 92);
            BevetMutatButton.Name = "BevetMutatButton";
            BevetMutatButton.Size = new Size(75, 38);
            BevetMutatButton.TabIndex = 26;
            BevetMutatButton.Text = "Szűrés";
            BevetMutatButton.UseVisualStyleBackColor = false;
            BevetMutatButton.Click += BevetMutatButton_Click;
            // 
            // DiagselectBox
            // 
            DiagselectBox.BackColor = Color.Lavender;
            DiagselectBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DiagselectBox.FlatStyle = FlatStyle.Flat;
            DiagselectBox.FormattingEnabled = true;
            DiagselectBox.Location = new Point(219, 63);
            DiagselectBox.Name = "DiagselectBox";
            DiagselectBox.Size = new Size(109, 23);
            DiagselectBox.TabIndex = 27;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(152, 37);
            label1.Name = "label1";
            label1.Size = new Size(61, 16);
            label1.TabIndex = 28;
            label1.Text = "Kategória";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(152, 67);
            label2.Name = "label2";
            label2.Size = new Size(41, 16);
            label2.TabIndex = 29;
            label2.Text = "Tanár";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(182, 95);
            label3.Name = "label3";
            label3.Size = new Size(31, 16);
            label3.TabIndex = 30;
            label3.Text = "Diák";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(172, 158);
            label4.Name = "label4";
            label4.Size = new Size(41, 16);
            label4.TabIndex = 31;
            label4.Text = "Tanár";
            // 
            // KatBox
            // 
            KatBox.BackColor = Color.Lavender;
            KatBox.DropDownStyle = ComboBoxStyle.DropDownList;
            KatBox.FlatStyle = FlatStyle.Flat;
            KatBox.FormattingEnabled = true;
            KatBox.Location = new Point(219, 34);
            KatBox.Name = "KatBox";
            KatBox.Size = new Size(109, 23);
            KatBox.TabIndex = 32;
            KatBox.SelectedIndexChanged += KatBox_SelectedIndexChanged;
            // 
            // ModositButton
            // 
            ModositButton.BackColor = Color.FromArgb(44, 64, 84);
            ModositButton.FlatStyle = FlatStyle.Popup;
            ModositButton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            ModositButton.ForeColor = SystemColors.ButtonFace;
            ModositButton.Location = new Point(527, 64);
            ModositButton.Name = "ModositButton";
            ModositButton.Size = new Size(75, 34);
            ModositButton.TabIndex = 33;
            ModositButton.Text = "Módosít";
            ModositButton.UseVisualStyleBackColor = false;
            ModositButton.Click += ModositButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 51, 64);
            ClientSize = new Size(679, 476);
            Controls.Add(ModositButton);
            Controls.Add(KatBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DiagselectBox);
            Controls.Add(BevetMutatButton);
            Controls.Add(BevetelDiag);
            Controls.Add(VegeLabel);
            Controls.Add(KezdetLabel);
            Controls.Add(LetszMutatoButton);
            Controls.Add(IntervalVege);
            Controls.Add(IntervalKezdet);
            Controls.Add(LetszamDiag);
            Controls.Add(BefizetesButton);
            Controls.Add(panel1);
            Controls.Add(comboBox1);
            Controls.Add(KuAdatButton);
            Controls.Add(LezarasButton);
            Controls.Add(AktivalasButton);
            Controls.Add(FelvDiakButton);
            Controls.Add(KurzusBox);
            Controls.Add(DiakokBox);
            Controls.Add(KurzusList);
            Controls.Add(tanarbox);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Coursi";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LetszamDiag).EndInit();
            ((System.ComponentModel.ISupportInitialize)BevetelDiag).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button2;
        private ComboBox tanarbox;
        private Button button3;
        private ListBox KurzusList;
        private Button Diakok_Button;
        private ComboBox DiakokBox;
        private Button KurzusButton;
        private ComboBox KurzusBox;
        private Button FelvDiakButton;
        private Button AktivalasButton;
        private Button LezarasButton;
        private Button KuAdatButton;
        private Panel panel1;
        private Button BevButton;
        private Button LetszamButton;
        private Button button5;
        private Button BefizetesButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart LetszamDiag;
        private DateTimePicker IntervalKezdet;
        private DateTimePicker IntervalVege;
        private Button LetszMutatoButton;
        private Label KezdetLabel;
        private Label VegeLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart BevetelDiag;
        private Button BevetMutatButton;
        private ComboBox DiagselectBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button AModButton;
        private ComboBox KatBox;
        private Button ModositButton;
    }
}
