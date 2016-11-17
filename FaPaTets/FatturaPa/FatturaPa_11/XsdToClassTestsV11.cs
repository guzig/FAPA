using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.CSharp;
using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    [TestFixture]
    public class XsdToClassTestsV11
    {
        // Test for XmlSchemaImporter
        [Test]
        public void XsdToClassTest_V11()
        {
            // identify the path to the xsd
            string xsdFileName = "fatturapa_v1.1.xsd";
            string xmlFileName = "fatturapa_v1.1.xml";
            string path = @"C:\Users\Tonio\Desktop\EM\EnergyManager\EnergyManager\EmulTests\DomainServices\FatturaPa\FatturaPa_11\";
            string xsdPath = Path.Combine(path, xsdFileName);
            string xmlPath = Path.Combine(path, xmlFileName);

            // load the xsd
            XmlSchema xsd;
            using (FileStream stream = new FileStream(xsdPath, FileMode.Open, FileAccess.Read))
            {
                xsd = XmlSchema.Read(stream, null);
            }

            Console.WriteLine("xsd.IsCompiled {0}", xsd.IsCompiled);

            XmlSchemas xsds = new XmlSchemas();
            xsds.Add(xsd);
            xsds.Compile(null, true);
            XmlSchemaImporter schemaImporter = new XmlSchemaImporter(xsds);

            // create the codedom
            CodeNamespace codeNamespace = new CodeNamespace("Generated");
            XmlCodeExporter codeExporter = new XmlCodeExporter(codeNamespace);

            var maps = new List<object>();
            foreach (XmlSchemaType schemaType in xsd.SchemaTypes.Values)
            {
                maps.Add(schemaImporter.ImportSchemaType(schemaType.QualifiedName));
            }
            foreach (XmlSchemaElement schemaElement in xsd.Elements.Values)
            {
                maps.Add(schemaImporter.ImportTypeMapping(schemaElement.QualifiedName));
            }
            foreach (XmlTypeMapping map in maps)
            {
                codeExporter.ExportTypeMapping(map);
            }

            //RemoveAttributes(codeNamespace);

            // Check for invalid characters in identifiers
            CodeGenerator.ValidateIdentifiers(codeNamespace);

            // output the C# code
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            using (StringWriter writer = new StringWriter())
            {
                codeProvider.GenerateCodeFromNamespace(codeNamespace, writer, new CodeGeneratorOptions());
                File.WriteAllText(xmlPath, writer.GetStringBuilder().ToString());
                Console.WriteLine(writer.GetStringBuilder().ToString());
            }
        }

        // Remove all the attributes from each type in the CodeNamespace, except
        // System.Xml.Serialization.XmlTypeAttribute
        private void RemoveAttributes(CodeNamespace codeNamespace)
        {
            foreach (CodeTypeDeclaration codeType in codeNamespace.Types)
            {
                CodeAttributeDeclaration xmlTypeAttribute = null;
                foreach (CodeAttributeDeclaration codeAttribute in codeType.CustomAttributes)
                {
                    Console.WriteLine(codeAttribute.Name);
                    if (codeAttribute.Name == "System.Xml.Serialization.XmlTypeAttribute")
                    {
                        xmlTypeAttribute = codeAttribute;
                    }
                }
                codeType.CustomAttributes.Clear();
                if (xmlTypeAttribute != null)
                {
                    codeType.CustomAttributes.Add(xmlTypeAttribute);
                }
            }
        }
    }
}