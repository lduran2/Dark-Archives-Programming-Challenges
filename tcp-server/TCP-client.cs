/**
 * TCP-client.cs
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

static class TcpServerClientTest {
	static TraceSource logger = new TraceSource("TCP Client Test");

	static void Main(String[] args) {
		IPAddress ipAddress = new IPAddress(0x0100007f);
		int portNumber = 8092;
		int len = 256;
		Byte[] buff = new Byte[len];
		Byte[] response = new Byte[len];
		TcpServerClient.CallBack callback = new TcpServerClient.CallBack(TcpServerClientTest.hToY);

		TcpServerClient.Serve(ipAddress, portNumber, buff, len, response, logger, callback);
	} /* end void Main(String[]) */

	private static int hToY(int eventId, Byte[] buff, int len, Byte[] r) {
		String request;
		String process;
		Byte[] response;
		request = Encoding.UTF8.GetString(buff, 0, len);
		TcpServerClient.Log(logger, TraceEventType.Information, eventId, "Requested: {0}", request);
		process = request;
		response = Encoding.UTF8.GetBytes(process);
		return response.Length;
	}

}
namespace DarkArchives {
	static class TcpServerClient {
		private static int eventId = 0;

		public delegate int CallBack(int eventId, Byte[] buff, int len, Byte[] response);

		public static void Serve(IPAddress ipAddress, int portNumber, Byte[] buff, int len, Byte[] response, TraceSource logger, TcpServerClient.CallBack callback) {

			TcpClient client = null;
			NetworkStream stream;
			bool isRequested = true;

			try {
					while (isRequested) {
						TcpServerClient.Log(logger, TraceEventType.Verbose, eventId++, "Connecting to {0}:{1}", ipAddress, portNumber);
						try {
							using (client = new TcpClient()) {
								client.Connect(ipAddress, portNumber);
								TcpServerClient.Log(logger, TraceEventType.Start, eventId++, "Connected to {0}:{1}", ipAddress, portNumber);
								stream = client.GetStream();
								while (TcpServerClient.IsInputing(stream, buff, logger, callback));
							} /* end using (TcpClient client) */
						} /* end try { server.AcceptTcpClient(); } */
						finally {
							if (null != client) {
								client.Close();
							} /* end if (null != client) */
							isRequested = false;
						} /* end finally */
					} /* end while (isRequested) */
			} /* end try { server.Start(); server.Stop(); } */
			catch (SocketException se) {
				TcpServerClient.Log(logger, TraceEventType.Error, eventId++, "SocketException {1}: {0}", se, se.ErrorCode);
			} /* end catch (SocketException se) */
		} /* end void Serve(IPAddress, int, Byte[], int, TraceSource, TcpServerClient.CallBack) */

		private static bool IsInputing(NetworkStream stream, Byte[] buff, TraceSource logger, TcpServerClient.CallBack callback) {
			int nRead;
			String sResponse = Console.ReadLine();
			Byte[] bsResponse = Encoding.UTF8.GetBytes(sResponse);
			int len = bsResponse.Length;
			stream.Write(bsResponse, 0, len);
			nRead = stream.Read(buff, 0, len);
			TcpServerClient.Log(logger, TraceEventType.Verbose, (eventId + 1), "Responded: {0}", Encoding.UTF8.GetString(buff, 0, nRead));
			return (0 != sResponse.Length);
		}

		public static void Log(TraceSource logger, TraceEventType eventType, int id, String format, params Object[] objects) {
			String message = String.Format(format, objects);
			logger.TraceEvent(eventType, id, message);
			Console.Write("[{0}#{1}] {2}\n", eventType, id, message);
		} /* end void Log(TraceSource, TraceEventType, int, String, params Object[]) */
	} /* static class TcpServerClient */

} /* namespace DarkArchives */
