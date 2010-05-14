using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Linq;
using Lucene.Linq.Mapping;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
namespace Lucene.Linq.Demo2 {

    
    [Document(DefaultAnalyzer=typeof(SimpleAnalyzer))]
    public class Country: IIndexable, IHit {

        [Field(FieldIndex.Tokenized, FieldStore.Yes, Analyzer = typeof(StandardAnalyzer), IsDefault = true)]
        public string Name { get; set; }
        [Field(FieldIndex.Tokenized, FieldStore.Yes, Analyzer = typeof(StandardAnalyzer), IsDefault = false)]
        public string FormalName { get; set; }
        [Field(FieldIndex.Tokenized, FieldStore.Yes, Analyzer = typeof(SimpleAnalyzer), IsDefault = false)]
        public string TwoLetterCode { get; set; }
        [Field(FieldIndex.Tokenized, FieldStore.Yes, Analyzer = typeof(SimpleAnalyzer), IsDefault = false)]
        public string ThreeLetterCode { get; set; }

        

        #region IHit Members

        public int DocumentId {
            get; set;
        }

        public float Relevance {
            get;
            set;
        }

        #endregion
    }
}
