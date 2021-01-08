using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class DocumentationCommentXml : IDocumentationCommentXml
    {      
        private readonly string xml;
        private readonly Lazy<RootNode> rootNode;

        public string Summary => rootNode.Value.Summary;
        public string Returns => rootNode.Value.Returns;
        public IEnumerable<IDocumentationCommentXmlParam> Params => rootNode.Value.Params;
        

        private DocumentationCommentXml(string xml)
        {
            this.xml = xml;
            rootNode = new Lazy<RootNode>(() => Deserialize(xml));
        }       


        public static DocumentationCommentXml Create(ISymbol symbol)
        {
            var xml = symbol.GetDocumentationCommentXml();
            if (!xml.Contains("</member>"))
            {
                xml = $"<member>{xml}</member>";
            }
            return new DocumentationCommentXml(xml);
        }


        public override string ToString()
        {
            return xml;
        }

        private RootNode Deserialize(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new RootNode();
            }

            RootNode result = null;
            var serializer = new XmlSerializer(typeof(RootNode)); 
            using (TextReader reader = new StringReader(xml))
            { 
                result = serializer.Deserialize(reader) as RootNode;             
            }

            return result;
        }
    }

    [XmlRoot(elementName:"member")]
    public class RootNode
    {
        [XmlElement(elementName: "summary")]       
        public string Summary { get; set; }
        [XmlElement(elementName: "returns")]
        public string Returns { get; set; }
        [XmlElement(elementName: "param")]
        public List<Param> Params { get; set; }
    }

    public class Param : IDocumentationCommentXmlParam
    {
        [XmlAttribute(attributeName: "name")]
        public string Name { get; set; }
        [XmlText]
        public string Value { get; set; }


        public override string ToString()
        {
            return $"{Name} - {Value}";
        }
    }
}