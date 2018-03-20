using System.Windows.Forms;

namespace SearchJar
{
    partial class Form1
    {

        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_filter_jar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_filter_class = new System.Windows.Forms.TextBox();
            this.button_filter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_class_name = new System.Windows.Forms.TextBox();
            this.text_dir = new System.Windows.Forms.TextBox();
            this.button_search = new System.Windows.Forms.Button();
            this.button_dir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lv_result = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ﾘｽﾄをCSV出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSV読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "JAR file";
            columnHeader1.Width = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 200);
            this.panel1.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.text_filter_jar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.text_filter_class);
            this.groupBox2.Controls.Add(this.button_filter);
            this.groupBox2.Location = new System.Drawing.Point(11, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(666, 85);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "絞込み";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "JAR名";
            // 
            // text_filter_jar
            // 
            this.text_filter_jar.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SearchJar.Properties.Settings.Default, "FILTER_JAR", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.text_filter_jar.Location = new System.Drawing.Point(96, 18);
            this.text_filter_jar.Name = "text_filter_jar";
            this.text_filter_jar.Size = new System.Drawing.Size(430, 19);
            this.text_filter_jar.TabIndex = 17;
            this.text_filter_jar.Text = global::SearchJar.Properties.Settings.Default.FILTER_JAR;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "クラス名";
            // 
            // text_filter_class
            // 
            this.text_filter_class.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SearchJar.Properties.Settings.Default, "FILTER_CLASS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.text_filter_class.Location = new System.Drawing.Point(96, 51);
            this.text_filter_class.Name = "text_filter_class";
            this.text_filter_class.Size = new System.Drawing.Size(430, 19);
            this.text_filter_class.TabIndex = 15;
            this.text_filter_class.Text = global::SearchJar.Properties.Settings.Default.FILTER_CLASS;
            // 
            // button_filter
            // 
            this.button_filter.Location = new System.Drawing.Point(541, 47);
            this.button_filter.Name = "button_filter";
            this.button_filter.Size = new System.Drawing.Size(116, 22);
            this.button_filter.TabIndex = 14;
            this.button_filter.Text = "絞込み";
            this.button_filter.Click += new System.EventHandler(this.button_filter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.text_class_name);
            this.groupBox1.Controls.Add(this.text_dir);
            this.groupBox1.Controls.Add(this.button_search);
            this.groupBox1.Controls.Add(this.button_dir);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 85);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "検索フォルダ";
            // 
            // text_class_name
            // 
            this.text_class_name.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SearchJar.Properties.Settings.Default, "CLASS_NAME", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.text_class_name.Location = new System.Drawing.Point(97, 50);
            this.text_class_name.Name = "text_class_name";
            this.text_class_name.Size = new System.Drawing.Size(429, 19);
            this.text_class_name.TabIndex = 10;
            this.text_class_name.Text = global::SearchJar.Properties.Settings.Default.CLASS_NAME;
            // 
            // text_dir
            // 
            this.text_dir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SearchJar.Properties.Settings.Default, "DIR", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.text_dir.Location = new System.Drawing.Point(97, 19);
            this.text_dir.Name = "text_dir";
            this.text_dir.Size = new System.Drawing.Size(430, 19);
            this.text_dir.TabIndex = 7;
            this.text_dir.Text = global::SearchJar.Properties.Settings.Default.DIR;
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(542, 48);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(117, 21);
            this.button_search.TabIndex = 9;
            this.button_search.Text = "検索";
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // button_dir
            // 
            this.button_dir.Location = new System.Drawing.Point(542, 17);
            this.button_dir.Name = "button_dir";
            this.button_dir.Size = new System.Drawing.Size(117, 22);
            this.button_dir.TabIndex = 8;
            this.button_dir.Text = "フォルダ参照";
            this.button_dir.Click += new System.EventHandler(this.button_dir_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "検索クラス名";
            // 
            // lv_result
            // 
            this.lv_result.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            this.columnHeader2});
            this.lv_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_result.Location = new System.Drawing.Point(0, 200);
            this.lv_result.Margin = new System.Windows.Forms.Padding(4);
            this.lv_result.Name = "lv_result";
            this.lv_result.Size = new System.Drawing.Size(967, 191);
            this.lv_result.TabIndex = 17;
            this.lv_result.UseCompatibleStateImageBehavior = false;
            this.lv_result.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Class";
            this.columnHeader2.Width = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(967, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ﾘｽﾄをCSV出力ToolStripMenuItem,
            this.cSV読込ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.fileToolStripMenuItem.Text = "ﾌｧｲﾙ";
            // 
            // ﾘｽﾄをCSV出力ToolStripMenuItem
            // 
            this.ﾘｽﾄをCSV出力ToolStripMenuItem.Name = "ﾘｽﾄをCSV出力ToolStripMenuItem";
            this.ﾘｽﾄをCSV出力ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.ﾘｽﾄをCSV出力ToolStripMenuItem.Text = "ﾘｽﾄをCSV出力";
            this.ﾘｽﾄをCSV出力ToolStripMenuItem.Click += new System.EventHandler(this.button_output_Click);
            // 
            // cSV読込ToolStripMenuItem
            // 
            this.cSV読込ToolStripMenuItem.Name = "cSV読込ToolStripMenuItem";
            this.cSV読込ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.cSV読込ToolStripMenuItem.Text = "CSV読込";
            this.cSV読込ToolStripMenuItem.Click += new System.EventHandler(this.button_read_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lv_result);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(967, 391);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(967, 419);
            this.toolStripContainer1.TabIndex = 18;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 397);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(967, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = global::SearchJar.Properties.Settings.Default.main_size;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SearchJar.Properties.Settings.Default, "main_loc", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::SearchJar.Properties.Settings.Default, "main_size", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::SearchJar.Properties.Settings.Default.main_loc;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SearchJar";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private Panel panel1;
        private GroupBox groupBox2;
        private Label label3;
        private TextBox text_filter_jar;
        private Label label2;
        private TextBox text_filter_class;
        private Button button_filter;
        private GroupBox groupBox1;
        private Label label4;
        private TextBox text_class_name;
        private TextBox text_dir;
        private Button button_search;
        private Button button_dir;
        private Label label1;
        private ListView lv_result;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripContainer toolStripContainer1;
        private ToolStripMenuItem ﾘｽﾄをCSV出力ToolStripMenuItem;
        private ToolStripMenuItem cSV読込ToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ColumnHeader columnHeader2;

    }
}
