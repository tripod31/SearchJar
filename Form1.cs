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
	/// Form1 �̊T�v�̐����ł��B
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
    {
		
		// �����o�ϐ�
		private Zip m_oZip;
        private ArrayList m_arr_lv=new ArrayList();     // virtualListView�p�f�[�^�z��

        public Form1()
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
            //

            m_oZip = new Zip();
            // ListView�̗񕝂�ݒ�
            for (int i = 0;i< this.lv_result.Columns.Count; i++) {
                this.lv_result.Columns[i].Width = Int32.Parse(Properties.Settings.Default.lv_column_width[i]);
            }
        }

        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
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
                "{0}��",
                lv_result.Items.Count);

        }

		// �C�x���g�n���h�� 
		//////////////////////////////////////////////////////////////////////////////////////////

		// �����f�B���N�g���{�^��
		private void button_dir_Click(object sender, System.EventArgs e)
		{
			// �t�H���_�I���_�C�A���O�\��
            folderBrowserDialog1.ShowDialog(this);

            string sDir = folderBrowserDialog1.SelectedPath;
            if (sDir.Length !=0 )
                text_dir.Text = sDir.ToString();

		}

		// �����{�^��
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

			m_oZip.arr_file.Clear();	// ���X�g�N���A
            m_oZip.SearchDir(text_dir.Text, zipname, text_file_name.Text);
            disp(m_oZip.arr_file);
        }


		// �i���݃{�^��
		private void button_filter_Click(object sender, System.EventArgs e)
		{
            lv_result.Items.Clear();	// ���X�g�N���A

            ArrayList arr = m_oZip.FilterFile(text_filter_zip.Text, text_filter_file.Text);
            disp(arr);
		}
		
		// �t�@�C���ɏo�̓{�^��
		private void button_output_Click(object sender, System.EventArgs e)
		{
			// �t�@�C���_�C�A���O��\��
			SaveFileDialog oDlg = new SaveFileDialog();
			oDlg.DefaultExt="csv";
			oDlg.Filter = "csv�t�@�C��(*.csv)|*.csv";

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
				MessageBox.Show(sFile + "�ɏo�͂��܂���");
			}
		}

		// CSV�ǂݍ��݃{�^��
		private void button_read_Click(object sender, System.EventArgs e)
		{
			// �t�@�C���_�C�A���O��\��
			OpenFileDialog oDlg = new OpenFileDialog();
			oDlg.DefaultExt="csv";
			oDlg.Filter = "csv�t�@�C��(*.csv)|*.csv";

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

		// �ݒ��ۑ�
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            // ListView�̗񕝂�ۑ�
            for (int i = 0; i < this.lv_result.Columns.Count; i++)
            {
                Properties.Settings.Default.lv_column_width[i]=this.lv_result.Columns[i].Width.ToString() ;
            }
            Properties.Settings.Default.Save();	    // �A�v���ݒ�t�@�C���ۑ�

		}

        private void lv_result_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = (ListViewItem)this.m_arr_lv[e.ItemIndex];
        }

        private void lv_result_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            //lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);  // �����Ȃ��H
        }
    }
}
