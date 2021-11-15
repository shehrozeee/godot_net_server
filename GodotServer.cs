using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace godot_net_server
{
    public class GodotServer
    {
        HttpListener httpListener;
        WebSocket Socket;
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();
        public GodotServer(string ip = "127.0.0.1", int port = 8000)
        {
            s_cts.CancelAfter(5000);
            httpListener = new HttpListener();
            httpListener.Prefixes.Add($"http://{ip}:{port}/");
        }
        public async Task Start()
        {
            while (Socket == null)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    Socket = webSocketContext.WebSocket;
                }
            }
        }
        public async Task<string> SendMessageAndGetReply(string message)
        {
            await Socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                WebSocketMessageType.Text, true, CancellationToken.None);
            var incominingBuffer = new ArraySegment<byte>();
            await Socket.ReceiveAsync(incominingBuffer, s_cts.Token);
            return Encoding.UTF8.GetString(incominingBuffer);
        }
        string make_command(string command)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { cmd = command });
        }
        public async Task reset()
        {
            var resp = await SendMessageAndGetReply(make_command("reset"));

        }
        public IEnumerable<float> step(float action)
        {
            return new[] { 0f };
        }
        public void close()
        {

        }
        public void render()
        {

        }

    }
}
