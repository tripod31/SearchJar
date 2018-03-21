using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;			// StringBuilder
using System.IO;
using System.Text.RegularExpressions;

namespace SearchZip
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
    {
		
		// メンバ変数
		private Zip m_oZip;
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

            m_oZip = new Zip();
            // ListViewの列幅を設定
            for (int i = 0;i< this.lv_result.Columns.Count; i++) {
                this.lv_result.Columns[i].Width = Int32.Parse(Properties.Settings.Default.lv_column_width[i]);
            }
        }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void disp(ArrayList arr_file)
        {
            // convert FileRec to ListViewItem
            m_arr_lv.Clear();
            for (int idx = 0; idx < arr_file.Count; idx++)
            {
                Zip.FileRec oFile = (Zip.FileRec)arr_file[idx];
                m_arr_lv.Add(new ListViewItem(new string[]{oFile.zipFileName,oFile.fileName}));
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
            Regex zipname = null;
            try
            {
                zipname = new Regex(text_zip_name.Text);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

			m_oZip.arr_file.Clear();	// リストクリア
            m_oZip.SearchDir(text_dir.Text, zipname, text_file_name.Text);
            disp(m_oZip.arr_file);
        }


		// 絞込みボタン
		private void button_filter_Click(object sender, System.EventArgs e)
		{
            lv_result.Items.Clear();	// リストクリア

            ArrayList arr = m_oZip.FilterFile(text_filter_zip.Text, text_filter_file.Text);
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

			string sFile = oDlg.FileName;
			int iRet = m_oZip.WriteCSV(sFile,text_filter_zip.Text,text_filter_file.Text);
			if ( iRet == -1)
			{
				MessageBox.Show(m_oZip.ErrMsg);
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

			int iRet = m_oZip.ReadCSV(sFile);
			if (iRet == -1)
			{
				MessageBox.Show(m_oZip.ErrMsg);
				return;
			}
            disp(m_oZip.arr_file);
		
		}

		// 設定を保存
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            // ListViewの列幅を保存
            for (int i = 0; i < this.lv_result.Columns.Count; i++)
            {
                Properties.Settings.Default.lv_column_width[i]=this.lv_result.Columns[i].Width.ToString() ;
            }
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
