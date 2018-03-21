using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;			// StringBuilder
using System.IO;

namespace SearchJar
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
    {
		
		// メンバ変数
		private Jar m_oJar;
        private ArrayList m_arr_lv=new ArrayList();     // virtualListView用データ配列

        public Form1()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //

            m_oJar = new Jar();
            this.lv_result.Columns[0].Width = Properties.Settings.Default.jar_width;
            this.lv_result.Columns[1].Width = Properties.Settings.Default.class_width;

        }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void disp(ArrayList arr_class)
        {
            // convert ClassRec to ListViewItem
            m_arr_lv.Clear();
            for (int idx = 0; idx < arr_class.Count; idx++)
            {
                Jar.ClassRec oClass = (Jar.ClassRec)arr_class[idx];
                m_arr_lv.Add(new ListViewItem(new string[]{oClass.jarFileName,oClass.className}));
            }

            lv_result.VirtualListSize = m_arr_lv.Count;
            DispCount();
        }
        
        private void DispCount()
        {
            toolStripStatusLabel1.Text = String.Format(
                "{0}件",
                lv_result.Items.Count);

        }

		// イベントハンドラ 
		//////////////////////////////////////////////////////////////////////////////////////////

		// 検索ディレクトリボタン
		private void button_dir_Click(object sender, System.EventArgs e)
		{
			// フォルダ選択ダイアログ表示
            folderBrowserDialog1.ShowDialog(this);

            string sDir = folderBrowserDialog1.SelectedPath;
            if (sDir.Length !=0 )
                text_dir.Text = sDir.ToString();

		}

		// 検索ボタン
		private void button_search_Click(object sender, System.EventArgs e)
		{
			if (text_dir.Text.Length == 0)
				return;

			m_oJar.arr_class.Clear();	// リストクリア

			string sDir =  text_dir.Text;
			string sClassName = text_class_name.Text;

            m_oJar.SearchDir(sDir, sClassName);
            disp(m_oJar.arr_class);
        }


		// 絞込みボタン
		private void button_filter_Click(object sender, System.EventArgs e)
		{
            lv_result.Items.Clear();	// リストクリア

			string sFilter_jar = text_filter_jar.Text;
			string sFilter_class =  text_filter_class.Text;

            ArrayList arr = m_oJar.FilterClass(sFilter_jar, sFilter_class);
            disp(arr);
		}
		
		// ファイルに出力ボタン
		private void button_output_Click(object sender, System.EventArgs e)
		{
			// ファイルダイアログを表示
			SaveFileDialog oDlg = new SaveFileDialog();
			oDlg.DefaultExt="csv";
			oDlg.Filter = "csvファイル(*.csv)|*.csv";

			if(  oDlg.ShowDialog() != DialogResult.OK)
				return;

			string sFilter_jar =  text_filter_jar.Text;
			string sFilter_class =  text_filter_class.Text;

			string sFile = oDlg.FileName;
			int iRet = m_oJar.WriteCSV(sFile,sFilter_jar,sFilter_class);
			if ( iRet == -1)
			{
				MessageBox.Show(m_oJar.ErrMsg);
			} 
			else 
			{
				MessageBox.Show(sFile + "に出力しました");
			}
		}

		// CSV読み込みボタン
		private void button_read_Click(object sender, System.EventArgs e)
		{
			// ファイルダイアログを表示
			OpenFileDialog oDlg = new OpenFileDialog();
			oDlg.DefaultExt="csv";
			oDlg.Filter = "csvファイル(*.csv)|*.csv";

			if(  oDlg.ShowDialog() != DialogResult.OK)
				return;

			string sFile = oDlg.FileName;

			int iRet = m_oJar.ReadCSV(sFile);
			if (iRet == -1)
			{
				MessageBox.Show(m_oJar.ErrMsg);
				return;
			}
            disp(m_oJar.arr_class);
		
		}

		// 設定を保存
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            Properties.Settings.Default.jar_width= this.lv_result.Columns[0].Width;
            Properties.Settings.Default.class_width = this.lv_result.Columns[1].Width;
            Properties.Settings.Default.Save();	    // アプリ設定ファイル保存

		}

        private void lv_result_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = (ListViewItem)this.m_arr_lv[e.ItemIndex];
        }

        private void lv_result_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            //lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);  // 効かない？
        }
    }
}
