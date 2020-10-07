using System;
using System.Text;
using JsonSrcGen;

namespace JsonSrcGen.TypeGenerators
{
    public class CustomConverterValueTypeGenerator : IJsonGenerator
    {
        public string TypeName { get; }
        readonly CodeBuilder _classLevelBuilder;


        public readonly string _converterName;

        public CustomConverterValueTypeGenerator(string typeName, string converterClassName, CodeBuilder classLevelBuilder)
        {
            TypeName = typeName;
            _classLevelBuilder = classLevelBuilder;
            _converterName =  $"CustomConverter{UniqueNumberGenerator.UniqueNumber}";
            classLevelBuilder.AppendLine(2, $"static readonly ICustomConverterValueType<{typeName}> {_converterName} = new {converterClassName}();");
        }

        public CodeBuilder ClassLevelBuilder => _classLevelBuilder;


        public void GenerateFromJson(CodeBuilder codeBuilder, int indentLevel, JsonType type, Func<string, string> valueSetter, string valueGetter)
        {
            string propertyValueName = $"property{UniqueNumberGenerator.UniqueNumber}Value";
            codeBuilder.AppendLine(indentLevel, $"json = {_converterName}.FromJson(json, out {TypeName} {propertyValueName});");
            codeBuilder.AppendLine(indentLevel, valueSetter(propertyValueName));
        }

        public void GenerateToJson(CodeBuilder codeBuilder, int indentLevel, StringBuilder appendBuilder, JsonType type, string valueGetter)
        {
            codeBuilder.MakeAppend(indentLevel, appendBuilder);
            codeBuilder.AppendLine(indentLevel, $"{_converterName}.ToJson(builder, {valueGetter});");
        }
    }
}