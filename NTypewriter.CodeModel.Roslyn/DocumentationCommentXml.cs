using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class DocumentationCommentXml : IDocumentationCommentXml
    {      
        private readonly string xml;
        private readonly Lazy<RootNode> rootNode;
        private static readonly Regex ExtractElementRegex = new Regex(@"(\<(param|summary|returns)(.*?)\>)(.*?)(\<\/\2\>)", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex ExtractNameRegex = new Regex("name=\"(.*?)\"", RegexOptions.Singleline | RegexOptions.Compiled);

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
            return new DocumentationCommentXml(xml);
        }


        public override string ToString()
        {
            return xml;
        }

        private RootNode Deserialize(string xml)
        {
            var result = new RootNode(); 

            if (string.IsNullOrEmpty(xml))
            {
                return result;
            }

            var matches = ExtractElementRegex.Matches(xml);
            foreach (Match match in matches)
            {
                var elementName = match.Groups[2].Value;
                var value = match.Groups[4].Value.Trim();

                switch (elementName)
                {
                    case "summary":
                        result.Summary = value;
                        break;
                    case "returns":
                        result.Returns = value;
                        break;
                    case "param":
                        var param = new Param();
                        param.Value = value;
                        var arguments = match.Groups[3].Value;
                        var nameMatch = ExtractNameRegex.Match(arguments);
                        param.Name = nameMatch.Groups[1].Value.Trim();                        
                        result.Params.Add(param);
                        break;
                }
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
        public List<Param> Params { get; set; } = new List<Param>();
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