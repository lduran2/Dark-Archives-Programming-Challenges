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

namespace DarkArchives {
	static class TcpServer {
		delegate int CallBack(Byte[] buff, int len, Byte[] response);

		static int eventId = 0;

		static void Main(String[] args) {
		} /* end void Main(String[]) */

		static void Serve(IPAddress ipAddress, int portNumber, Byte[] buff, int len, Byte[] response, TraceSource logger, CallBack callback) {

			TcpListener server = null;
			TcpClient client = null;
			NetworkStream stream;

			try {
				server = new TcpListener(ipAddress, portNumber);
				server.Start();

				while (true) {
					try {
						using (client = server.AcceptTcpClient()) {
							logger.TraceEvent(TraceEventType.Start, eventId++, "Connected to {0}:{1}", ipAddress, portNumber);
							stream = client.GetStream();
							while (processing(stream, buff, len, response, callback));
						} /* end using (TcpClient client) */
					} /* end try { server.AcceptTcpClient(); } */
					finally {
						if (null != client) {
							client.Close();
						} /* end if (null != client) */
					} /* end finally */
				} /* end while (true) */
			} /* end try { server.Start(); } */
			catch (SocketException se) {
				logger.TraceEvent(TraceEventType.Error, eventId++, "SocketException: {0}", se);
			} /* end catch (SocketException se) */
			finally {
				if (null != server) {
					server.Stop();
				} /* end if (null != server) */
			} /* end finally */
		} /* end void Serve(IPAddress, int, Byte[], int, TraceSource, CallBack) */

		private static bool processing(NetworkStream stream, Byte[] buff, int len, Byte[] response, CallBack callback) {
			int nRead;
			int nProcessed;
			nRead = stream.Read(buff, 0, len);
			nProcessed = callback(buff, nRead, response);
			stream.Write(response, 0, nProcessed);
			return (0 != nRead);
		}
	} /* static class TcpServer */
} /* namespace DarkArchives */
