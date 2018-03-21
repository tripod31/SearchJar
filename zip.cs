using System;
using System.Collections;	// ArrayList
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;

namespace SearchZip
{
	/// <summary>
	/// </summary>
	
	public class Zip
	{
		public class FileRec
		{
			public string zipFileName { get; set; }
			public string fileName { get; set; }
		};
	
		// メンバ変数
		private string m_sErrMsg;
		private ArrayList m_arr_file = new ArrayList();

		// プロパティ
		public string ErrMsg
		{
			get
			{
				return m_sErrMsg;
			}
		}

		public ArrayList arr_file
		{
			get
			{
				return m_arr_file;
			}
		}


	
		public Zip()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//

		}

        // 指定ディレクトリ下のZIPファイル内のクラスをリスト
        public int SearchDir(string sDir,Regex zipName,string sFileName)
        {
            m_arr_file.Clear();
            // ディレクトリでZIPファイルを検索
            ArrayList arr_zip_files = new ArrayList();
            Util.GetFileNames(sDir, zipName, ref arr_zip_files);  // 全てのファイルを対象にする

            // ZIPファイル毎にファイル検索
            for (int i = 0; i < arr_zip_files.Count; i++)
            {
                int iRet = SearchFile(arr_zip_files[i].ToString(), sFileName);
            }
            return 0;
        }

        public ArrayList FilterFile(string filter_zip, string filter_file)
        {
            //LINQ
            var query = from FileRec c in arr_file
                        where c.zipFileName.Contains(filter_zip) && c.fileName.Contains(filter_file)
                        select c;

            // convert query to arraylist
            ArrayList arr = new ArrayList();
            foreach (FileRec c in query)
                arr.Add(c);
            return arr;
        }

        //　ZIPファイル内のクラスをリストに追加
        private int SearchFile(string _zipFileName, string _fileName)
        {
            //ZipFileオブジェクトの作成
            ZipFile zf=null;
            try
            {
                zf = new ZipFile(_zipFileName);
            }
            catch
            {
                // ZIPファイルではない
                return -1;
            }
            finally
            {
            }

            if (zf.TestArchive(false))
            {
                //ZIP内のエントリを列挙
                foreach (ZipEntry ze in zf)
                {
                    if (ze.IsFile &&
                        ((ze.Name.IndexOf(_fileName) != -1) || (_fileName.Length == 0)))
                    {
                        FileRec oRec = new FileRec();
                        oRec.zipFileName = _zipFileName;
                        oRec.fileName = ze.Name;
                        m_arr_file.Add(oRec);
                    }
                }
            }
            return 0;
        }


		// クラス名の配列をファイルに出力
		// sFilter_zip		ZIPファイル名のフィルタ。マッチするもののみ出力する。空の場合は全て出力
		// sFilter_file	    ファイル名のフィルタ。マッチするもののみ出力する。空の場合は全て出力
		public int WriteCSV(string sFileName,string sFilter_zip,string sFilter_file)
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

			for (int i=1;i<m_arr_file.Count;i++)
			{
				FileRec oRec = (FileRec)m_arr_file[i];

				if ( sFilter_zip.Length != 0 ) 
				{
					// フィルタにマッチしない場合スキップ
					if (oRec.zipFileName.IndexOf(sFilter_zip) == -1)
						continue;
				}
				
				if ( sFilter_file.Length != 0 ) 
				{
					// フィルタにマッチしない場合スキップ
					if (oRec.fileName.IndexOf(sFilter_file) == -1)
						continue;
				}

				string sLine = string.Format("\"{0}\",\"{1}\"",oRec.zipFileName,oRec.fileName);
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

			m_arr_file.Clear();
			while (!parser.EndOfData)
			{	
				string[] token = parser.ReadFields();
				FileRec oRec = new FileRec();
				oRec.zipFileName = token[0];
				oRec.fileName = token[1];
				m_arr_file.Add(oRec);
			}

			parser.Close();

			return 0;
		}

	}
}
