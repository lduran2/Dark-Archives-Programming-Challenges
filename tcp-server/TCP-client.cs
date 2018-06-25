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

	static void Main(string[] args) {
		IPAddress ipAddress = new IPAddress(0x0100007f);
		int portNumber = 8092;
		int len = 256;
		Byte[] buff = new Byte[len];
		Byte[] response = new Byte[len];

		TcpServerClient.Serve(ipAddress, portNumber, buff, len, response, logger);
	} /* end void Main(String[]) */

}
namespace DarkArchives {
	static class TcpServerClient {
		private static int eventId = 0;

		public static void Serve(IPAddress ipAddress, int portNumber, Byte[] buff, int len, Byte[] response, TraceSource logger) {

			TcpClient client = null;
			NetworkStream stream;

			try {
				TcpServerClient.Log(logger, TraceEventType.Verbose, eventId++, "Connecting to {0}:{1}", ipAddress, portNumber);
 				using (client = new TcpClient()) {
					client.Connect(ipAddress, portNumber);
					TcpServerClient.Log(logger, TraceEventType.Start, eventId++, "Connected to {0}:{1}", ipAddress, portNumber);
					stream = client.GetStream();
					while (TcpServerClient.IsInputing(stream, buff, logger));
				} /* end using (TcpClient client) */
			} /* end try { server.Start(); server.AcceptTcpClient(); server.Stop(); } */
			catch (SocketException se) {
				TcpServerClient.Log(logger, TraceEventType.Error, eventId++, "SocketException {1}: {0}", se, se.ErrorCode);
			} /* end catch (SocketException se) */
			finally {
				if (null != client) {
					client.Close();
				} /* end if (null != client) */
			} /* end finally */
		} /* end void Serve(IPAddress, int, Byte[], int, TraceSource) */

		private static bool IsInputing(NetworkStream stream, Byte[] buff, TraceSource logger) {
			int nRead;
			string sResponse = Console.ReadLine();
			if (sResponse.Equals(String.Empty)) {
				return false;
			}
			Byte[] bsResponse = Encoding.UTF8.GetBytes(sResponse);
			int len = bsResponse.Length;
			stream.Write(bsResponse, 0, len);
			nRead = stream.Read(buff, 0, len);
			TcpServerClient.Log(logger, TraceEventType.Verbose, (eventId + 1), "Responded: {0}", Encoding.UTF8.GetString(buff, 0, nRead));
			Console.Write("{0}\n", sResponse.Length);
			return true;
		}

		public static void Log(TraceSource logger, TraceEventType eventType, int id, string format, params Object[] objects) {
			string message = String.Format(format, objects);
			logger.TraceEvent(eventType, id, message);
			Console.Write("[{0}#{1}] {2}\n", eventType, id, message);
		} /* end void Log(TraceSource, TraceEventType, int, String, params Object[]) */
	} /* static class TcpServerClient */

} /* namespace DarkArchives */
