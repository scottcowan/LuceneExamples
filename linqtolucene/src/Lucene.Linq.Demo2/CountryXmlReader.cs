using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Data.Sql;
using System.Web;

using System.Data.Common;

namespace Lucene.Linq.Demo2 {

    /// <summary>
    /// Populates the country table with XML data.
    /// See Country.XML in the project
    /// 
    /// This is a simple reader using linq to xml that puts the countries into an enumerable
    /// </summary>
    public class CountryXmlReader {

        private IEnumerable<XElement> GetXmlRows() {

            Assembly ass = Assembly.GetExecutingAssembly();

            // get resource name of Country.XML
            var resourceName = (from res in ass.GetManifestResourceNames()
                               where res.EndsWith("Country.xml",StringComparison.InvariantCultureIgnoreCase)
                               select res).First();


            using (StreamReader sr = new StreamReader(ass.GetManifestResourceStream(resourceName))) {

                XDocument doc = XDocument.Parse(sr.ReadToEnd());

                // return rows
                var rows = (from r in doc.Root.Elements() 
                           select r).Skip(1);

                foreach (var row in rows) {
                    yield return row;
                }


            }
        }



        public IEnumerable<Country> Execute() {

            /*
               Colummns
              <Col0>Sort Order</Col0>
              <Col1>Common Name</Col1>
              <Col2>Formal Name</Col2>
              <Col3>Type</Col3>
              <Col4>Sub Type</Col4>
              <Col5>Sovereignty</Col5>
              <Col6>Capital</Col6>
              <Col7>ISO 4217 Currency Code</Col7>
              <Col8>ISO 4217 Currency Name</Col8>
              <Col9>ITU-T Telephone Code</Col9>
              <Col10>ISO 3166-1 2 Letter Code</Col10>
              <Col11>ISO 3166-1 3 Letter Code</Col11>
              <Col12>ISO 3166-1 Number</Col12>
              <Col13>IANA Country Code TLD</Col13>
             */

            foreach (var row in GetXmlRows()) {

                Country c = new Country();
                c.Name = row.Element(XName.Get("Col1")).Value.HtmlDecode();
                c.FormalName = row.Element(XName.Get("Col2")).Value.HtmlDecode();

                if (row.Element(XName.Get("Col10")).Value.Length > 2) {
                    throw new Exception("two letter code is too short. " + row.Element(XName.Get("Col10")).Value);
                }
                c.TwoLetterCode = row.Element(XName.Get("Col10")).Value.Trim();
                c.ThreeLetterCode = row.Element(XName.Get("Col11")).Value;

                yield return c;
            }
        }

        

    }


    public static class MoreStringExtensions {
       

        public static string HtmlDecode(this string target) {
            return HttpUtility.HtmlDecode(target);
        }

        public static string HtmlEncode(this string target) {
            return HttpUtility.HtmlEncode(target);
        }
    }
}
