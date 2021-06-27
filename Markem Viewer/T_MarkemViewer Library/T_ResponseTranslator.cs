using MarkemViewer_Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace T_MarkemViewer_Library
{
    public class T_ResponseTranslator
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("")]
        [TestCase("{~TR1}")]
        [TestCase("{~null}")]
        [TestCase("   ")]
        public void TranslateResponse_IsThrowingArgumentExc_WhenReceivingInvalidResponse(string response)
        {
            ResponseTranslator translator = new ResponseTranslator();
            Assert.Throws<ArgumentException>(() => translator.TranslateResponse(response));
        }


        [TestCase("{~UD0|35.0|}", "35.0")]
        [TestCase("{~UD0|350000000000000000000000|}", "350000000000000000000000")]
        [TestCase("{~UD0|-500|}", "-500")]
        [TestCase("{~UD0|SomeResponse|}", "SomeResponse")]
        [TestCase("{~UD0|    |}", "    ")]
        [TestCase("{~UD10|response|}", "response")]
        [TestCase("{~UF0|response|}", "response")]
        [TestCase("{~UC0|response|}", "response")]
        public void TranslateResponse_IsReturningSingleValue_WhenReceivingCorrectResponse(string response, string correctValue)
        {
            ResponseTranslator translator = new ResponseTranslator();
            Dictionary<string, string> preparedResponse = new Dictionary<string, string>();
            preparedResponse.Add("Response", correctValue);

            var producedResponse = translator.TranslateResponse(response);

            Assert.IsTrue(preparedResponse.Count == producedResponse.Count);
            Assert.IsTrue(preparedResponse.ToString() == producedResponse.ToString());



        }


        [TestCase("{~UD0|SOMEVAL|3000|}", 
            new string[]{"SOMEVAL"},
            new string[] { "3000" })]

        [TestCase("{~UD0|SOMEVAL|3000|OTHERVAL|ooooo|}",
            new string[]{"SOMEVAL", "OTHERVAL"},
            new string[] { "3000", "ooooo" })]

        [TestCase("{~UF0|SOMEVAL|3000|OTHERVAL|ooooo|}",
            new string[] { "SOMEVAL", "OTHERVAL" },
            new string[] { "3000", "ooooo" })]

        [TestCase("{~UD0|SOMEVAL|3000|OTHERVAL|ooooo|lAstVal|123456789|}",
            new string[]{"SOMEVAL", "OTHERVAL", "lAstVal"},
            new string[] {"3000", "ooooo", "123456789" })]
        public void TranslateResponse_IsReturningMultipleValue_WhenReceivingCorrectResponse(string response, 
            string[] keys, string[] values)
        {
            Dictionary<string, string> preparedResponse = new Dictionary<string, string>();
            ResponseTranslator translator = new ResponseTranslator();
            for (int i = 0; i < keys.Length; i++)
            {
                preparedResponse.Add(keys[i], values[i]);    
            }
            
            
            var producedResponse = translator.TranslateResponse(response);


            Assert.IsTrue(preparedResponse.Count == producedResponse.Count);
            Assert.IsTrue(preparedResponse.ToString() == producedResponse.ToString());

        }




    }
}