using System.Collections.Generic;
using System.Linq;

namespace lrCalculator
{
    public class CompositeGrammarToken
    {

        public CompositeGrammarToken(List<GrammarToken> tokenList)
        {
            TokenList = tokenList;
            Length=tokenList.Count;
            HashCode=this.GetHashCode();
        }
        public int HashCode {get;}
        public int Length {get;}
        public List<GrammarToken> TokenList { get; }
        public GrammarToken Get(int index){
            return TokenList.ElementAt(index);
        }
        public string toString(){
            string ret="";
            foreach (var item in TokenList)
            {
                ret+=" "+item.Value;
            }
            return ret;
        }
    }
}
