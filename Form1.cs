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
	/// Form1 �̊T�v�̐����ł��B
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
    {
		
		// �����o�ϐ�
		private Jar m_oJar;
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

            m_oJar = new Jar();
            this.lv_result.Columns[0].Width = Properties.Settings.Default.jar_width;
            this.lv_result.Columns[1].Width = Properties.Settings.Default.class_width;

        }

        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
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

			m_oJar.arr_class.Clear();	// ���X�g�N���A

			string sDir =  text_dir.Text;
			string sClassName = text_class_name.Text;

            m_oJar.SearchDir(sDir, sClassName);
            disp(m_oJar.arr_class);
        }


		// �i���݃{�^��
		private void button_filter_Click(object sender, System.EventArgs e)
		{
            lv_result.Items.Clear();	// ���X�g�N���A

			string sFilter_jar = text_filter_jar.Text;
			string sFilter_class =  text_filter_class.Text;

            ArrayList arr = m_oJar.FilterClass(sFilter_jar, sFilter_class);
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

			int iRet = m_oJar.ReadCSV(sFile);
			if (iRet == -1)
			{
				MessageBox.Show(m_oJar.ErrMsg);
				return;
			}
            disp(m_oJar.arr_class);
		
		}

		// �ݒ��ۑ�
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            Properties.Settings.Default.jar_width= this.lv_result.Columns[0].Width;
            Properties.Settings.Default.class_width = this.lv_result.Columns[1].Width;
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
