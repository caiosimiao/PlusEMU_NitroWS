﻿using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Plus.Network.Codec;
using System.Collections.Generic;
using System.Text;

namespace Plus.Network.Handler
{
    class MessageInterceptorHandler : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.GetString(0, 3, Encoding.UTF8).StartsWith("GET")) {
                // this is a websocket upgrade request, so add the appropriate decoders/encoders
                context.Channel.Pipeline.AddAfter("messageInterceptor", "websocketCodec", new WebSocketFrameCodec());
                context.Channel.Pipeline.AddAfter("messageInterceptor", "protocolHandler", new WebSocketServerProtocolHandler("/", true));
                context.Channel.Pipeline.AddAfter("messageInterceptor", "objectAggregator", new HttpObjectAggregator(65536));
                context.Channel.Pipeline.AddAfter("messageInterceptor", "httpCodec", new HttpServerCodec());
            }
            // Remove ourselves
            context.Channel.Pipeline.Remove(this);
        }
    }
}
