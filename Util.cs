using System;
using System.Collections;	// ArrayList
using System.Windows.Forms;	// Application
using System.IO;
using System.Text;			// Encording
using System.Diagnostics;	// Process
using Common;

namespace SearchJar
{
	/// <summary>
	/// Util �̊T�v�̐����ł��B
	/// </summary>
	public class Util:UtilBase
	{
 
		public Util()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		// ��������󔒕����ŕ���
		// ���ʂ���󕶎���("")������
		public static ArrayList MySplit(string s)
		{
			ArrayList arr = new ArrayList();
			String[] tokens;
			
			tokens = s.Split(null);

			for (int i = 0;i<tokens.Length;i++)
			{
				if (tokens[i].Length != 0)
					arr.Add(tokens[i]);
			}
			return arr;
		}

		// �t�@�C�����ċA����
		public static void GetFileNames(string sStartDir,string sPattern,ref ArrayList arrRet)
		{
			string [] sFileArr = Directory.GetFiles(sStartDir,sPattern);
			for (int i=0;i<sFileArr.Length;i++) 
			{
				arrRet.Add(sFileArr[i]);
			}
			string [] sDirArr = Directory.GetDirectories(sStartDir);
			for (int i=0;i<sDirArr.Length;i++)
			{
				// �ċA�Ăяo��
				GetFileNames(sDirArr[i],sPattern,ref arrRet);
			}
		}


	}
}
