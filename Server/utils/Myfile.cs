﻿using System;
using System.IO;
using Server.utils;

namespace Server.Entities
{
	public class MyFile
	{
		string fullPath;
		string[] pathParts;
		string name;
		string ext;
		FileStream inStream;
		Boolean indexPage = true;

		/**
		 * Get the input stream
		 * @return
		 * @throws FileNotFoundException 
		 */
		private FileStream GetFileInputStream()
		{
			if (inStream == null)
			{
				inStream = new FileStream(fullPath, FileMode.Open);
			}
			return inStream;
		}

		/**
		 * Constructor
		 * parse filename
		 * @param path 
		 */
		public MyFile(string path)
		{
			fullPath = path;
			if (!Path.HasExtension(fullPath))
			{
				string orginalPath = fullPath;
				string[] defaultPages = AppConfigProcessor.Get().DefaultPages.Split(new string[] { ";" }, StringSplitOptions.None);
				Boolean result = false;
				foreach (string i in defaultPages)
				{
					fullPath = orginalPath + i;
					result = this.checkIfIndexPageExists(fullPath);
					if (result == true)
					{
						break;
					}
				}
			} else {
				indexPage = false;
			}

			pathParts = fullPath.Split('/');
			if (pathParts.Length > 0)
			{
				name = pathParts[pathParts.Length - 1];
			}
			else
			{
				name = "";
			}
		}

		protected Boolean checkIfIndexPageExists(String fullPath)
		{
			if (!File.Exists(fullPath))
			{
				return false;
			}
			indexPage = false;
			return true;
		}

		public Boolean indexPageExists()
		{
			return indexPage;
		}

		public override string ToString()
		{
			return fullPath;
		}

		/**
		 * 
		 * @return file name including extension
		 */
		public string GetName()
		{
			return name;
		}

		/**
		 * 
		 * @return file extension, including dot
		 */
		public string GetExtension()
		{
			if (ext == null)
			{
				int pos = name.LastIndexOf('.');
				if (pos < 0)
				{
					ext = "";
				}
				else
				{
					ext = name.Substring(pos);
				}
			}
			return ext;
		}

		/**
		 * Read bytes from file
		 * @param buffer: buffer to read to
		 * @param maxlen: maximum number of bytes to read
		 * @return number of bytes actually read, -1 means end of file
		 * @throws FileNotFoundException
		 * @throws IOException 
		 */
		public long Read(byte[] buffer, int maxlen)
		{
			GetFileInputStream();
			return inStream.Read(buffer, 0, maxlen);
		}

		/**
		 * Get the content type, determined by file extension (may be unreliable)
		 * @return content type as used in http headers
		 */
		public string GetContentType()
		{
			string lext = GetExtension().ToLower();
			switch (lext)
			{
				case ".pdf":
					return "application/pdf";
				case ".htm":
				case ".html":
					return "text/html; charset=UTF-8";
				case ".gif":
					return "image/gif";
				case ".jpg":
				case ".jpeg":
					return "image/jpeg";
				default:
					return "application/octet-stream\r\nContent-Disposition: attachment; filename=\"" + GetName() + "\"";
			}
		}

		public long GetLength()
		{
			GetFileInputStream();
			// FileAccess.

			return inStream.Length;
			//  inStream.GetChannel().size();
		}

		public void Close()
		{
			if (inStream != null)
			{
				inStream.Close();
			}
		}
	}
}