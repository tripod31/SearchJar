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

        }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
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

		// 実行ボタン
		private void button_go_Click(object sender, System.EventArgs e)
		{
			if (text_dir.Text.Length == 0)
				return;

            Cursor.Current = Cursors.WaitCursor;
			m_oJar.arr_class.Clear();	// リストクリア

			string sDir =  text_dir.Text;
			string sClassName = text_class_name.Text;

			// ディレクトリで*.jarファイルを検索
			ArrayList arr_jar_files = new ArrayList();
			Util.GetFileNames(sDir,"*.jar",ref arr_jar_files);

			// jarファイル毎にクラス検索		
			for ( int i = 0;i<arr_jar_files.Count;i++) 
			{
				string sJarFileName = arr_jar_files[i].ToString();
				
				int iRet = m_oJar.SearchClass(sJarFileName,sClassName);
				if (iRet == -1)
				{
					MessageBox.Show(m_oJar.ErrMsg);
					break;
				}
			}

			lv_result.Items.Clear();		// 表示リストクリア

			// リスト表示
			for (int i=0;i<m_oJar.arr_class.Count;i++)
			{
				Jar.ClassRec oRec = (Jar.ClassRec)m_oJar.arr_class[i];
                ListViewItem item = lv_result.Items.Add(oRec.sJarFileName);
				item.SubItems.Add(oRec.sClassName);
			}
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            Cursor.Current = Cursors.Default;
            DispCount();
        }

        private void DispCount()
        {
            toolStripStatusLabel1.Text = String.Format(
                "{0}件",
                lv_result.Items.Count);

		}

		// 表示絞込みボタン
		private void button_filter_Click(object sender, System.EventArgs e)
		{
            Cursor.Current = Cursors.WaitCursor;
            lv_result.Items.Clear();	// リストクリア

			string sFilter_jar = text_filter_jar.Text;
			string sFilter_class =  text_filter_class.Text;

			// リスト表示
			for (int i=0;i<m_oJar.arr_class.Count;i++)
			{
				Jar.ClassRec oRec = (Jar.ClassRec)m_oJar.arr_class[i];
				if (sFilter_jar.Length != 0) 
				{
					if (oRec.sJarFileName.IndexOf(sFilter_jar) == -1)
						continue;
				}

				if (sFilter_class.Length != 0) 
				{
					if (oRec.sClassName.IndexOf(sFilter_class) == -1)
						continue;
				}

                ListViewItem item = lv_result.Items.Add(oRec.sJarFileName);
				item.SubItems.Add(oRec.sClassName);
			}
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            Cursor.Current = Cursors.Default;
            DispCount();
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

            lv_result.Items.Clear();	// 表示リストクリア

			// リスト表示
			for (int i=0;i<m_oJar.arr_class.Count;i++)
			{
				Jar.ClassRec oRec = (Jar.ClassRec)m_oJar.arr_class[i];
                ListViewItem item = lv_result.Items.Add(oRec.sJarFileName);
				item.SubItems.Add(oRec.sClassName);
			}
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            DispCount();
		
		}

		// 設定を保存
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            Properties.Settings.Default.Save();	    // アプリ設定ファイル保存

		}



	}
}
