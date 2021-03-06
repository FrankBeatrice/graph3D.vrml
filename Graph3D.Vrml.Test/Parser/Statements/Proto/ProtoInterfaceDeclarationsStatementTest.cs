﻿using System.IO;
using System.Linq;
using Graph3D.Vrml.Fields;
using Graph3D.Vrml.Parser;
using Graph3D.Vrml.Parser.Statements.Extern;
using Graph3D.Vrml.Parser.Statements.Proto;
using Graph3D.Vrml.Tokenizer;
using NUnit.Framework;

namespace Graph3D.Vrml.Test.Parser.Statements.Proto {
    [TestFixture]
    public class ProtoInterfaceDeclarationsStatementTest {

        [Test]
        public void ParseTest() {
            var context = new ParserContext(new Vrml97Tokenizer(new StringReader(@"
[
    eventIn SFInt32 eventIn1
    eventOut SFInt32 eventOut1
    eventIn SFInt32 eventIn2
    eventOut SFInt32 eventOut2
    field SFInt32 field1 1
    exposedField SFInt32 exposedField1 2
    field SFInt32 field2 3
    exposedField SFInt32 exposedField2 4
]
")));
            var statement = ProtoInterfaceDeclarationsStatement.Parse(context, c => { });
            Assert.AreEqual(2, statement.EventsIn.Count);
            Assert.AreEqual(2, statement.EventsOut.Count);
            Assert.AreEqual(2, statement.Fields.Count);
            Assert.AreEqual(2, statement.ExposedFields.Count);

            var firstEventIn = statement.EventsIn.First();
            Assert.AreEqual("SFInt32", firstEventIn.FieldType);
            Assert.AreEqual("eventIn1", firstEventIn.EventId);

            var secondEventIn = statement.EventsIn.Last();
            Assert.AreEqual("SFInt32", secondEventIn.FieldType);
            Assert.AreEqual("eventIn2", secondEventIn.EventId);

            var firstEventOut = statement.EventsOut.First();
            Assert.AreEqual("SFInt32", firstEventOut.FieldType);
            Assert.AreEqual("eventOut1", firstEventOut.EventId);

            var secondEventOut = statement.EventsOut.Last();
            Assert.AreEqual("SFInt32", secondEventOut.FieldType);
            Assert.AreEqual("eventOut2", secondEventOut.EventId);

            var firstField = statement.Fields.First();
            Assert.AreEqual("SFInt32", firstField.FieldType);
            Assert.AreEqual("field1", firstField.FieldId);
            Assert.AreEqual(1, ((SFInt32)firstField.Value).Value);

            var secondField = statement.Fields.Last();
            Assert.AreEqual("SFInt32", secondField.FieldType);
            Assert.AreEqual("field2", secondField.FieldId);
            Assert.AreEqual(3, ((SFInt32)secondField.Value).Value);

            var firstExposedField = statement.ExposedFields.First();
            Assert.AreEqual("SFInt32", firstExposedField.FieldType);
            Assert.AreEqual("exposedField1", firstExposedField.FieldId);
            Assert.AreEqual(2, ((SFInt32)firstExposedField.Value).Value);

            var secondExposedField = statement.ExposedFields.Last();
            Assert.AreEqual("SFInt32", secondExposedField.FieldType);
            Assert.AreEqual("exposedField2", secondExposedField.FieldId);
            Assert.AreEqual(4, ((SFInt32)secondExposedField.Value).Value);
        }

        [Test]
        public void ParseEmptyTest() {
            var context = new ParserContext(new Vrml97Tokenizer(new StringReader(@"
[
]
")));
            var statement = ExternInterfaceDeclarationsStatement.Parse(context);
            Assert.AreEqual(0, statement.EventsIn.Count);
            Assert.AreEqual(0, statement.EventsOut.Count);
            Assert.AreEqual(0, statement.Fields.Count);
            Assert.AreEqual(0, statement.ExposedFields.Count);
        }
    }
}
