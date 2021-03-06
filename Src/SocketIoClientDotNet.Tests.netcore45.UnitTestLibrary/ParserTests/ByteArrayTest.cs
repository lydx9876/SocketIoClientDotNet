﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Parser;

namespace SocketIoClientDotNet.Tests.netcore45.UnitTestLibrary.ParserTests
{
    [TestClass]
    public class ByteArrayTest
    {
        private static Parser.Encoder Encoder = new Parser.Encoder();

        [TestMethod]
        public void EncodeByteArray()
        {
            var packet = new Packet(Parser.BINARY_EVENT)
            {
                Id = 23,
                Nsp = "/woot",
                Data = System.Text.Encoding.UTF8.GetBytes("abc")
            };
            Helpers.TestBin(packet);
        }

        [TestMethod]
        public void EncodeByteArray2()
        {
            var packet = new Packet(Parser.BINARY_EVENT)
            {
                Id = 0,
                Nsp = "/",
                Data = new byte[2]
            };
            Helpers.TestBin(packet);
        }



        [TestMethod]
        public void EncodeByteArrayInJson()
        {
            var exptected = System.Text.Encoding.UTF8.GetBytes("asdfasdf");
            var _args = new List<object> { "buffa" };
            _args.Add(exptected);

            var data = Packet.Args2JArray(_args);

            var packet = new Packet()
            {
                Type = Parser.BINARY_EVENT,
                Id = 999,
                Nsp = "/deep",
                Data = data
            };
            Helpers.TestBin(packet);
        }

        [TestMethod]
        public void EncodeByteArrayDeepInJson()
        {
            var buf = System.Text.Encoding.UTF8.GetBytes("howdy");
            var jobj = new JObject();
            jobj.Add("hello", "lol");
            jobj.Add("message", buf);
            jobj.Add("goodbye", "gotcha");

            var _args = new List<object> { "jsonbuff" };
            _args.Add(jobj);

            var data = Packet.Args2JArray(_args);

            var packet = new Packet()
            {
                Type = Parser.BINARY_EVENT,
                Id = 999,
                Nsp = "/deep",
                Data = data
            };
            Helpers.TestBin(packet);
        }


        // Cannot do any of the following in C# ???
        //[TestMethod]
        //public void EncodeDeepBinaryJSONWithNullValue()
        //{
        //    var data = JObject.Parse("{a: \"b\", c: 4, e: {g: null}, h: null}");
        //    data["h"].Replace(new Byte[9]);

        //    var packet = new Packet(Parser.BINARY_EVENT)
        //    {
        //        Id = 600,
        //        Nsp = "/",
        //        Data = data
        //    };
        //    Helpers.TestBin(packet);
        //}

        //[TestMethod]
        //public void EncodeBinaryAckWithByteArray()
        //{
        //    var data = JArray.Parse("[a, null, {}]");
        //    data[1] = System.Text.Encoding.UTF8.GetBytes("xxx");

        //    var packet = new Packet(Parser.BINARY_ACK)
        //    {
        //        Id = 127,
        //        Nsp = "/back",
        //        Data = data
        //    };
        //    Helpers.TestBin(packet);
        //}

        //[TestMethod]
        //public void CleanItselfUpOnClose()
        //{

        //    var packet = new Packet(Parser.ACK)
        //    {
        //        Id = 0,
        //        Nsp = "/",
        //        Data = System.Text.Encoding.UTF8.GetBytes("abc")
        //    };


        //    Encoder.Encode(packet, new Parser.Encoder.CallbackImp((encodedPackets) =>
        //    {
        //        var decoder = new Parser.Decoder();
        //        decoder.On(Parser.Decoder.EVENT_DECODED, new ListenerImpl((args) =>
        //        {
        //            throw new Exception("received a packet when not all data was sent.");
        //        }));

        //        decoder.Add((string)encodedPackets[0]);
        //        decoder.Add((byte[])encodedPackets[1]);
        //        decoder.Destroy();
        //        Assert.AreEqual(0, decoder.Reconstructor.Buffers.Count);
        //    }));


    }

}
