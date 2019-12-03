using System;
using System.Collections.Generic;
using System.Linq;

namespace lrCalculator
{
    public class States
    {
        public static int gl=0;
        public States(int stateNo,CompositeGrammarToken lrItem,int dotPointer,List<KeyValuePair<CompositeGrammarToken,int>> canonicalCollections,List<KeyValuePair<int,GrammarToken>> transisions)
        {

            StateNo = stateNo;
            LrItem = lrItem;
            DotPointer = dotPointer;
            Leaf = transisions.Count==0;
            CanonicalCollections = canonicalCollections;
            Transisions = transisions;
        }
        public int StateNo { get; }
        public CompositeGrammarToken LrItem { get; }
        public int DotPointer { get; }
        public bool Leaf { get; }
        public List<KeyValuePair<CompositeGrammarToken, int>> CanonicalCollections { get; }
        public List<KeyValuePair<int, GrammarToken>> Transisions { get; }
    

        public static List<States> GetStates(GrammarToken start,Dictionary<GrammarToken,List<CompositeGrammarToken>> grammar){

            // listing of all states
            List<States> statesList=new List<States>();
            
            // initialize
            int state=0;
            int dotPointer=0;
            
            //pair => closure item and dot pointer => A.A=> AA,1 
            Queue<KeyValuePair<CompositeGrammarToken,int>> queue=new Queue<KeyValuePair<CompositeGrammarToken, int>>();
            //dictionary  pair => closure item and dot pointer => A.A=> AA,1 <><><>  state 
            Dictionary<KeyValuePair<int,int>,int> seen=new Dictionary<KeyValuePair<int, int>, int>();
        
            queue.Enqueue(new KeyValuePair<CompositeGrammarToken, int>(new CompositeGrammarToken(new List<GrammarToken>{start}),dotPointer));
            // seen.Add(new KeyValuePair<string, int>(start,dotPointer),state);


            while (queue.Count>0)
            {
                gl++;
                if(gl>1000)break;
                
                var currentNode=queue.Dequeue();
                // A.A,1
                List<KeyValuePair<CompositeGrammarToken,int>> canonicalCollections=new List<KeyValuePair<CompositeGrammarToken, int>>();
                // state,(dot going over=> .AB=>A.B => A)
                List<KeyValuePair<int,GrammarToken>> transisionList=new List<KeyValuePair<int, GrammarToken>>();
                canonicalCollections=GetCanonicalCollections(currentNode,grammar);                

                if(currentNode.Value<currentNode.Key.Length){
                    Dictionary<GrammarToken,int> seenTransisionOfNode=new Dictionary<GrammarToken, int>();
                    foreach (var canonicalItem in canonicalCollections)
                    {   
                        var transisionEvent=canonicalItem.Key.Get(canonicalItem.Value);
                        var gotoState=0;
                        if(!seen.ContainsKey(new KeyValuePair<int, int>(canonicalItem.Key.HashCode,canonicalItem.Value+1))){
                            queue.Enqueue(new KeyValuePair<CompositeGrammarToken, int>(canonicalItem.Key,canonicalItem.Value+1));
                            
                            if(seenTransisionOfNode.ContainsKey(transisionEvent))
                                seenTransisionOfNode.TryGetValue(transisionEvent,out gotoState);
                            else
                                gotoState=++state;
                                
                            seen.Add(new KeyValuePair<int, int>(canonicalItem.Key.HashCode,canonicalItem.Value+1),gotoState);
                        }else
                        {
                            gotoState=seen.GetValueOrDefault(new KeyValuePair<int, int>(canonicalItem.Key.HashCode,canonicalItem.Value+1));
                        }
                        
                        if(!seenTransisionOfNode.ContainsKey(transisionEvent))
                            seenTransisionOfNode.Add(transisionEvent,gotoState);
                        
                        transisionList.Add(new KeyValuePair<int,GrammarToken>(gotoState,transisionEvent));
                    }
                }

                int stateNow=seen.GetValueOrDefault(new KeyValuePair<int, int>(currentNode.Key.HashCode,currentNode.Value));
                statesList.Add(new States(stateNow,currentNode.Key,currentNode.Value,canonicalCollections,transisionList));
            }
            return statesList;
        }


        static List<KeyValuePair<CompositeGrammarToken,int>> GetCanonicalCollections(KeyValuePair<CompositeGrammarToken,int> node,Dictionary<GrammarToken,List<CompositeGrammarToken>> grammer){

            List<KeyValuePair<CompositeGrammarToken,int>> canonicalCollections=new List<KeyValuePair<CompositeGrammarToken, int>>();
            Queue<KeyValuePair<CompositeGrammarToken,int>> nonTerminal=new Queue<KeyValuePair<CompositeGrammarToken,int>>();
            Dictionary<KeyValuePair<int,int>,bool> seen=new Dictionary<KeyValuePair<int, int>, bool>();
            
            nonTerminal.Enqueue(new KeyValuePair<CompositeGrammarToken, int>(node.Key,node.Value));
            seen.TryAdd(new KeyValuePair<int, int>(node.Key.HashCode,node.Value),true);
            while (nonTerminal.Count>0)
            {
                gl++;
                if(gl>1000)break;
                

                var closure=nonTerminal.Dequeue();
                
                canonicalCollections.Add(new KeyValuePair<CompositeGrammarToken, int>(closure.Key,closure.Value));
                
                
                if(closure.Value>=closure.Key.Length)continue;

                var rightOfDot=closure.Key.Get(closure.Value);

                if(rightOfDot.IsTerminal)continue;

                foreach (var item in grammer[rightOfDot])
                    {  
                        if(!seen.ContainsKey(new KeyValuePair<int, int>(item.HashCode,0)))
                            {
                                nonTerminal.Enqueue(new KeyValuePair<CompositeGrammarToken, int>(item,0));
                                seen.TryAdd(new KeyValuePair<int, int>(item.HashCode,0),true);
                            } 
                    }
            }
            return canonicalCollections;
        }
    }
}
