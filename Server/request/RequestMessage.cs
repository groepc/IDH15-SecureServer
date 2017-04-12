using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Server.Entities;
using Server.extensions;

namespace Server.request
{
	// A class representing the request that came from a client.
	public class RequestMessage
	{
		// Gets the HTTP method used for the request (GET, POST, etc.)
		public string HttpMethod { get; }

		// Gets the request path.
		public string Path { get; }

		// Gets any query string parameters that were present.
		public NameValueCollection QueryString { get; }
    
		// Gets the HTTP version of the request.
		public string HttpVersion { get; }

		// Gets a collection of the headers that were sent with the request.
		public NameValueCollection Headers { get; }

		// Gets a collection of data that was sent by an HTML form, if present.
		public NameValueCollection FormData { get; }

		public String RawHeader { get; }

		// Gets or sets the user that was authenticated and made this request.
		public User User { get; set; }

		// The constructor is private to force using the Create method for creating a new instance
		private RequestMessage(string httpMethod, string path, NameValueCollection queryString, string httpVersion, NameValueCollection headers, NameValueCollection formData, string rawHeader)
		{
			HttpMethod = httpMethod;
			Path = path;
			QueryString = queryString;
			HttpVersion = httpVersion;
			Headers = headers;
			FormData = formData;
			RawHeader = rawHeader;
		}

		public static RequestMessage Create(Stream inputStream)
		{
			string rawHeader = "";
			NameValueCollection headers = new NameValueCollection();
			NameValueCollection queryString = null;
			NameValueCollection formData = null;
			string httpMethod = null;
			string path = null;
			string httpVersion = null;

			bool readRequestLine = false;

			foreach (string header in ReadHeaders(inputStream))
			{
				rawHeader += header + "\r\n";
				// Parse the Request-Line if it was not read yet
				// Request-Line example:  GET http://www.website.com/something?key1=value1&key2=value2 HTTP/1.1
				if (!readRequestLine)
				{
					// If the request does not start with a Request-Line, it is propably not a request message we're dealing with
					if (string.IsNullOrEmpty(header))
						return null;

					// Validate the Request-Line

					string[] requestLineParts = header.Split(new[] { ' ' }, 3);

					// Parse all parts of the Request-Line
					httpMethod = requestLineParts[0].ToUpperInvariant();
					httpVersion = requestLineParts[2].ToUpperInvariant();

					string[] requestUriParts = requestLineParts[1].Trim('/').Split('?');
					path = requestUriParts[0];

					if (requestUriParts.Length > 1)
						queryString = HttpUtility.ParseQueryString(requestUriParts[1]);

					// Request-Line has been read, so that means the next lines are headers
					readRequestLine = true;
					continue;
				}

				// Parse the header
				string[] headerParts = header.Split(new[] { ':' }, 2);

				string headerName = headerParts[0].Trim();
				string headerValue = headerParts.Length > 1 ? headerParts[1].Trim() : string.Empty;

				headers.Add(headerName, headerValue);
			}

			if (!readRequestLine)
				return null;

			// Read body if the Content-Length header is present to determine if the request contains a body
			string contentLengthString = headers["Content-Length"];

			if (!string.IsNullOrEmpty(contentLengthString))
			{
				int contentLength;
				if (!int.TryParse(contentLengthString, out contentLength))
					throw new Exception("Invalid Content-Length header value.");

				// Always read the body content, if present
				using (Stream content = new MemoryStream())
				{
					inputStream.CopyTo(content, 0, contentLength);

					// Read and parse formdata if the body contains formdata
					formData = ParseFormData(headers, content);
				}
			}

			return new RequestMessage(
			    httpMethod,
			    path,
			    queryString ?? new NameValueCollection(),
			    httpVersion,
			    headers,
			    formData ?? new NameValueCollection(),
				rawHeader
			);
		}

		/// Enumerates through all the headers (including the Request-Line).
		private static IEnumerable<string> ReadHeaders(Stream inputStream)
		{
			// ASCII uses 1 byte per character
			// We read one character at a time
			// We don't use a StreamReader since it buffers more characters than we want, which may cause blocking
			using (BinaryReader reader = new BinaryReader(inputStream, Encoding.ASCII, true))
			{
				StringBuilder builder = new StringBuilder();

				while (true)
				{
					char c;

					try
					{
						c = reader.ReadChar();
					}
					catch (EndOfStreamException)
					{
						yield break;
					}

					if (c == '\n')
					{
						string header = builder.ToString(0, builder.Length - 1);

						if (string.IsNullOrEmpty(header))
							yield break;

						yield return header;

						builder = new StringBuilder();
					}
					else
					{
						builder.Append(c);
					}
				}
			}
		}

		// Checks if the request has a body containg form data, parses and places it into a new form data collection.
		private static NameValueCollection ParseFormData(NameValueCollection headers, Stream content)
		{
			string contentType = headers["Content-Type"];

			if (!string.IsNullOrEmpty(contentType))
			{
				string[] contentTypeParts = contentType.Split(';');

				if (string.Equals(contentTypeParts[0], "application/x-www-form-urlencoded", StringComparison.InvariantCultureIgnoreCase)) // TODO: multipart/form-data support?
				{
					// Parse charset if present or use default (the charset determines the encoding of the form data)
					string charsetPart = contentTypeParts.FirstOrDefault(ctp => ctp.StartsWith("charset", StringComparison.InvariantCultureIgnoreCase));
					string charset = charsetPart != null && charsetPart.Contains('=')
					    ? charsetPart.Split(new[] { '=' }, 2).Last()
					    : null;

					if (string.IsNullOrEmpty(charset))
						charset = "ISO-8859-1"; // default encoding for form data if no charset was specified

					Encoding encoding = Encoding.GetEncoding(charset);

					// Convert the content stream to a string
					byte[] contentBytes = new byte[content.Length];
					content.Position = 0L;
					content.Read(contentBytes, 0, contentBytes.Length);
					string formDataString = encoding.GetString(contentBytes);

					// Parse the form data (which should be the same format as a query string)
					if (!string.IsNullOrEmpty(formDataString))
						return HttpUtility.ParseQueryString(formDataString);
				}
			}
			return null;
		}

		// Creates a string representation of the Request-Line and includes the headers as well if that was specified. Does not output the body.
		public string ToString(bool includeHeaders)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("{0} /{1}", HttpMethod, Path);

			if (QueryString.Count > 0)
			{
				// Build and append the query string if any query parameters are present
				builder.Append('?');

				int fieldCount = 0;

				foreach (string fieldName in QueryString.Keys)
				{
					if (fieldCount > 0)
						builder.Append('&');

					string[] fieldValues = QueryString.GetValues(fieldName);

					if (fieldValues != null && fieldValues.Length > 0)
						builder.AppendFormat("{0}={1}", fieldName, string.Join(",", fieldValues.Select(HttpUtility.UrlEncode)));
					else
						builder.Append(fieldName);

					++fieldCount;
				}
			}

			builder.AppendFormat(" {0}", HttpVersion);

			// Append the headers if that was specified

			if (!includeHeaders)
				return builder.ToString();

			foreach (string headerName in Headers.Keys)
			{
				string[] headerValues = Headers.GetValues(headerName);
				string headerValuesString = headerValues != null
				    ? string.Join(";", headerValues)
				    : string.Empty;
				builder.AppendFormat("\r\n{0}: {1}", headerName, headerValuesString);
			}

			return builder.ToString();
		}

		// Creates a string representation of the Request-Line and includes the headers as well. Does not output the body.
		public override string ToString()
		{
			return ToString(true);
		}
	}
}