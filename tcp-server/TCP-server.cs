/**
 * TCP-server.cs
 * A simple TCP Server.
 * by: https://github.com/lduran2
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DarkArchives;

static class TcpServerTest {
	static TraceSource logger = new TraceSource("TCP Server Test");

	static void Main(String[] args) {
		IPAddress ipAddress = new IPAddress(0x0100007f);
		int portNumber = 80;
		int len = 256;
		Byte[] buff = new Byte[len];
		Byte[] response = new Byte[len];
		TcpServer.CallBack callback = new TcpServer.CallBack(TcpServerTest.hToY);

		TcpServer.Serve(ipAddress, portNumber, buff, len, response, logger, callback);
	} /* end void Main(String[]) */

	private static int hToY(int eventId, Byte[] buff, int len, Byte[] r) {
		String request;
		String process;
		Byte[] response;
		request = Encoding.Unicode.GetString(buff, 0, len);
		TcpServer.Log(logger, TraceEventType.Information, eventId, "Requested: {0}", request);
		process = request.Replace("H", "Y").Replace("h", "y");
		response = Encoding.Unicode.GetBytes(process);
		return response.Length;
	}

}
namespace DarkArchives {
	static class TcpServer {
		private static int eventId = 0;

		public delegate int CallBack(int eventId, Byte[] buff, int len, Byte[] response);

		public static void Serve(IPAddress ipAddress, int portNumber, Byte[] buff, int len, Byte[] response, TraceSource logger, TcpServer.CallBack callback) {

			TcpListener server = null;
			TcpClient client = null;
			NetworkStream stream;
			bool isRequested = true;

			try {
				try {
					server = new TcpListener(ipAddress, portNumber);
					server.Start();

					while (isRequested) {
						try {
							using (client = server.AcceptTcpClient()) {
								TcpServer.Log(logger, TraceEventType.Start, eventId++, "Connected to {0}:{1}", ipAddress, portNumber);
								stream = client.GetStream();
								while (TcpServer.Processing(stream, buff, len, response, logger, callback));
							} /* end using (TcpClient client) */
						} /* end try { server.AcceptTcpClient(); } */
						finally {
							if (null != client) {
								client.Close();
							} /* end if (null != client) */
							isRequested = false;
						} /* end finally */
					} /* end while (isRequested) */
				} /* end try { server.Start(); } */
				finally {
					if (null != server) {
						server.Stop();
					} /* end if (null != server) */
				} /* end finally */
			} /* end try { server.Start(); server.Stop(); } */
			catch (SocketException se) {
				TcpServer.Log(logger, TraceEventType.Error, eventId++, "SocketException {1}: {0}", se, se.ErrorCode);
			} /* end catch (SocketException se) */
		} /* end void Serve(IPAddress, int, Byte[], int, TraceSource, TcpServer.CallBack) */

		private static bool Processing(NetworkStream stream, Byte[] buff, int len, Byte[] response, TraceSource logger, TcpServer.CallBack callback) {
			int nRead;
			int nProcessed;
			nRead = stream.Read(buff, 0, len);
			nProcessed = callback(eventId, buff, nRead, response);
			TcpServer.Log(logger, TraceEventType.Verbose, (eventId + 1), "Responded: {0}", response);
			stream.Write(response, 0, nProcessed);
			return (0 != nRead);
		}

		public static void Log(TraceSource logger, TraceEventType eventType, int id, String format, params Object[] objects) {
			String message = String.Format(format, objects);
			logger.TraceEvent(eventType, id, message);
			Console.Write("[{0}#{1}] {2}", eventType, id, message);
		} /* end void Log(TraceSource, TraceEventType, int, String, params Object[]) */
	} /* static class TcpServer */

} /* namespace DarkArchives */
