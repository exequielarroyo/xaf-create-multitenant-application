﻿using DevExpress.Pdf;
using DevExpress.XtraRichEdit;

namespace OutlookInspired.Module.Services.Internal{
    internal static class OfficeExtensions{
        private static readonly RichEditDocumentServer RichEditDocumentServer = new();
        static T ToDocument<T>(this byte[] bytes,Func<IRichEditDocumentServer,T> data) 
            => RichEditDocumentServer.ToDocument(bytes,() => data(RichEditDocumentServer),DocumentFormat.OpenXml);
        
        public static string ToDocumentText(this byte[] bytes) => bytes.ToDocument(server => server.Text);
        public static byte[] ToDocumentBytes(this byte[] bytes) => bytes.ToDocument(server => server.OpenXmlBytes);

        public static T ToDocument<T>(this IRichEditDocumentServer server,byte[] bytes,Func<T> data,DocumentFormat? documentFormat=null){
            if (bytes == null || bytes.Length == 0){
                return default;
            }
            using var memoryStream = new MemoryStream(bytes);
            server.LoadDocument(memoryStream, documentFormat??DocumentFormat.Undefined);
            return data();
        }

        public static byte[] AddWaterMark(this byte[] bytes,string text){
            using var processor = new PdfDocumentProcessor();
            using var memoryStream = new MemoryStream(bytes);
            processor.LoadDocument(memoryStream);
            processor.AddWatermark(text);
            using var stream = new MemoryStream();
            processor.SaveDocument(stream);
            return stream.ToArray();
        }

        public static byte[] ToPdf(this byte[] bytes){
            using var richEditDocumentServer = new RichEditDocumentServer();
            richEditDocumentServer.LoadDocument(bytes,DocumentFormat.OpenXml);
            using var memoryStream = new MemoryStream();
            richEditDocumentServer.ExportToPdf(memoryStream);
            return memoryStream.ToArray();
        }

        public static RichEditDocumentServer CreateDocumentServer(this byte[] bytes, params object[] dataSource) 
            => new(){
                OpenXmlBytes = bytes,
                Options ={
                    MailMerge ={
                        DataSource = dataSource
                    }
                }
            };

    }
}