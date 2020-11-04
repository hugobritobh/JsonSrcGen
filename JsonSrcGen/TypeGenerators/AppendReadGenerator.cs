using System;
using System.Text;
using JsonSrcGen;

namespace JsonSrcGen.TypeGenerators
{
    public class AppendReadGenerator : IJsonGenerator
    {
        public string TypeName {get;}
        public string ReadType {get;set;}

        public AppendReadGenerator(string typeName)
        {
            TypeName = typeName;
        }

        public void GenerateFromJson(CodeBuilder codeBuilder, int indentLevel, JsonType type, Func<string, string> valueSetter, string valueGetter)
        {
            string propertyValueName = $"property{UniqueNumberGenerator.UniqueNumber}Value";
            if(ReadType == null)
            {
                codeBuilder.AppendLine(indentLevel, $"json = json.Read(out {TypeName} {propertyValueName});");
                codeBuilder.AppendLine(indentLevel, valueSetter(propertyValueName));
            }
            else
            {
                codeBuilder.AppendLine(indentLevel, $"json = json.Read(out {ReadType} {propertyValueName});");
                codeBuilder.AppendLine(indentLevel, valueSetter($"({TypeName}){propertyValueName}"));
            }
        }

        public void GenerateToJson(CodeBuilder codeBuilder, int indentLevel, StringBuilder appendBuilder, JsonType type, string valueGetter, bool canBeNull)
        {
            codeBuilder.MakeAppend(indentLevel, appendBuilder);
            codeBuilder.AppendLine(indentLevel, $"builder.Append({valueGetter});");
        }

        public CodeBuilder ClassLevelBuilder => null;

        public void OnNewObject(CodeBuilder codeBuilder, int indentLevel, Func<string, string> valueSetter)
        {

        }

        public void OnObjectFinished(CodeBuilder codeBuilder, int indentLevel, Func<string, string> valueSetter)
        {
            
        }
    }
}