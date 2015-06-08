using System;
using System.Collections;	// ArrayList
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

namespace SearchJar
{
	/// <summary>
	/// Jar �̊T�v�̐����ł��B
	/// </summary>
	
	public class Jar
	{
		public class ClassRec
		{
			public string jarFileName { get; set; }
			public string className { get; set; }
		};

		public const string TMP_FILENAME = "tmp_jar_list.txt";
		
		// �����o�ϐ�
		private string m_sErrMsg;
		private ArrayList m_arr_class = new ArrayList();

		// �v���p�e�B
		public string ErrMsg
		{
			get
			{
				return m_sErrMsg;
			}
		}

		public ArrayList arr_class
		{
			get
			{
				return m_arr_class;
			}
		}


	
		public Jar()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//

		}

        // �w��f�B���N�g������JAR�t�@�C�����̃N���X�����X�g
        public int SearchDir(string sDir,string sClassName)
        {
            m_arr_class.Clear();
            // �f�B���N�g����*.jar�t�@�C��������
            ArrayList arr_jar_files = new ArrayList();
            Util.GetFileNames(sDir, "*.jar", ref arr_jar_files);

            // jar�t�@�C�����ɃN���X����		
            for (int i = 0; i < arr_jar_files.Count; i++)
            {
                string sJarFileName = arr_jar_files[i].ToString();

                int iRet = SearchClass(sJarFileName, sClassName);
            }
            return 0;
        }

        public ArrayList FilterClass(string filter_jar, string filter_class)
        {
            //LINQ
            var query = from ClassRec c in arr_class
                        where c.jarFileName.Contains(filter_jar) && c.className.Contains(filter_class)
                        select c;

            // convert query to arraylist
            ArrayList arr = new ArrayList();
            foreach (ClassRec c in query)
                arr.Add(c);
            return arr;
        }

		//�@Jar�t�@�C�����̃N���X�����X�g�ɒǉ�
		private int SearchClass(string _jarFileName,string _className)
		{
            //ZipFile�I�u�W�F�N�g�̍쐬
            ICSharpCode.SharpZipLib.Zip.ZipFile zf =
                new ICSharpCode.SharpZipLib.Zip.ZipFile(_jarFileName);

            //ZIP���̃G���g�����
            foreach (ICSharpCode.SharpZipLib.Zip.ZipEntry ze in zf)
            {
                if  (ze.IsFile &&
                    ((ze.Name.IndexOf(_className) != -1) || (_className.Length == 0)))
                {
                    ClassRec oRec = new ClassRec();
                    oRec.jarFileName = _jarFileName;
                    oRec.className = ze.Name;
                    m_arr_class.Add(oRec);
                }
            }

            //����
            zf.Close();

			return 0;
		}


		// �N���X���̔z����t�@�C���ɏo��
		// sFilter_jar		JAR���̃t�B���^�B�}�b�`������̂̂ݏo�͂���B��̏ꍇ�͑S�ďo��
		// sFilter_class	�N���X���̃t�B���^�B�}�b�`������̂̂ݏo�͂���B��̏ꍇ�͑S�ďo��
		public int WriteCSV(string sFileName,string sFilter_jar,string sFilter_class)
		{
			FileStream   fs =null;
			try 
			{
				fs = new FileStream(sFileName, FileMode.Create ,FileAccess.Write);
			} 
			catch  ( Exception e )
			{
				m_sErrMsg = e.ToString();
				return -1;
			}
			StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(932));

			for (int i=1;i<m_arr_class.Count;i++)
			{
				ClassRec oRec = (ClassRec)m_arr_class[i];

				if ( sFilter_jar.Length != 0 ) 
				{
					// �t�B���^�Ƀ}�b�`���Ȃ��ꍇ�X�L�b�v
					if (oRec.jarFileName.IndexOf(sFilter_jar) == -1)
						continue;
				}
				
				if ( sFilter_class.Length != 0 ) 
				{
					// �t�B���^�Ƀ}�b�`���Ȃ��ꍇ�X�L�b�v
					if (oRec.className.IndexOf(sFilter_class) == -1)
						continue;
				}

				string sLine = string.Format("\"{0}\",\"{1}\"",oRec.jarFileName,oRec.className);
				sw.WriteLine(sLine);
			}

			sw.Close();
			fs.Close();

			return 0;
		}

		// CSV�t�@�C���ǂݍ���
		public int ReadCSV(string sFileName)
		{
            TextFieldParser parser;
            try 
			{
                parser = new TextFieldParser(sFileName);
			} 
			catch  ( Exception e )
			{
				m_sErrMsg = e.ToString();
				return -1;
			}

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // ��؂蕶���̓R���}

			m_arr_class.Clear();
			while (!parser.EndOfData)
			{	
				string[] token = parser.ReadFields();
				ClassRec oRec = new ClassRec();
				oRec.jarFileName = token[0];
				oRec.className = token[1];
				m_arr_class.Add(oRec);
			}

			parser.Close();

			return 0;
		}

	}
}
