using System;
using System.Collections;	// ArrayList
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace SearchJar
{
	/// <summary>
	/// Jar の概要の説明です。
	/// </summary>
	
	public class Jar
	{
		public struct ClassRec
		{
			public string sJarFileName;
			public string sClassName;
		};

		public const string TMP_FILENAME = "tmp_jar_list.txt";
		
		// メンバ変数
		private string m_sErrMsg;
		private ArrayList m_arr_class = new ArrayList();

		// プロパティ
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
			// TODO: コンストラクタ ロジックをここに追加してください。
			//

		}

		//　Jarファイルの中からクラスを検索
		public int SearchClass(string _jarFileName,string _className)
		{
            //ZipFileオブジェクトの作成
            ICSharpCode.SharpZipLib.Zip.ZipFile zf =
                new ICSharpCode.SharpZipLib.Zip.ZipFile(_jarFileName);

            //ZIP内のエントリを列挙
            foreach (ICSharpCode.SharpZipLib.Zip.ZipEntry ze in zf)
            {
                if  (ze.IsFile &&
                    ((ze.Name.IndexOf(_className) != -1) || (_className.Length == 0)))
                {
                    ClassRec oRec = new ClassRec();
                    oRec.sJarFileName = _jarFileName;
                    oRec.sClassName = ze.Name;
                    m_arr_class.Add(oRec);
                }
            }

            //閉じる
            zf.Close();

			return 0;
		}


		// クラス名の配列をファイルに出力
		// sFilter_jar		JAR名のフィルタ。マッチするもののみ出力する。空の場合は全て出力
		// sFilter_class	クラス名のフィルタ。マッチするもののみ出力する。空の場合は全て出力
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
					// フィルタにマッチしない場合スキップ
					if (oRec.sJarFileName.IndexOf(sFilter_jar) == -1)
						continue;
				}
				
				if ( sFilter_class.Length != 0 ) 
				{
					// フィルタにマッチしない場合スキップ
					if (oRec.sClassName.IndexOf(sFilter_class) == -1)
						continue;
				}

				string sLine = string.Format("\"{0}\",\"{1}\"",oRec.sJarFileName,oRec.sClassName);
				sw.WriteLine(sLine);
			}

			sw.Close();
			fs.Close();

			return 0;
		}

		// CSVファイル読み込み
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
            parser.SetDelimiters(","); // 区切り文字はコンマ

			m_arr_class.Clear();
			while (!parser.EndOfData)
			{	
				string[] token = parser.ReadFields();
				ClassRec oRec = new ClassRec();
				oRec.sJarFileName = token[0];
				oRec.sClassName = token[1];
				m_arr_class.Add(oRec);
			}

			parser.Close();

			return 0;
		}

	}
}
