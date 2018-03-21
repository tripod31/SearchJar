using System;
using System.Collections;	// ArrayList
using System.Windows.Forms;	// Application
using System.IO;
using System.Text;			// Encording
using System.Diagnostics;	// Process
using System.Text.RegularExpressions;

namespace SearchZip
{
	/// <summary>
	/// Util �̊T�v�̐����ł��B
	/// </summary>
	public class Util
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
        // filter:   ���K�\���̃t�B���^�[
		public static void GetFileNames(string sStartDir,Regex filter,ref ArrayList arrRet)
		{
			string[] sFileArr = Directory.GetFiles(sStartDir);
			for (int i=0;i<sFileArr.Length;i++)
			{
                if (filter.IsMatch(sFileArr[i]))
				    arrRet.Add(sFileArr[i]);
			}
			string[] sDirArr = Directory.GetDirectories(sStartDir);
			for (int i=0;i<sDirArr.Length;i++)
			{
				// �ċA�Ăяo��
				GetFileNames(sDirArr[i],filter,ref arrRet);
			}
		}
	}
}
